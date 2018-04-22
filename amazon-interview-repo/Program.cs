using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace AmazonInterview
{
  public class Program
  {
    /* Given a list of points, find the K points closest to the origin with no duplicates. */
    public static List<Point> SubmittedClosestToOrigin(List<Point> points, int k)
    {
      if (points == null || k < 0) throw new IndexOutOfRangeException();
      if (points.Count == 0) return points;
      
      HashSet<Point> noduplicates = new HashSet<Point>(new PointEqualityComparer());
      List<Point> results = new List<Point>();
     
      // Remove duplicates by adding each to a hash set. If not in the hash set, add to results set.
      foreach (Point p in points)
      {
        if (!noduplicates.Contains(p))
        {
          results.Add(p);
          noduplicates.Add(p);
        }
      }
      
      // sort the list. Return sublist [0..k]
      results.Sort((x,y) => x.CompareTo(y));
      
      return results.GetRange(0,Math.Min(k,results.Count));
    }
    
    public static List<Point> PQClosestToOrigin(List<Point> points, int k)
    {
      if (points == null || k < 0) throw new IndexOutOfRangeException();
      if (points.Count == 0) return points;
      
      /** Created a ReversePoint object  whose only change is that the CompareTo method will
          * create a Descending sorted list. Needed this change to keep track of the largest item in the Priority Queue.
          */
          
      PriorityQueue<ReversePoint> pq = new PriorityQueue<ReversePoint>();
      HashSet<Point> noduplicates = new HashSet<Point>(new PointEqualityComparer());
      
      foreach(Point p in points)
      {
        // Check if this point has already been visited.
        if (!noduplicates.Contains(p))
          noduplicates.Add(p);
        else
          continue;
        
        // Fill the queue with initial k elements.
        if (pq.Count() < k)
        {
          pq.Enqueue(new ReversePoint(p.X, p.Y));
        }
        else
        {
          ReversePoint peek = pq.Peek();
          
          // Compare current point to largest found point. If current is smaller, dequeue then add new smaller point.
          if (p.DistanceToOrigin() <= peek.DistanceToOrigin())
          {
            pq.Dequeue();
            pq.Enqueue(new ReversePoint(p.X, p.Y));
          }
        }
      }
      
      // Priority Queue class doesn't have a convenient AllItems method. Need to extract them manually.
      List<Point> results = new List<Point>();
      while (pq.Count() > 0)
      {
        ReversePoint p = pq.Dequeue();
        results.Add(new Point(p.X, p.Y));
      }
      
      // Results will be in Descending order. Need to reverse.
      results.Reverse();
      return results;
    }
    
    public static void Main(string[] args)
    {
      Random rnd = new Random();
      
      int k = 2500;
      int n = 100000000;
      Console.WriteLine("K = {0}", k);
      Console.WriteLine("Comparing *Submitted | PriorityQueue* in ms.");
      
      // For performance testing, run 10 times.
      for (int runs = 0; runs < 10; runs++)
      {
        List<Point> points = new List<Point>();
        
        // Add 100 million random points.
        for (int i = 0; i  < n; i++) 
        {
          // Limited points' range to ([-100, 100], [-100, 100]) because it doesn't finish the first loop otherwise.
          // Possibly Point.DistanceToOrigin() is encountering MAX_INT when squaring and adding the values?
          points.Add(new Point(rnd.Next(-100, 100), rnd.Next(-100, 100)));
        }
        
        // Run each version with a timer.
        var start = Process.GetCurrentProcess().TotalProcessorTime;
        List<Point> submitted_results = SubmittedClosestToOrigin(points,k);
        var stop = Process.GetCurrentProcess().TotalProcessorTime;
        var submitted_time = (stop - start).TotalMilliseconds;
        
        start = Process.GetCurrentProcess().TotalProcessorTime;
        List<Point> pq_results = PQClosestToOrigin(points, k);
        stop = Process.GetCurrentProcess().TotalProcessorTime;
        var pq_time = (stop - start).TotalMilliseconds;
        
        Console.WriteLine("{0:0.0000} ms | {1:0.0000} ms ({2:0.0000} ms {3}", 
                                    submitted_time,
                                    pq_time,
                                    Math.Abs(pq_time - submitted_time),
                                    (pq_time < submitted_time) ? "faster" : "slower");
      }
    }
  }
}