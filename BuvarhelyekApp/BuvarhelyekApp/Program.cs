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
    Console.WriteLine("8 - HTML export");
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
        case "8":
            ExportHtml(manager);
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

string GenerateTableRows(List<DiveSpot> list)
{
    string rows = "";

    foreach (var s in list)
    {
        rows += $@"
        <tr>
            <td>{s.Name}</td>
            <td>{s.Category}</td>
            <td>{s.Description}</td>
            <td>{s.Depth} m</td>
            <td>{s.Rating}</td>
        </tr>";
    }

    return rows;
}

string GenerateCards(List<DiveSpot> list, bool onlyFavorites = false)
{
    string cards = "";

    foreach (var s in list)
    {
        if (onlyFavorites && !s.IsFavorite) continue;

        string favClass = s.IsFavorite ? "card favorite" : "card";

        cards += $@"
        <div class='{favClass}'>
            <h3>{s.Name}</h3>
            <p>{s.Description}</p>
            <p>Kategória: {s.Category}</p>
            <p>Mélység: {s.Depth} m</p>
            <p>⭐ {s.Rating}</p>
        </div>";
    }

    return cards;
}

void ExportHtml(DiveSpotManager manager)
{
    string template = File.ReadAllText("template/template.html");

    // INDEX
    string indexContent = $@"
    <p>Összes búvárhely: {manager.DiveSpots.Count}</p>
    <p>Kategóriák száma: {manager.DiveSpots.Select(x => x.Category).Distinct().Count()}</p>

    <div class='card-container'>
        {GenerateCards(manager.DiveSpots.Take(3).ToList())}
    </div>";

    string indexHtml = template
        .Replace("{{TITLE}}", "Főoldal")
        .Replace("{{HEADER}}", "Üdvözöllek")
        .Replace("{{DESCRIPTION}}", "Fedezd fel a legjobb búvárhelyeket!")
        .Replace("{{CONTENT}}", indexContent);

    File.WriteAllText("index.html", indexHtml);

    // ITEMS
    string itemsContent = $@"
    <table>
        <tr>
            <th>Név</th>
            <th>Kategória</th>
            <th>Leírás</th>
            <th>Mélység</th>
            <th>Értékelés</th>
        </tr>
        {GenerateTableRows(manager.DiveSpots)}
    </table>";

    string itemsHtml = template
        .Replace("{{TITLE}}", "Összes hely")
        .Replace("{{HEADER}}", "Búvárhelyek listája")
        .Replace("{{DESCRIPTION}}", "Az összes rögzített búvárhely")
        .Replace("{{CONTENT}}", itemsContent);

    File.WriteAllText("items.html", itemsHtml);

    // FAVORITES
    string favContent = $@"
    <div class='card-container'>
        {GenerateCards(manager.DiveSpots, true)}
    </div>";

    string favHtml = template
        .Replace("{{TITLE}}", "Kedvencek")
        .Replace("{{HEADER}}", "Kedvenc búvárhelyek")
        .Replace("{{DESCRIPTION}}", "A legjobb helyek gyűjteménye")
        .Replace("{{CONTENT}}", favContent);

    File.WriteAllText("favorites.html", favHtml);

    Console.WriteLine("HTML export kész!");
}