using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Services;
using System.IO;
using System.Windows;

namespace Labwork24
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TicketService _ticketService = new(new CinemaDbContext());

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void SaveTicketButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(TicketIdTextBox.Text, out int ticketid))
                MessageBox.Show("ID должен быть числом!");

            var ticket = await _ticketService.GetTicketByIdAsync(ticketid);
            string filmName = await _ticketService.GetFilmNameByTicketIdAsync(ticketid);
            DateTime startDate = await _ticketService.GetSessionStartTimeByTicketId(ticketid);
            string cinemaName = await _ticketService.GetCinemaNameByTicketIdAsync(ticketid);
            int hallNumber = await _ticketService.GetHallNumByTicketIdAsync(ticketid);

            string result = $"Билет {ticket.TicketId} \r\n{filmName}\r\nНачало сеанса: {startDate:'hh:mm dd MMMMM'}\r\nКинотеатр: {cinemaName}\r\nЗал: {hallNumber}\r\nРяд: {ticket.Row} Место: {ticket.Seat}";

            string fileName = "C:\\Temp\\1.txt";

            using (StreamWriter writer = new(fileName))
            {
               await writer.WriteLineAsync(result);
            }
        }
    }
}