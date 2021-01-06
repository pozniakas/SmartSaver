using System.Collections.Generic;
using System.Data;
using Npgsql;
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
    }
}
