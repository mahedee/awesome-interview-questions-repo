using System;
using System.Collections.Generic;

namespace AmazonInterview
{
  public class PointEqualityComparer : IEqualityComparer<Point>
  {
    public bool Equals(Point a, Point b)
    {
      return a.X == b.X && a.Y == b.Y;
    }
    
    public int GetHashCode(Point p)
    {
      int hashcode = 23;
      hashcode = (hashcode * 37) + p.X;
      hashcode = (hashcode * 37) + p.Y;
      
      return hashcode;
    }
  }
}