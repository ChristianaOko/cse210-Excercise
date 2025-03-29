using System;
using System.Collections.Generic;

// Class for tracking comment information
class Comment
{
    // Properties
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    // Constructor
    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }
}

// Class for tracking video information
class Video
{
    // Properties
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }

    // Constructor
    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    // Method to add a comment
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Method to get number of comments
    public int GetCommentCount()
    {
        return Comments.Count;
    }

    // Method to get all comments
    public List<Comment> GetComments()
    {
        return Comments;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("YouTube Video Tracking Program\n");

        // Create a list to hold videos
        List<Video> videos = new List<Video>();

        // Create first video and add comments
        Video video1 = new Video("C# Programming Tutorial for Beginners", "CodeMaster", 1260);
        video1.AddComment(new Comment("ProgrammingFan", "This tutorial helped me understand classes. Thanks!"));
        video1.AddComment(new Comment("BeginnerCoder", "Could you explain inheritance in your next video?"));
        video1.AddComment(new Comment("TechEnthusiast", "Great explanation of abstraction concepts!"));
        videos.Add(video1);

        // Create second video and add comments
        Video video2 = new Video("10 VS Code Productivity Tips", "DevGuru", 540);
        video2.AddComment(new Comment("CodeNewbie", "I didn't know about tip #7, that's so useful!"));
        video2.AddComment(new Comment("SoftwareDev", "I use these shortcuts every day. Great video!"));
        video2.AddComment(new Comment("TechReviewer", "Could you make a video about VS Code extensions?"));
        video2.AddComment(new Comment("ProgrammerJoe", "These tips saved me hours of work. Thanks!"));
        videos.Add(video2);

        // Create third video and add comments
        Video video3 = new Video("Building a Web API with .NET Core", "BackendPro", 1845);
        video3.AddComment(new Comment("WebDeveloper", "Your explanation of REST principles was clear and concise."));
        video3.AddComment(new Comment("APIExpert", "I would add authentication to this example."));
        video3.AddComment(new Comment("FullStackLearner", "This helped me connect my React frontend to a .NET backend."));
        videos.Add(video3);

        // Create fourth video and add comments
        Video video4 = new Video("Git Workflow for Teams", "CollabMaster", 900);
        video4.AddComment(new Comment("TeamLead", "We implemented this workflow and it improved our productivity."));
        video4.AddComment(new Comment("JuniorDev", "Finally I understand merge vs rebase!"));
        video4.AddComment(new Comment("OpenSourceContributor", "Great explanation of branch strategy."));
        videos.Add(video4);

        // Display information for each video
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            
            // Convert seconds to minutes and seconds format
            int minutes = video.LengthInSeconds / 60;
            int seconds = video.LengthInSeconds % 60;
            Console.WriteLine($"Length: {minutes}:{seconds:D2}");
            
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");
            
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
            }
            
            Console.WriteLine(); // Empty line between videos
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}