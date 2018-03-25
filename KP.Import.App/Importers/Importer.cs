using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using KP.Import.App.Util;
using KP.Import.Models;

namespace KP.Import.App.Importers
{
    public abstract class Importer
    {
        protected string InputFile { get; }
        protected string OutputFile { get; }
        protected DateTime OperDate { get; }
        protected virtual Encoding Encoding => Encoding.GetEncoding(1251);

        protected CsvConfiguration CsvConfiguration { get; }
            = new CsvConfiguration
            {
                Delimiter = ";",
                HasHeaderRecord = false
            };

        protected Importer(string inputFile, string outputFile, DateTime operDate)
        {
            if (operDate < DateTime.Today.AddMonths(-1))
            {
                throw new ArgumentException(string.Empty, nameof(operDate));
            }

            InputFile = inputFile;
            OutputFile = outputFile;
            OperDate = operDate;

            CsvConfiguration.RegisterClassMap<ExchangeRecordMap>();
        }

        public abstract void Import();

        protected IEnumerable<T> ReadRecords<T>()
        {
            using (var reader = new StreamReader(InputFile, Encoding))
            {
                using (var csvReader = new CsvReader(reader, CsvConfiguration))
                {
                    return csvReader.GetRecords<T>().ToList();
                }
            }
        }

        protected void WriteRecords(IEnumerable<ExchangeRecord> records)
        {
            using (var writer = new StreamWriter(OutputFile, false, Encoding))
            {
                using (var csvWriter = new CsvWriter(writer, CsvConfiguration))
                {
                    csvWriter.WriteRecords(records);
                }
            }
        }
    }
}