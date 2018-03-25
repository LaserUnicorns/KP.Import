using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using KP.Import.App.Models;
using KP.Import.App.Util;
using KP.Import.Common.Contracts;
using KP.Import.DA.Sql;
using KP.Import.Models;

namespace KP.Import.App.Importers
{
    public class PaymentsImporter : Importer
    {
        public PaymentsImporter(string inputFile, string outputFile, DateTime operDate) : base(inputFile, outputFile, operDate)
        {
            CsvConfiguration.RegisterClassMap<PaymentMap>();
        }

        public override void Import()
        {
            List<Appartment> dbAccounts;
            using (var db = new KpImportContext())
            {
                dbAccounts = db.Appartments.Include(x => x.Readings).ToList();
            }

            var records =
                from payment in ReadRecords<Payment>()
                let dbAccount = dbAccounts.FirstOrDefault(x => x.AppartmentNumber == payment.ApartmentNumber)
                select new ExchangeRecord
                {
                    Type = ExchangeRecordType.Payment,
                    AccountNumber = dbAccount.AccountNumber,
                    Date = OperDate,
                    Value = payment.Amount
                };

            WriteRecords(records);
        }
    }
}