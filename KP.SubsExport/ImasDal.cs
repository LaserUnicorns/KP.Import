using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;

namespace KP.SubsExport
{
    class ImasDal
    {
        static string MakeConnectionString(string ds, string db, string user, string password)
        {
            return
                $"User={user};" +
                $"Password={password};" +
                $"Database={db};" +
                $"DataSource={ds};" +
                "Port=3050;" +
                "Dialect=3;" +
                "Charset=NONE;" +
                "Role=;" +
                "Connection lifetime=15;" +
                "Pooling=true;" +
                "MinPoolSize=0;" +
                "MaxPoolSize=50;" +
                "Packet Size=8192;" +
                "ServerType=0";
        }

        private readonly IWorkingPeriodService _workingPeriodService;
        private readonly string _connectionString;

        public ImasDal(
            IWorkingPeriodService workingPeriodService,
            string ds,
            string db,
            string user = "sysdba",
            string password = "masterkey"
            )
        {
            _workingPeriodService = workingPeriodService;
            _connectionString = MakeConnectionString(ds, db, user, password);
        }

        public List<DbReceiptItem> GetReceipt()
        {
            var result = new List<DbReceiptItem>();

            var workingPeriod = _workingPeriodService.GetWorkingPeriod();

            using (var connection = new FbConnection(this._connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "SELECT k.NSCHET, k.KVARTIRA, ik.PROPISANO, j.KODTARIF, j.SUMMA, j.KINDREG " +
                        "FROM JOURNAL j " +
                        "JOIN KARTA k ON k.KODKARTA = j.KODKARTA " +
                        "JOIN INDKARTA ik ON ik.KODKARTA = k.KODKARTA " +
                        $"WHERE j.DATEREG >= '{workingPeriod.Start:yyyy-MM-dd}' " +
                        $"AND j.DATEREG < '{workingPeriod.End:yyyy-MM-dd}' " +
                        "AND j.PRINADLEJNOST = 'T' " +
                        $"AND (ik.datak is null OR '{workingPeriod.Start:yyyy-MM-dd}' <= ik.datak) " +
                        "AND j.KODKARTA = 178 ";

                    using (var r = command.ExecuteReader())
                    {
                        if (r.HasRows)
                        {
                            while (r.Read())
                            {
                                var item = new DbReceiptItem
                                {
                                    AccountNumber = r.GetString(r.GetOrdinal("NSCHET")),
                                    Flat = r.GetString(r.GetOrdinal("KVARTIRA")),
                                    RegisteredCount = r.GetInt32(r.GetOrdinal("PROPISANO")),
                                    ServiceCode = (ImasServiceCode) r.GetInt32(r.GetOrdinal("KODTARIF")),
                                    ODN = r.GetString(r.GetOrdinal("KINDREG")),
                                    Amount = r.GetDecimal(r.GetOrdinal("SUMMA")),
                                };

                                result.Add(item);
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}
