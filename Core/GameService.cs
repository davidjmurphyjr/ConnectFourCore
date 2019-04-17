using System;
using System.Linq;

namespace Core
{
    public class GameService : IGameService
    {
        private readonly IMoveRepository _moveRepository;

        public GameService(IMoveRepository moveRepository)
        {
            _moveRepository = moveRepository;
        }

        public void  MakeMove(Guid gameId, int columnNumber)
        {
            var gameState =  GetGameState(gameId);
            var canMakeMove = CanMakeMove(gameState, columnNumber);
            if (!canMakeMove)
            {
                return;
            }
            var move = new Move
            {
                GameId = gameId,
                MoveNumber = gameState.NumberOfMovesMade,
                ColumnNumber = columnNumber
            };
            _moveRepository.Add(move);
        }

        private bool CanMakeMove(GameState gameState, int moveColumnNumber)
        {
            var anyEmptySpacesInColumn = gameState.Spaces
                .Where(s => s.ColumnNumber == moveColumnNumber)
                .OrderBy(s => s.RowNumber)
                .Any(s => s.Token == null);
            return anyEmptySpacesInColumn;
        }

        public GameState GetGameState(Guid gameId)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).ToList();
            var gameState = new GameState();
            foreach (var move in moves)
            {
                var firstEmptySpaceInColumn = gameState.Spaces
                    .Where(s => s.ColumnNumber == move.ColumnNumber)
                    .OrderBy(s => s.RowNumber)
                    .First(s => s.Token == null);
                var tokenToDrop = move.MoveNumber % 2 == 0 ? Token.Red : Token.Yellow;
                firstEmptySpaceInColumn.Token = tokenToDrop;
                
                var hasConnection = HasConnectionAt(gameState, firstEmptySpaceInColumn.ColumnNumber, firstEmptySpaceInColumn.RowNumber);
                if (hasConnection)
                {
                    gameState.Winner = tokenToDrop;
                    break;
                }
            }

            gameState.NumberOfMovesMade = moves.Count;
            return gameState;
        }

        public bool HasConnectionAt(GameState gameState, int columnNumber, int rowNumber)
        {
            return CheckForConnectionAt(gameState, columnNumber, rowNumber, Orientation.Vertical)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, Orientation.Horizontal)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, Orientation.DiagonalUp)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, Orientation.DiagonalDown);
        }

        private bool CheckForConnectionAt(GameState gameState, int columnNumber, int rowNumber, Orientation orientation)
        {
            var sequenceColor = gameState.Spaces.First(c => c.ColumnNumber == columnNumber && c.RowNumber == rowNumber).Token;
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
                case Orientation.Vertical:
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

                if (colToCheck < GameState.NumberOfColumns
                    && colToCheck >= 0
                    && rowToCheck < GameState.NumberOfRows
                    && rowToCheck >= 0
                    && gameState.Spaces.First(s => s.RowNumber == rowToCheck && s.ColumnNumber == colToCheck).Token == sequenceColor)
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
        
      
    }

    public interface IGameService
    {
        void MakeMove(Guid gameId, int columnNumber);
        GameState GetGameState(Guid gameId);
    }
}