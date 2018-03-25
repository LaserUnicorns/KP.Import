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
    public class ReadingsImporter : Importer
    {
        public ReadingsImporter(string inputFile, string outputFile, DateTime operDate) : base(inputFile, outputFile, operDate)
        {
        }

        public override void Import()
        {
            var csvAccounts = from rec in ReadRecords<ExchangeRecord>()
                              where rec.Type == ExchangeRecordType.Meter
                              group rec by rec.AccountNumber
                              into g
                              select g.ToCsvAccount();

            //var curr = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var curr = new DateTime(OperDate.Year, OperDate.Month, 1);
            var prev = curr.AddDays(-1);

            List<Appartment> dbAccounts;
            using (var db = new KpImportContext())
            {
                dbAccounts = db.Appartments.Include(x => x.Readings).ToList();
            }

            var result =
                from csvAccount in csvAccounts
                let dbAccount = dbAccounts.FirstOrDefault(x => x.AccountNumber == csvAccount.AccountNumber)
                where dbAccount != null
                //from dbAccount in dbAccounts
                //let csvAccount = csvAccounts.FirstOrDefault(x=>x.AccountNumber == dbAccount.AccountNumber)
                //where csvAccount != null
                let currReadings = dbAccount.Readings.Where(x => x.Year == curr.Year && x.Month == curr.Month).ToList()
                let lastReadings = dbAccount.Readings.Where(x => x.Year == prev.Year && x.Month == prev.Month).ToList()
                from ReadingKind kind in Enum.GetValues(typeof(ReadingKind))
                let current = currReadings.FirstOrDefault(x => x.Kind == kind)
                let previous = lastReadings.FirstOrDefault(x => x.Kind == kind)
                let consumption = current?.Value - previous?.Value
                where consumption.HasValue
                from meter in csvAccount.GetMeters(kind)
                select new ExchangeRecord
                {
                    Type = ExchangeRecordType.MeterReading,
                    AccountNumber = meter.AccountNumber,
                    Code = meter.Code,
                    Date = curr.AddMonths(1).AddDays(-1),
                    Value = meter.Value + consumption.Value
                };

            WriteRecords(result);
        }
    }
}