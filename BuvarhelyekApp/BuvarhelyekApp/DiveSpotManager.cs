using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class DiveSpotManager
{
    public List<DiveSpot> DiveSpots { get; set; }

    public DiveSpotManager()
    {
        DiveSpots = new List<DiveSpot>();
    }

    public void AddDiveSpot(DiveSpot spot)
    {
        DiveSpots.Add(spot);
    }

    public void ListAll()
    {
        if (DiveSpots.Count == 0)
        {
            Console.WriteLine("Nincs még búvárhely a listában.");
            return;
        }

        Console.WriteLine("\n--- Búvárhelyek listája ---");
        foreach (var spot in DiveSpots)
        {
            Console.WriteLine(spot);
        }
    }

    public List<DiveSpot> SearchByName(string keyword)
    {
        return DiveSpots
            .Where(s => s.Name.ToLower().Contains(keyword.ToLower()))
            .ToList();
    }

    public List<DiveSpot> FilterByCategory(string category)
    {
        return DiveSpots
            .Where(s => s.Category.ToLower() == category.ToLower())
            .ToList();
    }

    public void SortByRatingDescending()
    {
        DiveSpots = DiveSpots
            .OrderByDescending(s => s.Rating)
            .ToList();
    }

    public void PrintList(List<DiveSpot> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Nincs találat.");
            return;
        }

        foreach (var spot in list)
        {
            Console.WriteLine(spot);
        }
    }

    public int GetNextId()
    {
        if (DiveSpots.Count == 0)
        {
            return 1;
        }

        return DiveSpots.Max(s => s.Id) + 1;
    }
    public void SaveToCsv(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.WriteLine("Id,Name,Category,Description,Depth,Rating,IsFavorite");

            foreach (var spot in DiveSpots)
            {
                writer.WriteLine($"{spot.Id},{spot.Name},{spot.Category},{spot.Description},{spot.Depth},{spot.Rating},{spot.IsFavorite}");
            }
        }
    }
    public void LoadFromCsv(string fileName)
    {
        if (!File.Exists(fileName))
        {
            Console.WriteLine("A fájl nem található.");
            return;
        }

        DiveSpots.Clear();

        string[] lines = File.ReadAllLines(fileName);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(',');

            int id = int.Parse(parts[0]);
            string name = parts[1];
            string category = parts[2];
            string description = parts[3];
            int depth = int.Parse(parts[4]);
            int rating = int.Parse(parts[5]);
            bool isFavorite = bool.Parse(parts[6]);

            DiveSpot spot = new DiveSpot(id, name, category, description, depth, rating, isFavorite);
            DiveSpots.Add(spot);
        }
    }
}
