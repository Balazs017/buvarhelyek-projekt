DiveSpotManager manager = new DiveSpotManager();

bool running = true;

while (running)
{
    Console.WriteLine("1 - Elem hozzáadása");
    Console.WriteLine("2 - Lista megjelenítése");
    Console.WriteLine("3 - Keresés név alapján");
    Console.WriteLine("4 - Szűrés kategória szerint");
    Console.WriteLine("5 - Rendezés értékelés szerint");
    Console.WriteLine("6 - CSV mentés");
    Console.WriteLine("7 - CSV betöltés");
    Console.WriteLine("0 - Kilépés");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddNewDiveSpot(manager);
            break;

        case "2":
            manager.ListAll();
            break;

        case "3":
            SearchDiveSpot(manager);
            break;

        case "4":
            FilterDiveSpot(manager);
            break;

        case "5":
            manager.SortByRatingDescending();
            Console.WriteLine("A lista rendezve lett értékelés szerint csökkenő sorrendben.");
            break;

        case "6":
            SaveToCsvMenu(manager);
            break;

        case "7":
            LoadFromCsvMenu(manager);
            break;

        case "0":
            running = false;
            Console.WriteLine("Kilépés...");
            break;

        default:
            Console.WriteLine("Érvénytelen menüpont.");
            break;
    }
}

static void AddNewDiveSpot(DiveSpotManager manager)
{
    int id = manager.GetNextId();

    Console.Write("Név: ");
    string name = Console.ReadLine();

    Console.Write("Kategória: ");
    string category = Console.ReadLine();

    Console.Write("Leírás: ");
    string description = Console.ReadLine();

    int depth = ReadInt("Mélység (méter): ");
    int rating = ReadInt("Értékelés (1-5): ");
    bool isFavorite = ReadBool("Kedvenc? (i/n): ");

    DiveSpot newSpot = new DiveSpot(id, name, category, description, depth, rating, isFavorite);
    manager.AddDiveSpot(newSpot);

    Console.WriteLine("Búvárhely sikeresen hozzáadva.");
}

static void SearchDiveSpot(DiveSpotManager manager)
{
    Console.Write("Add meg a keresett nevet vagy névrészletet: ");
    string keyword = Console.ReadLine();

    var results = manager.SearchByName(keyword);
    manager.PrintList(results);
}

static void FilterDiveSpot(DiveSpotManager manager)
{
    Console.Write("Add meg a kategóriát: ");
    string category = Console.ReadLine();

    var filtered = manager.FilterByCategory(category);
    manager.PrintList(filtered);
}

static int ReadInt(string message)
{
    int value;
    Console.Write(message);

    while (!int.TryParse(Console.ReadLine(), out value))
    {
        Console.Write("Hibás szám, próbáld újra: ");
    }

    return value;
}

static bool ReadBool(string message)
{
    Console.Write(message);
    string input = Console.ReadLine().ToLower();

    while (input != "i" && input != "n")
    {
        Console.Write("Csak i vagy n lehet. Próbáld újra: ");
        input = Console.ReadLine().ToLower();
    }

    return input == "i";
}
static void SaveToCsvMenu(DiveSpotManager manager)
{
    Console.Write("Add meg a fájl nevét (pl. divespots.csv): ");
    string fileName = Console.ReadLine();

    manager.SaveToCsv(fileName);
    Console.WriteLine("CSV mentés kész.");
}
static void LoadFromCsvMenu(DiveSpotManager manager)
{
    Console.Write("Add meg a fájl nevét: ");
    string fileName = Console.ReadLine();

    manager.LoadFromCsv(fileName);
    Console.WriteLine("CSV betöltés kész.");
}