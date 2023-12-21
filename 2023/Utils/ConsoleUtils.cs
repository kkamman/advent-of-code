namespace Utils;

public static class ConsoleUtils
{
    public static void RunProgram(string title, Func<string, string> program)
    {
        Console.WriteLine(title + Environment.NewLine);

        while (true)
        {
            Console.WriteLine("Result: {0}", program(QueryInputFilePath()));

            Console.Write(Environment.NewLine + "Press any key to run again, or press ESC to exit.");

            if (Console.ReadKey(intercept: true).Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

            Console.WriteLine(Environment.NewLine);
        }
    }

    public static string QueryInputFilePath(string defaultPath = "input.txt")
    {
        Console.Write("Input filepath: ({0}) ", defaultPath);
        var inputFilePath = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputFilePath))
        {
            inputFilePath = defaultPath;
        }

        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine("'{0}' is not a valid filepath.", inputFilePath);
            return QueryInputFilePath();
        }

        return inputFilePath;
    }

    public static bool QueryBoolean(string query, bool defaultValue = true)
    {
        var options = defaultValue ? "[(y)/n]" : "[y/(n)]";
        Console.Write("{0} {1} ", query, options);
        var answer = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(answer))
        {
            return defaultValue;
        }

        if (answer == "y")
        {
            return true;
        }

        if (answer == "n")
        {
            return false;
        }

        return QueryBoolean(query);
    }
}
