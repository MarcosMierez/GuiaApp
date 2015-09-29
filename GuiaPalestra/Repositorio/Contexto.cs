using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.SessionState;

namespace GuiaPalestrasOnline.Repositorio
{
    public class Contexto
    {
        public static string getConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MYSQL_CONNECTION_STRING") ??
                   "Database=palestrasonline;Data Source=localhost;User Id=root; Password=root; Pooling=false";
        }
        public Contexto()
        {
            SqlBd.Open();
        }

        public MySqlConnection SqlBd = new MySqlConnection(getConnectionString());
    }
}