namespace FilmLibrary.Models;

public partial class Frame
{
    public int FrameId { get; set; }
    public int FilmId { get; set; }
    public string FileName { get; set; } = null!;

    public virtual Film Film { get; set; } = null!;
}
