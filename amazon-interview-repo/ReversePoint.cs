using System;

namespace AmazonInterview
{
  public class ReversePoint : Point
  {
    public ReversePoint(int x, int y) : base(x,y) {}
    
    public override int CompareTo(Point b)
    {
      double Tdto = this.DistanceToOrigin();
      double Bdto = b.DistanceToOrigin();
      
      return Bdto.CompareTo(Tdto);
    }
  }
}