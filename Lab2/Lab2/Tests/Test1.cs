namespace Lab2;

[TestClass]
public class Test1
{
    [TestMethod]
    public void MoveTest()
    {
        CatMouseGame game = new CatMouseGame(10);
        game.move(new Command('M', 1));
        game.move(new Command('C', 5));
        game.move(new Command('M', 31));
        Assert.AreEqual(2, game.getMousePos());

    }
    [TestMethod]
    public void Distance()
    {
        Player p = new Player("Player");
        p.Move(10, 5);
        p.Move(10, 11);
        p.Move(10, 12);
        Assert.AreEqual(23, p.Distance);
    }
}
