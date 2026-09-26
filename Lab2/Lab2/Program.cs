using Lab2;
using System;


struct Command
{
    public char command;
    public int? argument;

    public Command(char c, int turn)
    {
        command = c;
        argument = turn;
    }
    public Command(string[] ar)
    {
        command = ar[0][0];
        argument = ar.Length == 2 ? Convert.ToInt32(ar[1]) : null;
    }
}
class Program
{
    static (List<Command>, int start) ReadFile(string path)
    {
        var f = File.ReadLines(path);
        int s = Convert.ToInt32(f.First());
        List<Command> commands = new List<Command>();
        foreach( string c in f.Skip(1))
        {
            string[] raw = c.Split('\t');
            commands.Add(new Command(raw));

        }
        return (commands, s);
    }

    static void Execute(string path, List<Command> commands, CatMouseGame game)
    {
        File.WriteAllText(path, "Cat Mouse Game\n");
        File.AppendAllText(path, "\nCat Mouse Distance");
        File.AppendAllText(path,new string('-',15));
        foreach(Command c in commands)
        {
            switch (c.command) 
            {
                case 'C':
                    game.iterate(Mover.Cat, c.argument ?? 0);
                    break;
                case 'M':
                    game.iterate(Mover.Mouse, c.argument ?? 0);
                    break;
                case 'P':
                    File.AppendAllText(path, game.State());
                    break;
            }
        }
        File.AppendAllText(path, new string('-', 15));
        File.AppendAllText(path, game.FinalState());
    }
    

    void Main()
    {

    }
}
