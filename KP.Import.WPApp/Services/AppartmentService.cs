using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KP.Import.Common.Contracts;

namespace KP.Import.WPApp.Services
{
    public interface IAppartmentService
    {
        Task<Appartment> GetAppartment(int number);
        Task<Appartment> GetAppartmentFull(int number, int month, int year);
    }

    public class AppartmentService : ServiceBase, IAppartmentService
    {
        public Task<Appartment> GetAppartment(int number)
        {
            return GetResponse<Appartment>("GetApparment", new Dictionary<string, object>
            {
                ["number"] = number
            });
        }

        public Task<Appartment> GetAppartmentFull(int number, int month, int year)
        {
            return GetResponse<Appartment>("GetAppartment", new Dictionary<string, object>
            {
                ["number"] = number,
                ["month"]=month,
                ["year"]=year
            });
        }

        protected override string Controller => "Appartment";
    }
}
