using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KP.Import.App.Helpers;
using KP.Import.Common.Contracts;
using KP.Import.DA.Sql;
using KP.Import.Models;

namespace KP.Import.App.Importers
{
    public class RealReadingsImporter : Importer
    {
        public RealReadingsImporter(string inputFile, string outputFile, DateTime operDate) : base(inputFile, outputFile, operDate)
        {
        }

        public override void Import()
        {
            var csvAccounts = from rec in ReadRecords<ExchangeRecord>()
                              where rec.Type == ExchangeRecordType.Meter
                              group rec by rec.AccountNumber
                              into g
                              select g.ToCsvAccount();

            List<Appartment> dbAccounts;
            using (var db = new KpImportContext())
            {
                dbAccounts = db.Appartments.Include(x => x.Readings).ToList();
            }

            var result =
                from csvAccount in csvAccounts
                let dbAccount = dbAccounts.FirstOrDefault(x => x.AccountNumber == csvAccount.AccountNumber)
                where dbAccount != null
                let readings = dbAccount.Readings.Where(x => x.Year == this.OperDate.Year && x.Month == this.OperDate.Month).ToList()
                from ReadingKind kind in Enum.GetValues(typeof(ReadingKind))
                let reading = readings.FirstOrDefault(x => x.Kind == kind)
                from meter in csvAccount.GetMeters(kind)
                select new ExchangeRecord
                {
                    Type = ExchangeRecordType.MeterReading,
                    AccountNumber = meter.AccountNumber,
                    Code = meter.Code,
                    Date = this.OperDate,
                    Value = reading.Value,
                };

            WriteRecords(result);
        }
    }
}
