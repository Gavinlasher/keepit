namespace keepit.Models
{
  public class Keep
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public int Views { get; set; }
    public int Kept { get; set; }
    public string CreatorId { get; set; }
    public Profile Creator { get; set; }
  }
}