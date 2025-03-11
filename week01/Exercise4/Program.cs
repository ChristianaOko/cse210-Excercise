using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        int input;

        // Collect numbers until 0 is entered
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        do
        {
            Console.Write("Enter number: ");
            input = int.Parse(Console.ReadLine());

            if (input != 0)
            {
                numbers.Add(input);
            }

        } while (input != 0);

        // Check if the list is empty
        if (numbers.Count > 0)
        {
            // Calculate the sum
            int sum = 0;
            foreach (int number in numbers)
            {
                sum += number;
            }

            // Calculate the average
            double average = (double)sum / numbers.Count;

            // Find the largest number
            int largestNumber = numbers[0];
            foreach (int number in numbers)
            {
                if (number > largestNumber)
                {
                    largestNumber = number;
                }
            }

            // Display the results
            Console.WriteLine("The sum is: " + sum);
            Console.WriteLine("The average is: " + average);
            Console.WriteLine("The largest number is: " + largestNumber);
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}
