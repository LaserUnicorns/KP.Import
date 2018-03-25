using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.SubsExport
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbPath = "C:\\IMAS\\KVARTPLATA\\DATA0.FDB";
            var dateService = new DateService();
            var workingPeriodService = new WorkingPeriodService(dateService);
            var snilsMapFactory = new SnilsMapFactory();
            var serviceCodeMapFactory = new ServiceCodeMapFactory();

            var dbDal = new ImasDal(workingPeriodService, dbPath);
            var dbList = dbDal.GetReceipt();

            var mapper = new ServiceTransformer(snilsMapFactory, serviceCodeMapFactory);
            var fullList = mapper.Map(dbList);

            var tableName = $"S{Properties.Resources.Organization}{dateService.GetWorkingDate():yyMM}.dbf";
            string filename;
            using (var dbfDal = new DbfDal(Directory.GetCurrentDirectory(), tableName))
            {
                filename = dbfDal.Filename;

                foreach (var item in fullList)
                {
                    dbfDal.AddReceiptItem(item);
                }
            }

            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var targetFile = Path.Combine(desktop,
                $"{Path.GetFileNameWithoutExtension(filename)}_imp{Path.GetExtension(filename)}");
            if (File.Exists(targetFile))
            {
                File.Delete(targetFile);
            }
            File.Move(filename, targetFile);

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
