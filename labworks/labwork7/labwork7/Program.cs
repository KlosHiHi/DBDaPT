namespace labwork7
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Task 1
            Console.WriteLine(DataAccessLayer.ConnectionString);

            //DataAccessLayer.ChangeConnectionSettings("pgsad", "ispp0101", "user", "12134");
            //Console.WriteLine(DataAccessLayer.ConnectionString);
            //DataAccessLayer.TryConnection();

            DataAccessLayer.ChangeConnectionSettings("mssql", "ispp3101", "ispp3101", "3101");
            Console.WriteLine(DataAccessLayer.ConnectionString);

            if (DataAccessLayer.TryConnection())
                Console.WriteLine("Подключение успешно");

            //Task 2
            Console.WriteLine($"Изменено строк: {await DataAccessLayer.UpdateRowsCommandAsync("UPDATE Game SET Price=1100 WHERE GameId = 11")}");
            Console.WriteLine(await DataAccessLayer.GetRowsCommandAsync("SELECT * FROM Game WHERE GameId = 10"));

            //Task 3
            DataAccessLayer.ChangeSessionPriceAsync(350, 3);

            //Task4
            DataAccessLayer.UploadFilmPosterAsync(1, "");

            DataAccessLayer.DownloadFilmPosterAsync(1, "");

            //Task 5
            List<Film> films = await DataAccessLayer.GetFilmsAsync();

            foreach (Film film in films)
            {
                Console.WriteLine(film);
            }
        }
    }
}
