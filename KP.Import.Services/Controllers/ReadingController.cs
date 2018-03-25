using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KP.Import.Common.Contracts;
using KP.Import.DA.Sql;

namespace KP.Import.Services.Controllers
{
    public class ReadingController : ApiController
    {
        [HttpPost]
        public void SaveReadings(List<Reading> readings)
        {
            using (var db = new KpImportContext())
            {
                //if (coldWater.HasValue)
                //{
                //    db.Readings.Add(new Reading
                //    {
                //        AccountNumber = accountNumber,
                //        Month = month,
                //        Kind = ReadingKind.Cold,
                //        Year = year,
                //        Value = coldWater.Value
                //    });
                //}

                //if (hotWater.HasValue)
                //{
                //    db.Readings.Add(new Reading
                //    {
                //        AccountNumber = accountNumber,
                //        Month = month,
                //        Kind = ReadingKind.Hot,
                //        Year = year,
                //        Value = hotWater.Value
                //    });
                //}

                foreach (var reading in readings)
                {
                    //db.Readings.Add(reading);
                    var r = db.Readings.FirstOrDefault(
                        x => x.AccountNumber == reading.AccountNumber &&
                             x.Year == reading.Year &&
                             x.Month == reading.Month &&
                             x.Kind == reading.Kind);
                    if (r == null)
                    {
                        db.Readings.Add(reading);
                    }
                    else
                    {
                        r.Value = reading.Value;
                    }
                }

                db.SaveChanges();
            }
        }
    }
}
