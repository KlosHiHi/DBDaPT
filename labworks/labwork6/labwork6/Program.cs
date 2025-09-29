

namespace labwork6
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
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
            Console.WriteLine(await DataAccessLayer.SelectRowsCommandAsync("SELECT * FROM Game WHERE GameId = 10"));

            //Task 1
            DataAccessLayer.ChangeSessionPriceAsync(350, 3);

            byte[] fileData = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "")); // чтение содержимого файла
            if (fileData.Length < 1024) // проверка, что размер меньше 1 КБ
            {
                // выполнение SqlCommand с параметром, в параметр передать значение fileData
            }
        }
    }
}
