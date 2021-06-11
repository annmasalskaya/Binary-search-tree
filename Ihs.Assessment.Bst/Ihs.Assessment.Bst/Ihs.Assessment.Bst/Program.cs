using System;
using System.Text.RegularExpressions;

namespace Ihs.Assessment.Bst
{
    public class Program
    {
        public static Regex AddCommandRegex = new Regex(@"i\s([0-9]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        public static Regex RemoveCommandRegex = new Regex(@"d\s([0-9]+)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static BinarySearchTree<int> _tree = new();

        public static void Main(string[] args)
        {
            Console.WriteLine("Use -help to see available commands or start use BST");

            while (true)
            {
                var input = Console.ReadLine();
                Console.WriteLine(HandleConsoleInput(input));
            }
        }

        private static string HandleConsoleInput(string input)
            => input switch
            {
                "-help" => "i XX: Add a node with a value XX \r\nd YY: Remove a node with a value YY\r\nh h: Output height of the tree",
                "h" => $"Height {_tree.GetHeight()}" ,
                _ => ParseInputAndPerfromTreeModification(input),
            };

        private static string ParseInputAndPerfromTreeModification(string input)
        {
            if (AddCommandRegex.IsMatch(input))
            {
                _tree.Add(ParseInput(input, AddCommandRegex));
                
                return "The value has been added";
            }
            else
            {
                if (RemoveCommandRegex.IsMatch(input))
                {
                    if (_tree.Remove(ParseInput(input, RemoveCommandRegex)))
                    {
                        return "The value has been removed";
                    }
                    else
                    {
                        return "No such value in the tree";
                    }
                }
                else
                {
                    return "Not supported command or argument type, please use '-help'";
                }
            }
        }

        private static int ParseInput(string input, Regex regex)
        {
            var match = regex.Match(input);
            var item = match.Groups[1].Value;

            return int.Parse(item);
        }
    }
}
