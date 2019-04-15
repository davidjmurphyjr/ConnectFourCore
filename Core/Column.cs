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
        /// <summary> 
        /// returns the row where the token landed. 
        /// null if the token could ne be added because the column is full 
        /// </summary>
        public int? Drop(Token token)
        {
            int? openRowNumber = null;
            //look for an open space row in the column starting from the bottom.
            for (int rowNumber = 0; rowNumber < Tokens.Length; rowNumber++)
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