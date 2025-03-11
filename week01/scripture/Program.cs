using System;
using System.Collections.Generic;

namespace ScriptureMemorization
{
    // Class to represent a word in the scripture
    public class Word
    {
        public string Text { get; set; }
        public bool Hidden { get; set; }

        public Word(string text)
        {
            Text = text;
            Hidden = false;
        }

        // Hide the word by setting Hidden to true
        public void Hide()
        {
            Hidden = true;
        }

        // Return the word or underscore if hidden
        public override string ToString()
        {
            return Hidden ? new string('_', Text.Length) : Text;
        }
    }

    // Class to represent the reference of the scripture (e.g., John 3:16)
    public class Reference
    {
        public string Text { get; set; }

        public Reference(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }

    // Class to represent the scripture itself
    public class Scripture
    {
        public Reference ScriptureReference { get; set; }
        public List<Word> Words { get; set; }

        public Scripture(string reference, string text)
        {
            ScriptureReference = new Reference(reference);
            Words = new List<Word>();

            foreach (var word in text.Split(' '))
            {
                Words.Add(new Word(word));
            }
        }

        // Hide a random word from the scripture
        public void HideRandomWord()
        {
            var unhiddenWords = Words.FindAll(word => !word.Hidden);
            if (unhiddenWords.Count > 0)
            {
                Random random = new Random();
                var wordToHide = unhiddenWords[random.Next(unhiddenWords.Count)];
                wordToHide.Hide();
            }
        }

        // Display the scripture with hidden words shown as underscores
        public string Display()
        {
            var result = $"{ScriptureReference}\n";
            foreach (var word in Words)
            {
                result += word.ToString() + " ";
            }
            return result.Trim();
        }

        // Check if all words are hidden
        public bool IsFullyHidden()
        {
            return Words.TrueForAll(word => word.Hidden);
        }
    }

    // Main program class
    public class Program
    {
        private Scripture _scripture;

        // Method to load a scripture
        public void LoadScripture()
        {
            string reference = "John 3:16";
            string text = "For God so loved the world that he gave his only begotten Son";
            _scripture = new Scripture(reference, text);
        }

        // Method to clear the console screen
        public void ClearScreen()
        {
            Console.Clear();
        }

        // Main method to run the program
        public void Run()
        {
            LoadScripture();

            while (!_scripture.IsFullyHidden())
            {
                ClearScreen();
                Console.WriteLine(_scripture.Display());

                Console.Write("Press Enter to hide a word, or type 'quit' to exit: ");
                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (userInput == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (string.IsNullOrEmpty(userInput))
                {
                    _scripture.HideRandomWord();
                }
            }

            // Final display with all words hidden
            ClearScreen();
            Console.WriteLine(_scripture.Display());
            Console.WriteLine("Scripture completely hidden! Memorization complete.");
        }

        // Main entry point of the program
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
        }
    }
}
