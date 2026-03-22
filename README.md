# 🌊 Búvárhelyek adatkezelő alkalmazás

## Projekt leírása
Ez a projekt egy C# konzolos alkalmazás, amely búvárhelyek adatainak kezelésére szolgál.  
A program lehetővé teszi különböző búvárhelyek nyilvántartását, keresését, szűrését és rendezését.  
A projekt későbbi részeként a tárolt adatokból automatikusan generált HTML oldalak is készülnek.

## Csapattagok
- Mercz Péter
- Fekete Balázs

## Választott téma
Búvárhelyek

## Adatmodell rövid leírása
A program fő adatosztálya a `DiveSpot`, amely egy búvárhely adatait tárolja.

A főbb mezők:
- `Id` – egyedi azonosító
- `Name` – a búvárhely neve
- `Category` – kategória
- `Description` – rövid leírás
- `Depth` – mélység méterben
- `Rating` – értékelés
- `IsFavorite` – kedvenc-e

Az adatok kezelését a `DiveSpotManager` osztály végzi, amely listában tárolja az objektumokat.

## A program funkciói
Jelenleg megvalósított funkciók:
- új búvárhely hozzáadása
- búvárhelyek listázása
- keresés név alapján
- szűrés kategória szerint
- rendezés értékelés alapján

Tervezett további funkciók:
- CSV mentés
- CSV betöltés
- HTML export

## A generált oldalak rövid bemutatása
A program a későbbiekben az alábbi HTML oldalakat fogja generálni:

- `index.html` – főoldal, projektleírás és statisztikák
- `items.html` – az összes búvárhely listája
- `favorites.html` – a kedvenc búvárhelyek külön oldala

## Képek
A képek később kerülnek hozzáadásra a projekthez.