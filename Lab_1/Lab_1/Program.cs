using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticSearch
{
    class Program
    {
        struct Protein
        {
            public string name; // protein name
            public string organism; //organism name
            public string amino_acids; // sequence of amino_asids
        }

        struct Command
        {
            public string name;
            public string parameter1;
            public string parameter2;
        }

        static List<Command> ReadCommands(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            List<Command> commands = new List<Command>();

            Command command;
            command.name = String.Empty;
            command.parameter1 = String.Empty;
            command.parameter2 = String.Empty;

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('\t');

                if (parts.Length == 2)
                {
                    command.name = parts[0];
                    command.parameter1 = parts[1];
                    command.parameter2 = String.Empty;
                }
                else
                {
                    command.name = parts[0];
                    command.parameter1 = parts[1];
                    command.parameter2 = parts[2];
                }
                commands.Add(command);
            }
            return commands;
        }

        static List<Protein> ReadData(string filename)
        {
            //reader object to read data from file
            StreamReader reader = new StreamReader(filename);

            // empty list to keep data about proteins
            List<Protein> data = new List<Protein>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] parts = line.Split('\t');
                Protein protein;
                protein.name = parts[0];
                protein.organism = parts[1];
                protein.amino_acids = parts[2];
                data.Add(protein);
            }
            return data;
        }

        static string Encoding(string amino_acids)
        {
            string encoded = String.Empty;
            for (int i = 0; i < amino_acids.Length; i++)
            {
                char ch = amino_acids[i];
                int count = 1;
                while (i < amino_acids.Length - 1 && amino_acids[i + 1] == ch)
                {
                    count++;
                    i++;
                }
                if (count > 2) encoded = encoded + count + ch;
                if (count == 1) encoded = encoded + ch;
                if (count == 2) encoded = encoded + ch + ch;
            }
            return encoded;
        }

        static string Decoding(string amino_acids)
        {
            string decoded = String.Empty;
            for (int i = 0; i < amino_acids.Length; i++)
            {   // 8ATA3TCGC4T....
                char ch = amino_acids[i];
                if (char.IsDigit(ch))  // '8' -> int 8
                {
                    char letter = amino_acids[i + 1];
                    int count = ch - '0'; // '8' - '0' = 8
                    for (int j = 1; j < count; j++)
                        decoded = decoded + letter;
                }
                else decoded = decoded + ch;
            }
            return decoded;
        }

        static void PrintData(List<Protein> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine("Protein " + (i + 1));
                Console.WriteLine(data[i].name);
                Console.WriteLine(data[i].organism);
                Console.WriteLine(data[i].amino_acids);
                Console.WriteLine("========================");
            }
        }

        static void PrintCommands(List<Command> commands)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                Console.WriteLine("Command " + (i + 1));
                Console.WriteLine(commands[i].name);
                Console.WriteLine(commands[i].parameter1);
                Console.WriteLine(commands[i].parameter2);
                Console.WriteLine("========================");
            }
        }

        static void CommandHandler(List<Protein> proteins, List<Command> commands, string path)
        {
            int comm_count = 0;
            StringBuilder b = new StringBuilder();
            int width = Console.WindowWidth;
            for (int i = 0; i < commands.Count; i++)
            {
                comm_count++;
                b.Append(new string('-' , width)+'\n');

                if (commands[i].name == "search")
                {
                    b.Append($"{comm_count:D3}" + "\t" + commands[i].name + "\t" + commands[i].parameter1 + '\n');
                    b.Append($"{"organism",-25}" + $"{"protein",-25}" + '\n');
                    bool is_empt = true;
                    foreach (Protein p in proteins)
                        if (Encoding(p.amino_acids).Contains(commands[i].parameter1))
                        {
                            b.Append($"{p.organism,-25}" + $"{p.name,-25}" + '\n');
                            is_empt = false;
                        }
                    if (is_empt == true) b.Append("NOT FOUND\n");
                }


                else if (commands[i].name == "diff")
                {
                    b.Append($"{comm_count:D3}" + "\t" + commands[i].name + "\t" + commands[i].parameter1 + '\t'+ commands[i].parameter2+'\n');
                    b.Append("Amino-acids difference: \n");
                    Protein first = proteins.FirstOrDefault(pr => pr.name == commands[i].parameter1);
                    Protein second = proteins.FirstOrDefault(pr => pr.name == commands[i].parameter2);
                    if ( (!string.IsNullOrEmpty(first.name) && !string.IsNullOrEmpty(second.name)))
                    {
                        int r = 0;
                        r += Math.Abs(first.amino_acids.Length - second.amino_acids.Length);

                        int min = first.amino_acids.Length <= second.amino_acids.Length ? first.amino_acids.Length : second.amino_acids.Length;
                        for (int j = 0; j < min; j++) if (first.amino_acids[j] != second.amino_acids[j]) r++;
                        b.Append(r);
                    }
                    else if (string.IsNullOrEmpty(first.name))
                    { b.Append($"MISSING: {commands[i].parameter1}"); }

                    else if (string.IsNullOrEmpty(second.name))
                    { b.Append($"MISSING: {commands[i].parameter2}"); }

                    else
                    {
                        b.Append($"MISSING: {commands[i].parameter1} {commands[i].parameter2}");
                    }
                    b.Append('\n');
                }
                else if (commands[i].name == "mode")
                {
                    b.Append($"{comm_count:D3}" + "\t" + commands[i].name + "\t" + commands[i].parameter1 + '\n');
                    b.Append("amino-acid occurs:\n");
                    Protein p = proteins.FirstOrDefault(pr => pr.name == commands[i].parameter1);
                    if (!string.IsNullOrEmpty(p.name))
                    { char l = 'V';
                        int max = 0;
                        HashSet<char> letters = new HashSet<char>(p.amino_acids.ToCharArray());
                        foreach (char letter in letters)
                        {
                            if (p.amino_acids.Count(letter) > max)
                            {
                                l = letter;
                                max = p.amino_acids.Count(letter);
                            }
                        }
                        b.Append($"{l}\t\t{max}\n"); }
                    else b.Append($"MISSING: {commands[i].parameter1}");


                }
                b.Append(new string('-', width));
                b.Append('\n');
            }
            File.WriteAllText(path, b.ToString());
            b.Clear();
        }

        static void Main(string[] args)
        {
            for(int i = 0; i < 3; i++)
            {
                List<Protein> data = ReadData(@$"D:\C#\Labs\Lab_1\Lab_1\input\proteins\sequences.{i}.txt");
                List<Command> commands = ReadCommands(@$"D:\C#\Labs\Lab_1\Lab_1\input\commands\commands.{i}.txt");
                CommandHandler(data, commands, @$"D:\C#\Labs\Lab_1\Lab_1\output\gendata{i}.txt");
            }
        }
    }
}