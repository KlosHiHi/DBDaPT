using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Services;
using Microsoft.Win32;
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
            DateTime var = new();

            var ticket = await _ticketService.GetTicketDtoByIdAsync(ticketid);
            string result = $"Билет {ticket.TicketId} \r\n{ticket.FilmName}\r\nНачало сеанса: {ticket.SessionStartDate?.ToString("HH:mm d MMMM")}\r\nКинотеатр: {ticket.CinemaName}\r\nЗал: {ticket.HallNumber}\r\nРяд: {ticket.Row} Место: {ticket.Seat}";

            SaveFileDialog fileDialog = new();
            fileDialog.ShowDialog();

            var fileName = fileDialog.FileName;

            using (StreamWriter writer = new(fileName))
            {
                writer.WriteLine(result);
            }
        }
    }
}