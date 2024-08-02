using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace dbwpfprject.DB
{
    public class DBDB
    {
        public static MySqlConnection connect()
        {
            string ip = "127.0.0.1";
            int port = 3306;
            string uid = "root";
            string pwd = "1234";
            string dbname = "dbproject";
            MySqlConnection conn;
            string connectString = $"Server={ip};Port={port};Database={dbname};Uid={uid};Pwd={pwd};CharSet=utf8;";
            conn = new MySqlConnection(connectString);
            return conn;
        }

    }
}
