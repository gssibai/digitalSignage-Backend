namespace backend.Models;

public class ScreenTagDto
{
    public int TagId { get; set; }
    public int ScreenId { get; set; }
    public string TagName { get; set; }
    public string ScreenName { get; set; }
}

public class ScreenTagCreateDto
{
    public int ScreenId { get; set; }
    public string TagName { get; set; }
}