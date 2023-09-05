namespace Schneider.Minefield
{
    public class Coordinate : IEquatable<Coordinate>
    {
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X;
        public int Y;

        public bool Equals(Coordinate? other)
        {
            if (other == null)
            {
                return false;
            }

            if (other.Y != Y || other.X != X)
            {
                return false;
            }
            return true;
        }

        public override string ToString()
        {
            return $"{X} {Y}";
        }
    }
}