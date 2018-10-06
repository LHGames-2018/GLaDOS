using System.Collections.Generic;
using LHGames.Helper;

namespace LHGames.Navigation
{
    public class NavUtils
    {
        public static IEnumerable<Point> ForEachAdjacentTile(Tile origin)
        {
            return new List<Point>()
            {
                new Point(origin.Position.X + 1, origin.Position.Y),
                new Point(origin.Position.X - 1, origin.Position.Y),
                new Point(origin.Position.X, origin.Position.Y + 1),
                new Point(origin.Position.X, origin.Position.Y - 1),
            };
        } 
    }
}