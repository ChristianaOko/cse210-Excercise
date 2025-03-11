using System;

class Program
{
    // Function to display a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function to prompt the user for their name and return it as a string
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // Function to prompt the user for their favorite number and return it as an integer
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int favoriteNumber = int.Parse(Console.ReadLine());
        return favoriteNumber;
    }

    // Function to square the number and return the result
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function to display the result with the user's name and squared number
    static void DisplayResult(string name, int squaredNumber)
    {
        Console.WriteLine($"{name}, the square of your number is {squaredNumber}");
    }

    // Main function that calls all other functions and ties everything together
    static void Main()
    {
        // Call DisplayWelcome to show the message
        DisplayWelcome();

        // Get the user's name and favorite number
        string userName = PromptUserName();
        int favoriteNumber = PromptUserNumber();

        // Square the favorite number
        int squaredNumber = SquareNumber(favoriteNumber);

        // Display the result
        DisplayResult(userName, squaredNumber);
    }
}
