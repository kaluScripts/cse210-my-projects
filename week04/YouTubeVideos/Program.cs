class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Video 1
        Video video1 = new Video("How to Make Sourdough Bread", "BreadBakingPro", 720);
        video1.AddComment(new Comment("Maria Lopez", "This recipe changed my life! Perfect crust every time."));
        video1.AddComment(new Comment("Jake Thompson", "Finally a tutorial that explains the fermentation step clearly."));
        video1.AddComment(new Comment("SunnySide_Bakes", "I tried this and it came out amazing. Thank you!"));
        video1.AddComment(new Comment("ChefMarcus", "Great video, but I prefer a longer cold proof for more flavor."));
        videos.Add(video1);

        // Video 2
        Video video2 = new Video("Top 10 Python Tips for Beginners", "CodeWithAlex", 540);
        video2.AddComment(new Comment("DevNovice99", "Tip #7 was a game changer for my projects!"));
        video2.AddComment(new Comment("Sara Kim", "Very well explained. Subscribed immediately!"));
        video2.AddComment(new Comment("TechTalksDaily", "I use Python every day and still learned something new here."));
        videos.Add(video2);

        // Video 3
        Video video3 = new Video("Beginner's Guide to Watercolor Painting", "ArtByNadia", 1080);
        video3.AddComment(new Comment("ColorfulDreamer", "This made me feel like I can actually paint. So encouraging!"));
        video3.AddComment(new Comment("LucasBianchi", "The wet-on-wet technique demo was exactly what I needed."));
        video3.AddComment(new Comment("PaintAndSip_Fan", "Just bought my first set of watercolors. Starting with this!"));
        video3.AddComment(new Comment("GalleryOwner_PDX", "Solid fundamentals. I recommend this to all my students."));
        videos.Add(video3);

        // Video 4
        Video video4 = new Video("5 Minute Morning Yoga Routine", "ZenWithJordan", 315);
        video4.AddComment(new Comment("MorningRiser22", "I've done this every day for two weeks and feel incredible."));
        video4.AddComment(new Comment("YogaNewbie", "Short, effective, and easy to follow. Perfect for beginners."));
        video4.AddComment(new Comment("FitnessCoachTara", "Love the pacing. Great for all fitness levels."));
        videos.Add(video4);

        // Display all videos
        foreach (Video video in videos)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"Title:    {video.GetTitle()}");
            Console.WriteLine($"Author:   {video.GetAuthor()}");
            Console.WriteLine($"Length:   {video.GetLengthInSeconds()} seconds");
            Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
            Console.WriteLine();
            Console.WriteLine("--- Comments ---");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  {comment.GetCommenterName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}
