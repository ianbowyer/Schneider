namespace Schneider.Minefield
{
    public class MinefieldCore
    {
        public MinefieldCore()
        {
            CurrentMinefield = new(8, 8);
        }

        public int NumberOfLives { get; set; } = 3;

        public int Score { get; set; } = 0;

        public Coordinate CurrentPosition { get; set; } = new Coordinate(0, 0);

        public Minefield CurrentMinefield { get; set; }

        public MoveOutcome Move(Direction direction)
        {
            if (IsAllowedMove(direction))
            {
                switch (direction)
                {
                    case Direction.Up:
                        return GetMoveOutcome(new Coordinate(CurrentPosition.X, CurrentPosition.Y + 1));

                    case Direction.Down:
                        return GetMoveOutcome(new Coordinate(CurrentPosition.X, CurrentPosition.Y - 1));

                    case Direction.Left:
                        return GetMoveOutcome(new Coordinate(CurrentPosition.X - 1, CurrentPosition.Y));

                    case Direction.Right:
                        return GetMoveOutcome(new Coordinate(CurrentPosition.X + 1, CurrentPosition.Y));
                }
            }

            return MoveOutcome.NotAllowed;
        }

        private bool IsAllowedMove(Direction direction)
        {
            return direction switch
            {
                Direction.Up => CurrentPosition.Y < 7,
                Direction.Down => CurrentPosition.Y > 0,
                Direction.Left => CurrentPosition.X > 0,
                Direction.Right => CurrentPosition.X < 7,
                _ => false,
            };
        }

        private MoveOutcome GetMoveOutcome(Coordinate newCoordinate)
        {
            var status = CurrentMinefield.GetStatus(newCoordinate);

            if (status == MoveOutcome.MineExplodes)
            {
                // No Change to current position
                Score++;
                NumberOfLives--;
                return MoveOutcome.MineExplodes;
            }

            if (CurrentPosition.Y == 6)
            {
                Score++;
                CurrentPosition = newCoordinate;
                return MoveOutcome.Completed;
            }

            if (status == MoveOutcome.ExplodedMine)
            {
                Score++;
                CurrentPosition = newCoordinate;
                return MoveOutcome.ExplodedMine;
            }

            Score++;
            CurrentPosition = newCoordinate;
            return status;
        }
    }

    public enum MoveOutcome
    {
        NoMine,
        ExplodedMine,
        MineExplodes,
        Completed,
        NotAllowed,
    }

    public enum Direction
    {
        Up = 'U',
        Down = 'D',
        Left = 'L',
        Right = 'R',
    }

    public enum MinefieldSquareStatus
    {
        Empty = 'E',
        ActiveMine = 'M',
        ExplodedMine = '*',
    }
}