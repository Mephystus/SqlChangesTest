namespace SqlChangesTest.Schema;

public class MyTable
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
}