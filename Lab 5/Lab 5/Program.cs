using System.Text;
using Lab_5.Enums;
using Lab_5.Gift;
using Lab_5.Models;

Console.OutputEncoding = Encoding.UTF8;

var gift = new NewYearGift("Детский новогодний подарок 'Сказка'");

var candies = new Sweet[]
{
    new ChocolateCandy("Аленка", 15.0, 52.0, "Красный Октябрь", ChocolateType.Milk, 35.0),
    new ChocolateCandy("Вдохновение", 18.0, 42.0, "Бабаевский", ChocolateType.Dark, 72.0, true, "Дробленый фундук"),
    new ChocolateCandy("Белый мишка", 14.5, 58.0, "Рот Фронт", ChocolateType.White, 25.0, true, "Вафельная крошка"),
    new Lollipop("Чупа-Чупс", 12.0, 85.0, "Perfetti Van Melle", "Клубника со сливками", true),
    new Lollipop("Мятный леденец", 8.0, 78.0, "Рот Фронт", "Перечная мята", false),
    new JellyCandy("Бешеные пчелки", 9.5, 62.0, "Roshen", "Каррагинан", false),
    new JellyCandy("Кислые червячки", 11.0, 68.0, "Trolli", "Желатин", true),
    new WaffleCandy("Мишка косолапый", 30.0, 48.0, "Красный Октябрь", 4, true),
    new WaffleCandy("Красная шапочка", 28.0, 51.0, "Красный Октябрь", 3, true)
};

gift.AddRange(candies);

Console.WriteLine(gift);

Console.WriteLine("--------------------------------------------------------------------------------");
Console.WriteLine($"ИТОГОВЫЙ ВЕС ПОДАРКА: {gift.GetTotalWeight():F1} г (Сахар: {gift.GetTotalSugarWeight():F1} г)");
Console.WriteLine("--------------------------------------------------------------------------------\n");

Console.WriteLine("=== Сортировка по возрастанию содержания сахара ===");
gift.SortBySugarPercentage();
foreach (var sweet in gift)
{
    Console.WriteLine($"{sweet.SugarPercentage,5:F1}% сахара | {sweet.Name} ({sweet.Manufacturer}) - {sweet.Weight:F1} г");
}

Console.WriteLine("\n=== Сортировка по возрастанию веса конфет ===");
gift.SortByWeight();
foreach (var sweet in gift)
{
    Console.WriteLine($"{sweet.Weight,5:F1} г | {sweet.Name} ({sweet.Manufacturer}) - {sweet.SugarPercentage:F1}% сахара");
}

const double minSugar = 45.0;
const double maxSugar = 60.0;
Console.WriteLine($"\n=== Поиск конфет с содержанием сахара от {minSugar}% до {maxSugar}% ===");
var foundSweets = gift.FindBySugarRange(minSugar, maxSugar).ToList();

if (foundSweets.Count > 0)
{
    foreach (var sweet in foundSweets)
    {
        Console.WriteLine($"[Найдено] {sweet}");
    }
}
else
{
    Console.WriteLine("Конфеты с заданными параметрами не найдены.");
}
