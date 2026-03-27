public class DiveSpot
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public int Depth { get; set; }
    public int Rating { get; set; }
    public bool IsFavorite { get; set; }

    public DiveSpot(int id, string name, string category, string description, int depth, int rating, bool isFavorite)
    {
        Id = id;
        Name = name;
        Category = category;
        Description = description;
        Depth = depth;
        Rating = rating;
        IsFavorite = isFavorite;
    }

    public override string ToString()
    {
        return $"{Id} | {Name} | {Category} | Mélység: {Depth} m | Értékelés: {Rating} | Kedvenc: {(IsFavorite ? "Igen" : "Nem")}";
    }
}
