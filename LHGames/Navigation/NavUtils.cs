using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Navigation
{
    public class NavUtils
    {        
        public static IEnumerable<Point> ForEachAdjacentTile(Point origin)
        {
            return new List<Point>()
            {
                new Point(origin.X + 1, origin.Y),
                new Point(origin.X - 1, origin.Y),
                new Point(origin.X, origin.Y + 1),
                new Point(origin.X, origin.Y - 1),
            };
        } 
    }
}