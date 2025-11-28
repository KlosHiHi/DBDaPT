namespace GameStoreDbLibrary.Models;

public partial class Lw23comment
{
    public int CommentId { get; set; }
    public int? PostId { get; set; }
    public string? Text { get; set; }

    public virtual Lw23post? Post { get; set; }
}
