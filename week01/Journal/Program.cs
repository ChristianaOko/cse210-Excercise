using System;

public class Program
{
    private static Journal journal = new Journal();
    private static Prompt promptGenerator = new Prompt();

    public static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournalToFile();
                    break;
                case "4":
                    LoadJournalFromFile();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Journal Program");
        Console.WriteLine("1. Write a New Entry");
        Console.WriteLine("2. Display the Journal");
        Console.WriteLine("3. Save Journal to File");
        Console.WriteLine("4. Load Journal from File");
        Console.WriteLine("5. Exit");
        Console.Write("Choose an option: ");
    }

    private static void WriteNewEntry()
    {
        string prompt = promptGenerator.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        journal.AddEntry(new Entry(prompt, response, date));
        Console.WriteLine("Entry added successfully!");
        Console.ReadLine(); // Wait for the user to read the message
    }

    private static void DisplayJournal()
    {
        journal.DisplayEntries();
        Console.WriteLine("Press Enter to return to the menu.");
        Console.ReadLine();
    }

    private static void SaveJournalToFile()
    {
        Console.Write("Enter filename to save journal: ");
        string filename = Console.ReadLine();
        journal.SaveToFile(filename);
        Console.WriteLine("Journal saved successfully!");
        Console.ReadLine(); // Wait for the user to read the message
    }

    private static void LoadJournalFromFile()
    {
        Console.Write("Enter filename to load journal: ");
        string filename = Console.ReadLine();
        if (File.Exists(filename))
        {
            journal.LoadFromFile(filename);
            Console.WriteLine("Journal loaded successfully!");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
        Console.ReadLine(); // Wait for the user to read the message
    }
}

