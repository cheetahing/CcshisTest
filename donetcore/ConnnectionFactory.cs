using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace donetcore
{
    public static class ConnnectionFactory
    {
        public const string ConnectionString = "Data Source=192.168.1.50;Initial Catalog=testMq;User ID=sa;password=123456;MultipleActiveResultSets=true";

        public const string ConnectionString2 = "Data Source=192.168.1.50;Initial Catalog=testMq2;User ID=sa;password=123456;MultipleActiveResultSets=true";
        public static IDbConnection dbConnection;

        public static IDbConnection Create()
        {
            //if (dbConnection == null)
            //{
                dbConnection= new SqlConnection(ConnectionString);
        //}
            return dbConnection;
        }
        public static IDbConnection Create2()
        {
            //if (dbConnection == null)
            //{
            dbConnection = new SqlConnection(ConnectionString2);
            //}
            return dbConnection;
        }
    }
}
