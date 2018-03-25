using System;
using KP.Import.App.Importers;

namespace KP.Import.App
{
    class Program
    {
        static void Main(string[] args)
        {
            const string inn = "7605020213";

            var importer = new ReadingsImporter(
                //$@"\\atlant\exchange\L_7605020213_A040F09B-CE3C-4354-A849-62D110630508.CSV",
                //$@"\\atlant\exchange\P_{inn}_{Guid.NewGuid()}.csv",
                @"C:\Users\Viktor\Downloads\L_7605020213_817489FC-2D0B-4B84-9D94-2F5F6F1D61AA.CSV",
                $@"C:\Users\Viktor\Downloads\P_{inn}_{Guid.NewGuid()}.csv",
                new DateTime(2018, 02, 28));

            //var importer = new PaymentsImporter(
            //    $@"G:\OneDrive\work\volga\201611\payments.csv",
            //    $@"G:\OneDrive\work\volga\201611\P_{inn}_{Guid.NewGuid()}.csv",
            //    new DateTime(2016, 11, 30)
            //    );

            //var importer = new RealReadingsImporter(
            //    $@"\\atlant\exchange\L_7605020213_22869715-9BFA-48D9-947D-BC452359974F.CSV",
            //    $@"\\atlant\exchange\P_{inn}_{Guid.NewGuid()}.csv",
            //    new DateTime(2017, 06, 30));

            importer.Import();

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
