﻿namespace Schneider.Minefield
{
    using System.ComponentModel;
    using System.Text;

    public class Minefield
    {
        private Random random = new Random();

        private List<Coordinate> _activeMines = new List<Coordinate>();
        private List<Coordinate> _explodedMines = new List<Coordinate>();
        private readonly int _maxSquares;

        public Minefield(int width, int height)
        {
            _maxSquares = width * height;
            GridWidth = width;
            GridHeight = height;
        }

        public int GridWidth { get; }
        public int GridHeight { get; }

        public int MinesLeft => _activeMines.Count;

        public MoveOutcome GetStatus(Coordinate coordinate)
        {
            if (_activeMines.Contains(coordinate))
            {
                _activeMines.Remove(coordinate);
                _explodedMines.Add(coordinate);
                return MoveOutcome.MineExplodes;
            }

            if (_explodedMines.Contains(coordinate))
            {
                return MoveOutcome.ExplodedMine;
            }

            return MoveOutcome.NoMine;
        }

        public void PlantMine(Coordinate location)
        {
            var exists = _activeMines.FirstOrDefault(x => x.Equals(location));
            if (exists is null)
            {
                _activeMines.Add(location);
            }
        }

        public void PlantExplodedMine(Coordinate coordinate)
        {
            _explodedMines.Add(coordinate);
        }

        public void PlantMines(int numberOfMines)
        {
            if (numberOfMines > _maxSquares)
            {
                throw new ArgumentException("More mines than available spaces");
            }

            while (_activeMines.Count < numberOfMines)
            {
                var x = random.Next(0, 8);
                var y = random.Next(0, 8);
                PlantMine(new Coordinate(x, y));
            }
        }

        public string ShowMap()
        {
            var sorted = _activeMines.OrderBy(x => x.X).ThenBy(x => x.Y).ToList();
            var sb = new StringBuilder();
            for (int y = GridHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < GridWidth; x++)
                {
                    if (_activeMines.Any(a => a.Equals(new Coordinate(x, y))))
                    {
                        sb.Append("M");
                    }
                    else if (_explodedMines.Any(a => a.Equals(new Coordinate(x, y))))
                    {
                        sb.Append("E");
                    }
                    else
                    {
                        sb.Append("0");
                    }
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }
}