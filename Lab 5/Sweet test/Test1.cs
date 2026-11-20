using Lab_5.Enums;
using Lab_5.Gift;
using Lab_5.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Sweet_test;

[TestClass]
public sealed class Test1
{
    [TestMethod]
    public void GetTotalWeight_CalculatesCorrectSum()
    {
        var gift = new NewYearGift("Тестовый подарок");
        gift.Add(new ChocolateCandy("Аленка", 15.0, 50.0, "Фабрика 1", ChocolateType.Milk, 35.0));
        gift.Add(new Lollipop("Леденец", 10.0, 80.0, "Фабрика 2", "Мята", false));
        gift.Add(new WaffleCandy("Вафля", 25.0, 40.0, "Фабрика 3", 3, true));

        var actualWeight = gift.GetTotalWeight();

        Assert.AreEqual(50.0, actualWeight, 0.001);
    }

    [TestMethod]
    public void GetTotalSugarWeight_CalculatesCorrectTotalSugar()
    {
        var gift = new NewYearGift("Тестовый подарок");
        gift.Add(new ChocolateCandy("Конфета 1", 20.0, 50.0, "Фабрика 1", ChocolateType.Dark, 70.0));
        gift.Add(new JellyCandy("Мармелад", 10.0, 60.0, "Фабрика 2", "Желатин", false));

        var actualSugarWeight = gift.GetTotalSugarWeight();

        Assert.AreEqual(16.0, actualSugarWeight, 0.001);
    }

    [TestMethod]
    public void SortByWeight_SortsCandiesAscendingByWeight()
    {
        var gift = new NewYearGift("Тестовый подарок");
        var heavy = new WaffleCandy("Тяжелая", 30.0, 40.0, "Фабрика 1", 4, true);
        var light = new Lollipop("Легкая", 5.0, 90.0, "Фабрика 2", "Вишня", true);
        var medium = new ChocolateCandy("Средняя", 15.0, 50.0, "Фабрика 3", ChocolateType.Milk, 30.0);

        gift.AddRange([heavy, light, medium]);
        gift.SortByWeight();

        CollectionAssert.AreEqual(new List<Sweet> { light, medium, heavy }, gift.Sweets.ToList());
    }

    [TestMethod]
    public void SortBySugarPercentage_SortsCandiesAscendingBySugar()
    {
        var gift = new NewYearGift("Тестовый подарок");
        var highSugar = new Lollipop("Сладкий", 10.0, 85.0, "Фабрика 1", "Лимон", false);
        var lowSugar = new ChocolateCandy("Горький", 20.0, 30.0, "Фабрика 2", ChocolateType.Dark, 85.0);
        var midSugar = new JellyCandy("Средний", 15.0, 55.0, "Фабрика 3", "Пектин", true);

        gift.AddRange([highSugar, lowSugar, midSugar]);
        gift.SortBySugarPercentage();

        CollectionAssert.AreEqual(new List<Sweet> { lowSugar, midSugar, highSugar }, gift.Sweets.ToList());
    }

    [TestMethod]
    public void FindBySugarRange_ReturnsCandiesWithinSpecifiedRange()
    {
        var gift = new NewYearGift("Тестовый подарок");
        var sweet1 = new ChocolateCandy("Шоколад", 20.0, 35.0, "Фабрика 1", ChocolateType.Dark, 80.0);
        var sweet2 = new JellyCandy("Желе", 15.0, 50.0, "Фабрика 2", "Агар", false);
        var sweet3 = new WaffleCandy("Вафля", 25.0, 55.0, "Фабрика 3", 3, true);
        var sweet4 = new Lollipop("Карамель", 10.0, 80.0, "Фабрика 4", "Клубника", true);

        gift.AddRange([sweet1, sweet2, sweet3, sweet4]);

        var found = gift.FindBySugarRange(40.0, 60.0).ToList();

        CollectionAssert.AreEquivalent(new List<Sweet> { sweet2, sweet3 }, found);
    }
}
