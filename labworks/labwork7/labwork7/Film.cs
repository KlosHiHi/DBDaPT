namespace labwork7
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Name { get; set; }
        public ushort Duration { get; set; }
        public DateOnly ReleaseYear { get; set; }
        public string Description { get; set; } = "";
        public byte[] Poster { get; set; } = [];
        public byte AgeLimit { get; set; } = (byte)0;
        public DateOnly StartRental { get; set; } = DateOnly.FromDateTime(DateTime.Now.Date);
        public DateOnly EndRental { get; set; } = DateOnly.FromDateTime(DateTime.Now.Date);

        public override string ToString()
        {
            return $"{FilmId}.{Name} - ({ReleaseYear}г.) \n{Description} {Duration}мин., \nНачало проката {StartRental}, Конец проката {EndRental}, {AgeLimit}+";
        }
    }
}
