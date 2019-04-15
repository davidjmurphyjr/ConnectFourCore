namespace Core
{
    public class Column
    {
        public const int NumberOfRows = 6;
        public Token?[] Tokens  = new Token?[NumberOfRows];

        public Token? Row(int rowNumber)
        {
            return Tokens[rowNumber];
        }
        
        public int? Drop(Token token)
        {
            int? openRowNumber = null;
            for (var rowNumber = 0; rowNumber < Tokens.Length; rowNumber++)
            {
                if (Tokens[rowNumber] == null)
                {
                    openRowNumber = rowNumber;
                    break;
                }
            }
            if (openRowNumber != null)
            {
                Tokens[openRowNumber.Value] = token;
            }
            return openRowNumber;
        }
    }
}