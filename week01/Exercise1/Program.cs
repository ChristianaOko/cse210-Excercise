static void Exercise1()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.Write("Name cannot be empty. Please enter your name: ");
            name = Console.ReadLine();
        }

        Console.WriteLine($"Hello {name}, welcome to C# programming!");
    }
