namespace Core
{
    public class Board
    {
        public const int NumberOfColumns = 7;

        private readonly Column[] columns;

        public Token?[][] Rows
        {
            get
            {
                var rows = new Token?[Core.Column.NumberOfRows][];
                for(int rowNumber = 0; rowNumber < rows.Length; rowNumber++){
                   Token?[] row = new Token?[NumberOfColumns];
                    for(int columnNumber = 0; columnNumber < NumberOfColumns; columnNumber++){
                        row[columnNumber] = Column(columnNumber).Row(rowNumber);
                    }
                    rows[rowNumber] = row;
                } 
                return rows;
            }
        }

        public Board()
        {
            columns = new Column[NumberOfColumns];
            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new Column();
            }
        }

        public Column Column(int columnNumber)
        {
            return columns[columnNumber];
        }

        private bool CheckForConnectionAt(int columnNumber, int rowNumber, Orientation orientation)
        {
            Token sequenceColor = Column(columnNumber).Row(rowNumber).Value;
            var connected = 1;
            var colToCheck = columnNumber;
            var rowToCheck = rowNumber;
            var rowDelta = 0;
            var colDelta = 0;
            var checkedReverseDirection = false;
            switch (orientation)
            {
                case Orientation.Horizontal:
                    colDelta = 1;
                    break;
                case Orientation.Virtical:
                    rowDelta = 1;
                    break;
                case Orientation.DiagonalUp:
                    rowDelta = 1;
                    colDelta = 1;
                    break;
                case Orientation.DiagonalDown:
                    rowDelta = -1;
                    colDelta = 1;
                    break;
            }
            while (connected < 4)
            {
                rowToCheck = rowToCheck + rowDelta;
                colToCheck = colToCheck + colDelta;

                if (colToCheck < NumberOfColumns
                && colToCheck >= 0
                && rowToCheck < Core.Column.NumberOfRows
                && rowToCheck >= 0
                && Column(colToCheck).Row(rowToCheck) == sequenceColor)
                {
                    connected++;
                }
                else
                {
                    if (!checkedReverseDirection)
                    {
                        checkedReverseDirection = true;
                        colToCheck = columnNumber;
                        rowToCheck = rowNumber;
                        rowDelta = rowDelta * -1;
                        colDelta = colDelta * -1;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return connected == 4;
        }

        public bool HasConnectionAt(int columnNumber, int rowNumber)
        {
            return CheckForConnectionAt(columnNumber, rowNumber, Orientation.Virtical)
            || CheckForConnectionAt(columnNumber, rowNumber, Orientation.Horizontal)
            || CheckForConnectionAt(columnNumber, rowNumber, Orientation.DiagonalUp)
            || CheckForConnectionAt(columnNumber, rowNumber, Orientation.DiagonalDown);
        }
    }

    public enum Orientation
    {
        Horizontal,
        Virtical,
        DiagonalUp,
        DiagonalDown
    }
}
