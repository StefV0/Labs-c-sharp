using Lab2;
using System;
using System.Text;

class Program
{
    static (List<Command> commands, int start) ReadFile(string path)
    {
        var f = File.ReadLines(path);
        int s = Convert.ToInt32(f.First());
        List<Command> commands = new List<Command>();
        foreach( string c in f.Skip(1))
        {
            string[] raw = c.Split(' ');
            commands.Add(new Command(raw));

        }
        return (commands, s);
    }

    static void Execute(string path, List<Command> commands, CatMouseGame game)
    {
        StringBuilder b = game.RunGame(commands);
        File.WriteAllText(path, b.ToString());
    }
    

    static void Main(string[] args)
    {
        for (int i = 1; i < 4; i++)
        {
            var tupple = ReadFile(@$"D:\C#\Labs\Lab2\Lab2\Tests\{i}.ChaseData.txt");
            CatMouseGame game = new CatMouseGame(tupple.start);
            Execute($@"D:\C#\Labs\Lab2\Lab2\Results\{i}.PursuitLog.txt",tupple.commands,game);
            
        }
    }
}
