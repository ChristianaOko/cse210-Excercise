using System;

public class MeditationActivity : Activity
{
    private List<string> _mantras;
    
    public MeditationActivity() : base("Meditation Activity",
        "This activity will guide you through a mindful meditation session with focus words to center your thoughts.")
    {
        _mantras = new List<string>
        {
            "Peace begins with me",
            "I am present in this moment",
            "I choose calm over worry",
            "I embrace stillness",
            "My mind is clear and focused"
        };
    }
    
    public void Run()
    {
        DisplayStartingMessage();
        
        Random random = new Random();
        string mantra = _mantras[random.Next(_mantras.Count)];
        
        Console.WriteLine("Find a comfortable position and focus on your breathing.");
        Console.WriteLine("We'll use the following mantra during our meditation:");
        Console.WriteLine();
        Console.WriteLine($"--- {mantra} ---");
        Console.WriteLine();
        Console.WriteLine("Repeat this silently as you breathe.");
        ShowSpinner(5);
        
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);
        
        Console.Clear();
        while (DateTime.Now < endTime)
        {
            // Visual breathing guide using text that grows and shrinks
            Console.WriteLine($"▪️ {mantra} ▪️");
            
            // Growing text for inhale
            for (int i = 1; i <= 4; i++)
            {
                Console.WriteLine();
                string spaces = new string(' ', 10 - i * 2);
                string arrows = new string('>', i * 2);
                Console.WriteLine($"{spaces}Inhale{arrows}");
                Thread.Sleep(500);
            }
            
            // Shrinking text for exhale
            for (int i = 4; i >= 1; i--)
            {
                Console.WriteLine();
                string spaces = new string(' ', 10 - i * 2);
                string arrows = new string('<', i * 2);
                Console.WriteLine($"{spaces}Exhale{arrows}");
                Thread.Sleep(500);
            }
            
            Console.Clear();
        }
        
        DisplayEndingMessage();
    }
}
