using ExcelDemo.Core.Constants;
using ExcelDemo.Core.Helpers;
using ExcelDemo.Core.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ExcelDemo.DAL.Repositories
{
    public class Repository : IRepository
    {
        private readonly IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ExecuteInsertData(DataTable dt, string tableName)
        {
            var conString = _configuration.GetSection(Constants.ConfigConnection).Value;
            using var con = new SqlConnection(conString);
            con.Open();
            SqlBulkCopy blk = new SqlBulkCopy(con);
            blk.DestinationTableName = $"[{tableName}]";
            blk.WriteToServer(dt);
            con.Close();
        }

        public async Task ExecuteCreateTable(string query)
        {
            var conString = _configuration.GetSection(Constants.ConfigConnection).Value;
            using var con = new SqlConnection(conString);
            SqlCommand sqlCommand = new SqlCommand(query, con);
            con.Open();
            await sqlCommand.ExecuteNonQueryAsync();
        }

        public async Task<DataTable> GetData(string query)
        {
            DataTable dt = new DataTable();

            var conString = _configuration.GetSection(Constants.ConfigConnection).Value;
            using var con = new SqlConnection(conString);
            SqlCommand sqlCommand = new SqlCommand(query, con);
            con.Open();
            using IDataReader dr = await sqlCommand.ExecuteReaderAsync();
            if (dr.FieldCount > 0)
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    DataColumn dc = new DataColumn(dr.GetName(i), dr.GetFieldType(i));
                    dt.Columns.Add(dc);
                }
                object[] rowobject = new object[dr.FieldCount];
                while (dr.Read())
                {
                    dr.GetValues(rowobject);
                    dt.LoadDataRow(rowobject, true);
                }
            }
            return dt;
        }
    }
}
