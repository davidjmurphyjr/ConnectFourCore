namespace Core
{
    public class Space
    {
        public readonly int RowNumber;
        public readonly int ColumnNumber;

        public Token? Token { get; set; } = null;
        public Space(int rowNumber, int columnNumber)
        {
            RowNumber = rowNumber;
            ColumnNumber = columnNumber;
        }
    }
}