using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KP.Import.Common.Contracts;
using KP.Import.DA.Sql;

namespace KP.Import.Services.Controllers
{
    public class AppartmentController : ApiController
    {
        public Appartment GetAppartment(int number)
        {
            using (var db = new KpImportContext())
            {
                return db.Appartments.FirstOrDefault(x => x.AppartmentNumber == number);
            }
        }

        public Appartment GetAppartment(int number, int month, int year)
        {
            using (var db = new KpImportContext())
            {
                var appartment = db.Appartments
                                   .Where(x => x.AppartmentNumber == number)
                                   .Include(x => x.Readings)
                                   .FirstOrDefault();
                //db.Entry(appartment)
                //  .Collection(x => x.Readings)
                //  .Query()
                //  .Where(x => x.Month < month)
                //  .GroupBy(x => x.Kind,
                //      (kind, readings) => readings.OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).Take(1))
                //  .Load();

                return appartment;
            }
        }
    }
}
