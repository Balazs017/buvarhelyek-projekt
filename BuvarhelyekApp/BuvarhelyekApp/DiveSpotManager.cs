using System;
using System.Collections.Generic;
using System.Linq;

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
}
