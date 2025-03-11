using System;

class Program
{
    static void Main()
    {
        // Ask the user for their grade percentage
        Console.Write("Enter your grade percentage: ");
        int grade = int.Parse(Console.ReadLine());

        // Declare a variable for the letter grade
        string letter = "";

        // Determine the letter grade based on the grade percentage
        if (grade >= 90)
        {
            letter = "A";
        }
        else if (grade >= 80)
        {
            letter = "B";
        }
        else if (grade >= 70)
        {
            letter = "C";
        }
        else if (grade >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // Output the letter grade
        Console.WriteLine("Your letter grade is: " + letter);

        // Determine if the user passed the course
        if (grade >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Sorry, you didn't pass this time. Keep trying, you can do it!");
        }
    }
}
