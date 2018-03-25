using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace KP.Import.InitApp.DataObjects
{
    public class Person
    {
        public long Account { get; set; }
        public string Name { get; set; }
    }

    public class PersonMap : CsvClassMap<Person>
    {
        public PersonMap()
        {
            Map(x => x.Account).Index(0);
            Map(x => x.Name).Index(1);
        }
    }
}
