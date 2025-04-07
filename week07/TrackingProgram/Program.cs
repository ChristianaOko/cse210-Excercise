using System;
using System.Collections.Generic;

// Base Activity class
public abstract class Activity
{
    private DateTime _date;
    private int _minutes;

    public Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Getters for common properties
    public DateTime GetDate()
    {
        return _date;
    }

    public int GetMinutes()
    {
        return _minutes;
    }

    // Abstract methods to be implemented by derived classes
    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();

    // Common method to get summary for all activities
    public virtual string GetSummary()
    {
        return $"{_date.ToString("dd MMM yyyy")} {GetType().Name} ({_minutes} min) - " +
               $"Distance {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, " +
               $"Pace: {GetPace():F1} min per mile";
    }
}

// Running activity class
public class Running : Activity
{
    private double _distance; // distance in miles

    public Running(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        return (_distance / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / _distance;
    }
}

// Cycling activity class
public class Cycling : Activity
{
    private double _speed; // speed in mph

    public Cycling(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override double GetDistance()
    {
        return (_speed * GetMinutes()) / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        return 60 / _speed;
    }
}

// Swimming activity class
public class Swimming : Activity
{
    private int _laps;
    private const double LapDistanceInMiles = 0.031; // 50 meters * 0.62 / 1000

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        return _laps * LapDistanceInMiles;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetMinutes() / GetDistance();
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Create activities
        List<Activity> activities = new List<Activity>();

        // Create a running activity
        activities.Add(new Running(
            new DateTime(2022, 11, 3),
            30,
            3.0)); // 3.0 miles

        // Create a cycling activity
        activities.Add(new Cycling(
            new DateTime(2022, 11, 4),
            45, 
            15.0)); // 15 mph speed

        // Create a swimming activity
        activities.Add(new Swimming(
            new DateTime(2022, 11, 5),
            60,
            40)); // 40 laps

        // Display summaries for all activities
        Console.WriteLine("Exercise Tracking Program");
        Console.WriteLine("------------------------");
        
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}