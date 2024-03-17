using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace testFirstWPF
{
    public static class DateBase
    {
        private static readonly string connectionString = @"Data Source = /root/restaurantbot/restaurant.db";

        private async static Task<bool> IfProcessExists(string name)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                command.CommandText = $"SELECT COUNT(*) FROM Process WHERE Name LIKE '{name}'";

                int existsProcess = (int)await command.ExecuteScalarAsync();

                if(existsProcess > 0)
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }


            }

        }

        public async static Task AddProcess(string name, string startTime)
        {
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var command = new SqliteCommand();
                command.Connection = connection;
                if (await IfProcessExists(name))
                {
                    command.CommandText = $"INSERT INTO Process (Name,  Time) values (@Name, @Time)";

                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Time", startTime);

                    await command.ExecuteNonQueryAsync();

                    connection.Close();
                }
                else
                {
                    connection.Close();
                }
                


            }

        }
    }
}
