using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace GuiaPalestrasOnline.Repositorio
{
    public class Contexto
    {
        public Contexto()
        {
            SqlBd.Open();
        }
        public  MySqlConnection SqlBd = new MySqlConnection("Database=palestrasonline;Data Source=localhost;User Id=root; Password=root");
    }
}