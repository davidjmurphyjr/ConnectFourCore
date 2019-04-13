namespace Core
{
    public class Game
    {
        public Board Board { get; } 
        public Token? Winner { get; private set; }
        public Game(){
            Board = new Board();
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