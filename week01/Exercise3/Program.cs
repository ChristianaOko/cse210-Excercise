using System;

class GuessMyNumber
{
    static void Main()
    {
        Random random = new Random(); // Create a random number generator
        int magicNumber = random.Next(1, 101); // Random number between 1 and 100
        int userGuess = 0;

        // Initial prompt to the user
        Console.WriteLine("Welcome to Guess My Number!");
        
        // Loop to keep guessing until the correct number is found
        while (userGuess != magicNumber)
        {
            // Ask the user for their guess
            Console.Write("What is your guess? ");
            userGuess = int.Parse(Console.ReadLine());

            // Check if the guess is correct, higher, or lower
            if (userGuess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (userGuess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
            else
            {
                Console.WriteLine("You guessed it!");
            }
        }
    }
}
