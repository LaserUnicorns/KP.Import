using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.SubsExport
{
    class DbfDal : IDisposable
    {
        private const string CONNECTION_STRING_FMT = "Driver={{Microsoft dBASE Driver (*.dbf)}};DriverID=277;Dbq={0}";

        private readonly OdbcConnection _connection;
        private readonly string _tableName;

        public DbfDal(string directoryName, string tableName)
        {
            _tableName = tableName;
            Filename = Path.Combine(directoryName, _tableName);

            _connection = new OdbcConnection(string.Format(CONNECTION_STRING_FMT, directoryName));
            _connection.Open();

            CreateTable();
        }

        public string Filename { get; }
        
        public void Dispose()
        {
            if (_connection == null) return;

            if (_connection.State == ConnectionState.Closed) return;

            _connection.Close();
        }

        private void CreateTable()
        {
            using (var fs = File.Create(Filename))
            {
                var original = Properties.Resources.original;
                fs.Write(original, 0, original.Length);
            }
        }

        public void AddReceiptItem(ReceiptItemFull item)
        {
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"INSERT INTO [{_tableName}]" +
                                  " (pfr, orgjkx, lsjkx, nsp, ulc, dom, korp, kvar, komnata, kol_reg, kol_vrem, usluga, summa, dolg, gaschd, izmen)" +
                                  " VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                // parameters should follow in strictly this order
                cmd.Parameters.AddWithValue("@pfr", item.Snils);
                cmd.Parameters.AddWithValue("@orgjkx", item.Organization);
                cmd.Parameters.AddWithValue("@lsjkx", item.AccountNumber);
                cmd.Parameters.AddWithValue("@nsp", item.City);
                cmd.Parameters.AddWithValue("@ulc", item.Street);
                cmd.Parameters.AddWithValue("@dom", item.House);
                cmd.Parameters.AddWithValue("@korp", item.Building);
                cmd.Parameters.AddWithValue("@kvar", item.Flat);
                cmd.Parameters.AddWithValue("@komnata", string.Empty);
                cmd.Parameters.AddWithValue("@kol_reg", item.RegisteredCount);
                cmd.Parameters.AddWithValue("@kol_vrem", 0);

                cmd.Parameters.AddWithValue("@usluga", item.ServiceCode);
                cmd.Parameters.AddWithValue("@summa", (double)item.Amount);

                cmd.Parameters.AddWithValue("@dolg", string.Empty);
                cmd.Parameters.AddWithValue("@gaschd", string.Empty);
                cmd.Parameters.AddWithValue("@izmen", string.Empty);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
