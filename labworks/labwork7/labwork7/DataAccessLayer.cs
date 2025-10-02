using Microsoft.Data.SqlClient;
using System.Data;

namespace labwork7
{
    public static class DataAccessLayer
    {
        private static string _server = "mssql";
        private static string _datebase = "ispp3101";
        private static string _userLogin = "ispp3101";
        private static string _password = "3101";

        private static List<Film> _films = [];

        public static string ConnectionString
        {
            get
            {
                SqlConnectionStringBuilder _builder = new()
                {
                    DataSource = _server,
                    InitialCatalog = _datebase,
                    UserID = _userLogin,
                    Password = _password,
                    TrustServerCertificate = true
                };

                return _builder.ConnectionString;
            }
        }

        public static void ChangeConnectionSettings(string server, string database, string login, string password)
        {
            _server = server;
            _datebase = database;
            _userLogin = login;
            _password = password;
        }

        public static bool TryConnection()
        {
            using SqlConnection connection = new(ConnectionString);

            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<int> UpdateRowsCommandAsync(string sqlCommand)
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand command = new(sqlCommand, connection);
                int changedRowsAmount = await command.ExecuteNonQueryAsync();
                return changedRowsAmount;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public static async Task<object?> GetRowsCommandAsync(string sqlCommand)
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();
                SqlCommand command = new(sqlCommand, connection);

                return await command.ExecuteScalarAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static async Task ChangeSessionPriceAsync(decimal newPrice, int sessionId)
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();

                string query = "UPDATE Session SET Price = @newPrice WHERE SessionId = @sessionId";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@newPrice", newPrice);
                command.Parameters.AddWithValue("@sessionId", sessionId);

                await command.ExecuteNonQueryAsync();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public static async Task UploadFilmPosterAsync(int filmId, string fileName)
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();

                string query = "UPDATE Film SET Poster = @fileData WHERE filmId = @filmId";
                SqlCommand command = new(query, connection);

                byte[] fileData = await File.ReadAllBytesAsync(fileName);

                if (fileData.Length < 1024)
                {
                    command.Parameters.AddWithValue("@filmId", filmId);
                    command.Parameters.AddWithValue("@fileData", fileData);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task DownloadFilmPosterAsync(int filmId, string fileName)
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();

                string query = "SELECT Film WHERE filmId = @filmId";
                SqlCommand command = new(query, connection);

                command.Parameters.AddWithValue("@filmId", filmId);

                var fileData = await command.ExecuteScalarAsync();

                await File.WriteAllBytesAsync(fileName, fileData as byte[]);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task<List<Film>> GetFilmsAsync()
        {
            await using SqlConnection connection = new(ConnectionString);

            try
            {
                await connection.OpenAsync();
                string query = "SELECT Film WHERE filmId = @filmId";
                SqlCommand command = new(query, connection);

                var reader = await command.ExecuteReaderAsync();

                if(reader.HasRows)
                    while (reader.Read())
                    {
                        _films.Add(new Film
                        {
                            FilmId = Convert.ToInt32(reader["FilmId"]),
                            Name = reader["Name"].ToString(),
                            Duration = Convert.ToUInt16(reader["Duration"]),
                            ReleaseYear = DateOnly.FromDateTime(Convert.ToDateTime(reader["ReleaseDate"])),
                            Description = reader["Description"].ToString(),
                            Poster = (byte[])reader["Poster"],
                            AgeLimit = Convert.ToByte(reader["AgeLimit"]),
                            StartRental = DateOnly.FromDateTime(Convert.ToDateTime(reader["StartRental"])),
                            EndRental = DateOnly.FromDateTime(Convert.ToDateTime(reader["EndRental"]))
                        });
                    }

                return _films;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
