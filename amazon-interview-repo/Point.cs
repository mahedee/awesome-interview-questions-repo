using System;

namespace AmazonInterview
{
  public class Point : IComparable<Point>
  {
    public int X { get; private set; }
    public int Y { get; private set; }
    
    public Point(int x, int y)
    {
      X = x;
      Y = y;
    }
    
    public double DistanceToOrigin()
    {
      return Math.Pow(X, 2) + Math.Pow(Y, 2);
    }
    
    public virtual int CompareTo(Point b)
    {
      double Tdto = this.DistanceToOrigin();
      double Bdto = b.DistanceToOrigin();
      
      return Tdto.CompareTo(Bdto);
    }
    
    public override string ToString()
    {
      return String.Format("({0,2}, {1,2}) [{2,2}]", this.X, this.Y, this.DistanceToOrigin());
    }
  }
}