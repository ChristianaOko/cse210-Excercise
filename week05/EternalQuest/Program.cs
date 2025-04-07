using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace EternalQuest
{
    // Abstract base class for all types of goals
    public abstract class Goal
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Points { get; protected set; }
        public bool IsComplete { get; protected set; }

        public Goal(string name, string description, int points)
        {
            Name = name;
            Description = description;
            Points = points;
            IsComplete = false;
        }

        // Abstract method to be implemented by derived classes
        public abstract int RecordEvent();

        // Virtual method that can be overridden by derived classes
        public virtual string GetDetailsString()
        {
            string status = IsComplete ? "[X]" : "[ ]";
            return $"{status} {Name} ({Description})";
        }

        // For saving/loading purposes
        public virtual string GetStringRepresentation()
        {
            return $"{GetType().Name}:{Name},{Description},{Points},{IsComplete}";
        }
    }

    // Class for simple goals that can be completed once
    public class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points) 
            : base(name, description, points)
        {
        }

        // Constructor for loading from file
        public SimpleGoal(string name, string description, int points, bool isComplete)
            : base(name, description, points)
        {
            IsComplete = isComplete;
        }

        public override int RecordEvent()
        {
            if (!IsComplete)
            {
                IsComplete = true;
                return Points;
            }
            return 0; // No points if already completed
        }
    }

    // Class for eternal goals that are never complete
    public class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override int RecordEvent()
        {
            // Eternal goals always award points but are never complete
            return Points;
        }

        public override string GetDetailsString()
        {
            // Eternal goals are never complete so we use a different symbol
            return $"[∞] {Name} ({Description})";
        }
    }

    // Class for checklist goals that must be completed a certain number of times
    public class ChecklistGoal : Goal
    {
        public int Target { get; private set; }
        public int CompletedCount { get; private set; }
        public int Bonus { get; private set; }

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            Target = target;
            Bonus = bonus;
            CompletedCount = 0;
        }

        // Constructor for loading from file
        public ChecklistGoal(string name, string description, int points, bool isComplete, 
                             int target, int completedCount, int bonus)
            : base(name, description, points)
        {
            Target = target;
            CompletedCount = completedCount;
            Bonus = bonus;
            IsComplete = isComplete;
        }

        public override int RecordEvent()
        {
            if (!IsComplete)
            {
                CompletedCount++;
                
                // Check if the goal is now complete
                if (CompletedCount >= Target)
                {
                    IsComplete = true;
                    return Points + Bonus; // Award regular points plus bonus
                }
                
                return Points; // Award regular points
            }
            return 0; // No points if already completed
        }

        public override string GetDetailsString()
        {
            string status = IsComplete ? "[X]" : "[ ]";
            return $"{status} {Name} ({Description}) -- Currently completed: {CompletedCount}/{Target}";
        }

        public override string GetStringRepresentation()
        {
            return $"{GetType().Name}:{Name},{Description},{Points},{IsComplete},{Target},{CompletedCount},{Bonus}";
        }
    }

    // Class to manage the user's goals and score
    public class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public int Score => _score;

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public void RecordEvent(int index)
        {
            if (index >= 0 && index < _goals.Count)
            {
                int pointsEarned = _goals[index].RecordEvent();
                _score += pointsEarned;
                
                Console.WriteLine($"Congratulations! You earned {pointsEarned} points!");
                Console.WriteLine($"You now have {_score} points.");
            }
        }

        public void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("You don't have any goals yet.");
                return;
            }

            Console.WriteLine("Your Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        public List<Goal> GetGoals()
        {
            return _goals;
        }

        public void SaveGoals(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                // First line is the score
                writer.WriteLine(_score);
                
                // Remaining lines are the goals
                foreach (Goal goal in _goals)
                {
                    writer.WriteLine(goal.GetStringRepresentation());
                }
            }
        }

        public void LoadGoals(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File does not exist.");
                return;
            }

            _goals.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                // First line is the score
                string scoreLine = reader.ReadLine();
                _score = int.Parse(scoreLine);
                
                // Read the rest of the file
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] parts = line.Split(':', 2);
                    
                    if (parts.Length < 2) continue;
                    
                    string goalType = parts[0];
                    string[] data = parts[1].Split(',');
                    
                    switch (goalType)
                    {
                        case "SimpleGoal":
                            _goals.Add(new SimpleGoal(
                                data[0], 
                                data[1], 
                                int.Parse(data[2]), 
                                bool.Parse(data[3])
                            ));
                            break;
                        case "EternalGoal":
                            _goals.Add(new EternalGoal(
                                data[0], 
                                data[1], 
                                int.Parse(data[2])
                            ));
                            break;
                        case "ChecklistGoal":
                            _goals.Add(new ChecklistGoal(
                                data[0], 
                                data[1], 
                                int.Parse(data[2]), 
                                bool.Parse(data[3]), 
                                int.Parse(data[4]), 
                                int.Parse(data[5]), 
                                int.Parse(data[6])
                            ));
                            break;
                    }
                }
            }
        }
    }

    // Enhanced program with gamification features
    public class Program
    {
        // Added gamification - level system
        private static string GetCurrentLevel(int score)
        {
            if (score < 500) return "Novice Seeker (Level 1)";
            else if (score < 1000) return "Apprentice Quester (Level 2)";
            else if (score < 2000) return "Dedicated Pilgrim (Level 3)";
            else if (score < 3500) return "Spiritual Voyager (Level 4)";
            else if (score < 5000) return "Enlightened Disciple (Level 5)";
            else if (score < 7500) return "Celestial Pathfinder (Level 6)";
            else if (score < 10000) return "Exalted Champion (Level 7)";
            else return "Eternal Master (Level 8)";
        }

        // Added gamification - achievements
        private static List<string> CheckAchievements(GoalManager manager)
        {
            List<string> achievements = new List<string>();
            List<Goal> goals = manager.GetGoals();
            
            int completedGoals = 0;
            int eternalEvents = 0;
            bool hasCompletedChecklist = false;
            
            foreach (Goal goal in goals)
            {
                if (goal.IsComplete)
                    completedGoals++;
                
                if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete)
                    hasCompletedChecklist = true;
                
                if (goal is EternalGoal && goal.GetDetailsString().Contains("Scripture"))
                    eternalEvents++;
            }
            
            if (completedGoals >= 1)
                achievements.Add("First Step - Complete your first goal");
            
            if (completedGoals >= 5)
                achievements.Add("Goal Setter - Complete 5 goals");
            
            if (hasCompletedChecklist)
                achievements.Add("Persistent - Complete a checklist goal");
            
            if (eternalEvents >= 1)
                achievements.Add("Eternal Learner - Record progress on scripture study");
            
            return achievements;
        }

        public static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            bool running = true;

            while (running)
            {
                // Display current level based on score
                string currentLevel = GetCurrentLevel(manager.Score);
                
                Console.WriteLine();
                Console.WriteLine($"=== ETERNAL QUEST - {currentLevel} ===");
                Console.WriteLine($"You have {manager.Score} points.");
                Console.WriteLine();
                Console.WriteLine("Menu Options:");
                Console.WriteLine("  1. Create New Goal");
                Console.WriteLine("  2. List Goals");
                Console.WriteLine("  3. Save Goals");
                Console.WriteLine("  4. Load Goals");
                Console.WriteLine("  5. Record Event");
                Console.WriteLine("  6. View Achievements");
                Console.WriteLine("  7. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": // Create new goal
                        CreateGoal(manager);
                        break;
                    case "2": // List goals
                        manager.ListGoals();
                        break;
                    case "3": // Save goals
                        Console.Write("Enter filename to save: ");
                        string saveFilename = Console.ReadLine();
                        manager.SaveGoals(saveFilename);
                        Console.WriteLine("Goals saved successfully!");
                        break;
                    case "4": // Load goals
                        Console.Write("Enter filename to load: ");
                        string loadFilename = Console.ReadLine();
                        manager.LoadGoals(loadFilename);
                        Console.WriteLine("Goals loaded successfully!");
                        break;
                    case "5": // Record event
                        RecordEvent(manager);
                        break;
                    case "6": // View achievements
                        DisplayAchievements(manager);
                        break;
                    case "7": // Quit
                        running = false;
                        Console.WriteLine("Thanks for using Eternal Quest!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void CreateGoal(GoalManager manager)
        {
            Console.WriteLine("The types of Goals are:");
            Console.WriteLine("  1. Simple Goal");
            Console.WriteLine("  2. Eternal Goal");
            Console.WriteLine("  3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");

            string goalTypeChoice = Console.ReadLine();
            
            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();
            
            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();
            
            Console.Write("What is the amount of points associated with this goal? ");
            int points = int.Parse(Console.ReadLine());

            Goal newGoal;

            switch (goalTypeChoice)
            {
                case "1": // Simple Goal
                    newGoal = new SimpleGoal(name, description, points);
                    break;
                case "2": // Eternal Goal
                    newGoal = new EternalGoal(name, description, points);
                    break;
                case "3": // Checklist Goal
                    Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                    int target = int.Parse(Console.ReadLine());
                    
                    Console.Write("What is the bonus for accomplishing it that many times? ");
                    int bonus = int.Parse(Console.ReadLine());
                    
                    newGoal = new ChecklistGoal(name, description, points, target, bonus);
                    break;
                default:
                    Console.WriteLine("Invalid goal type.");
                    return;
            }

            manager.AddGoal(newGoal);
            Console.WriteLine("Goal created successfully!");
        }

        private static void RecordEvent(GoalManager manager)
        {
            manager.ListGoals();
            
            Console.Write("Which goal did you accomplish? ");
            string input = Console.ReadLine();
            
            if (int.TryParse(input, out int index))
            {
                manager.RecordEvent(index - 1); // Adjust for 0-based indexing
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private static void DisplayAchievements(GoalManager manager)
        {
            List<string> achievements = CheckAchievements(manager);
            
            if (achievements.Count == 0)
            {
                Console.WriteLine("You haven't earned any achievements yet. Keep going!");
            }
            else
            {
                Console.WriteLine("=== YOUR ACHIEVEMENTS ===");
                foreach (string achievement in achievements)
                {
                    Console.WriteLine($"🏆 {achievement}");
                }
            }
        }

        // Added exceeding requirements details
        /* 
           EXCEEDING REQUIREMENTS:
           1. Added a level system based on points earned
           2. Added achievements that unlock based on progress
           3. Enhanced the user interface with clearer formatting
           4. Added validation and better error handling
           5. Used different symbols for different goal types (∞ for eternal goals)
        */
    }
}