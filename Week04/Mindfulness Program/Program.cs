using System;

class Program
{
    static void Main(string[] args)
    {
        // This adds the creative component of tracking activity usage
        Dictionary<string, int> activityLog = new Dictionary<string, int>()
        {
            { "Breathing Activity", 0 },
            { "Reflection Activity", 0 },
            { "Listing Activity", 0 },
            { "Meditation Activity", 0 }  // Extra activity
        };
        
        bool quit = false;
        
        while (!quit)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Program");
            Console.WriteLine("===================");
            Console.WriteLine("1. Start breathing activity");
            Console.WriteLine("2. Start reflection activity");
            Console.WriteLine("3. Start listing activity");
            Console.WriteLine("4. Start meditation activity");  // Extra activity
            Console.WriteLine("5. View activity usage");        // Extra feature
            Console.WriteLine("6. Quit");
            Console.WriteLine();
            Console.Write("Please select a choice from the menu: ");
            
            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    activityLog["Breathing Activity"]++;
                    break;
                    
                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    activityLog["Reflection Activity"]++;
                    break;
                    
                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    activityLog["Listing Activity"]++;
                    break;
                    
                case "4":
                    MeditationActivity meditation = new MeditationActivity();
                    meditation.Run();
                    activityLog["Meditation Activity"]++;
                    break;
                    
                case "5":
                    DisplayActivityLog(activityLog);
                    break;
                    
                case "6":
                    quit = true;
                    break;
                    
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Thread.Sleep(2000);
                    break;
            }
        }
    }
    
    static void DisplayActivityLog(Dictionary<string, int> log)
    {
        Console.Clear();
        Console.WriteLine("Activity Usage Log");
        Console.WriteLine("=================");
        
        foreach (var activity in log)
        {
            Console.WriteLine($"{activity.Key}: {activity.Value} times");
        }
        
        Console.WriteLine("\nPress enter to continue...");
        Console.ReadLine();
    }
}
