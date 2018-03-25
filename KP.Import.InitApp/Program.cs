using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using KP.Import.Common.Contracts;
using KP.Import.DA.Sql;
using KP.Import.InitApp.DataObjects;

namespace KP.Import.InitApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //{
            //    var filepath = @"G:\_kp\list.csv";
            //    var reader = new StreamReader(filepath, Encoding.GetEncoding(1251));

            //    var csv = new CsvReader(reader);
            //    csv.Configuration.Delimiter = ";";
            //    csv.Configuration.HasHeaderRecord = false;
            //    csv.Configuration.RegisterClassMap<PersonMap>();

            //    var persons = csv.GetRecords<Person>().ToList();

            //    using (var db = new KpImportContext())
            //    {
            //        foreach (var person in persons)
            //        {
            //            db.Appartments.Add(new Appartment
            //            {
            //                AccountNumber = person.Account,
            //                Owner = person.Name,
            //                AppartmentNumber = int.Parse(person.Account.ToString().Replace("461702", ""))
            //            });
            //        }

            //        db.SaveChanges();
            //    }
            //}
            {
                var filepath = @"g:\_kp\curr.csv";
                var reader = new StreamReader(filepath, Encoding.GetEncoding(1251));

                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<ReadingsMap>();

                var readings = csv.GetRecords<Readings>().ToList();

                using (var db = new KpImportContext())
                {
                    foreach (var reading in readings)
                    {
                        db.Readings.Add(new Reading
                        {
                            AccountNumber = reading.AccountNumber,
                            Month = 10,
                            Year = 2015,
                            Kind = ReadingKind.Cold,
                            Value = reading.ColdWater
                        });

                        db.Readings.Add(new Reading
                        {
                            AccountNumber = reading.AccountNumber,
                            Month = 10,
                            Year = 2015,
                            Kind = ReadingKind.Hot,
                            Value = reading.HotWater
                        });
                    }

                    db.SaveChanges();
                }
            }

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
