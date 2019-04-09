using System.Collections.Generic;

namespace Core
{
    public enum Token
    {
        Black,
        Red
    }

    public class Column
    {
        public const int NumberOfRows = 6;
        private Token?[] tokens  = new Token?[NumberOfRows];
         public Token?[] Tokens  {
             get{ return tokens; }
         }
        public Token? Row(int rowNumber)
        {
            return tokens[rowNumber];
        }
        /// <summary> 
        /// returns the row where the token landed. 
        /// null if the token could ne be added because the column is full 
        /// </summary>
        public int? Drop(Token token)
        {
            int? openRowNumber = null;
            //look for an open space row in the column starting from the bottom.
            for (int rowNumber = 0; rowNumber < tokens.Length; rowNumber++)
            {
                if (tokens[rowNumber] == null)
                {
                    openRowNumber = rowNumber;
                    break;
                }
            }
            if (openRowNumber != null)
            {
                tokens[openRowNumber.Value] = token;
            }
            return openRowNumber;
        }
    }

    public class Game
    {
        public Board Board { get; } 
        public string BlackPlayer { get; set; }
        public string RedPlayer { get; set; }
        public bool IsBlackTurn { get; }
        public Token? Winner { get; private set; }
        public Game(){
            Board = new Board();
            BlackPlayer = "Player One";
            RedPlayer = "Player Two";
        }

        public void MakeMove(int columnNumber, Token token)
        {
            int? rowNumberTokenLandedAt = Board.Column(columnNumber).Drop(token);
            if (rowNumberTokenLandedAt != null)
            {
                if(Board.HasConnectionAt(columnNumber, rowNumberTokenLandedAt.Value))
                {
                    Winner = token;
                }
            }
        }
    }
}