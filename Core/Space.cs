namespace Core
{
    public class Space
    {
        public readonly int RowNumber;

        public Token? Token { get; set; } = null;
        public Space(int rowNumber)
        {
            RowNumber = rowNumber;
        }
    }
}