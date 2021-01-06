using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Npgsql;
using System.Diagnostics;
using System.Configuration;
using DbEntities.Entities;

namespace WebAPI.Services
{
    public class AdoNetContext
    {
        private string connStr = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

        public IEnumerable<Goal> SelectGoals()
        {
            var connection = new NpgsqlConnection(connStr);
            connection.Open();

            var dataAdapter = new NpgsqlDataAdapter();
            var command = new NpgsqlCommand("SELECT * FROM smartsaver.goal;", connection);
            //dataAdapter.SelectCommand = command;

            using NpgsqlDataReader rdr = command.ExecuteReader();

            while (rdr.Read())
            {
                yield return new Goal
                {
                    Id = rdr.GetInt32(0),
                    Title = rdr.GetString(1),
                    Description = rdr.GetString(2),
                    Amount = rdr.GetDecimal(3),
                    Deadlinedate = rdr.GetDateTime(4),
                    Creationdate = rdr.GetDateTime(5)
                };
                //Debug.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                //        rdr.GetString(2));
            }

            connection.Close();
            dataAdapter.Dispose();
        }

        public void Update(Goal goal)
        {
            using var connection = new NpgsqlConnection(connStr);
            connection.Open();

            using var dataAdapter = new NpgsqlDataAdapter();
            dataAdapter.SelectCommand = new NpgsqlCommand("SELECT * FROM smartsaver.goal;", connection);

            var commandBuilder = new NpgsqlCommandBuilder(dataAdapter);

            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                if ((long) row["Id"] == goal.Id)
                {
                    row["Title"] = goal.Title;
                    row["Amount"] = goal.Amount;
                    row["Description"] = goal.Description;
                    row["Deadlinedate"] = goal.Deadlinedate;
                }
            }

            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
            dataAdapter.Update(dataTable);
        }

        //public void GetVersion()
        //{
        //    var cs = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";

        //    using var con = new NpgsqlConnection(cs);
        //    con.Open();

        //    var sql = "SELECT version();";

        //    using var cmd = new NpgsqlCommand(sql, con);

        //    var version = cmd.ExecuteScalar().ToString();
        //    Debug.WriteLine($"PostgreSQL version: {version}");
        //}
    }
}
