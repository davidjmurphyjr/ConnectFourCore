using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public interface IGameService
    {
        GameState GetGameState(List<int> moves);
        bool CanMakeMove(GameState gameState, int columnNumber);
    }
    public class GameService : IGameService
    {
        public bool CanMakeMove(GameState gameState, int columnNumber)
        {
            if (gameState.Winner.HasValue)
            {
                return false;
            }

            var moveColumnHasEmptySpace = gameState.Columns[columnNumber].Any(t => t == null);
            if (!moveColumnHasEmptySpace)
            {
                return false;
            }

            return true;
        }
        
        public GameState GetGameState(List<int> moves)
        {
            var gameState = new GameState {TokenToDrop = Token.Red};
            foreach (var columnNumber in moves)
            {
                var moveColumn = gameState.Columns[columnNumber];
                var rowNumberOfFirstEmptySpace = Array.IndexOf(moveColumn, null);
                var tokenToDrop = MoveNumberToToken(gameState.NumberOfMovesMade);
                moveColumn[rowNumberOfFirstEmptySpace] = tokenToDrop;
                gameState.NumberOfMovesMade++;
                
                if (moves.Count >= 7)
                {
                    var hasConnection = HasConnectionAt(gameState, columnNumber, rowNumberOfFirstEmptySpace);
                    if (hasConnection)
                    {
                        gameState.TokenToDrop = null;
                        gameState.Winner = tokenToDrop;
                        return gameState;
                    }
                }
                gameState.TokenToDrop = MoveNumberToToken(gameState.NumberOfMovesMade);
            }

            return gameState;
        }



        private Token MoveNumberToToken(int moveNumber)
        {
            return moveNumber % 2 == 0 ? Token.Red : Token.Yellow;
        }
        
        private bool HasConnectionAt(GameState gameState, int columnNumber, int rowNumber)
        {
            return CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.Vertical)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.Horizontal)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.DiagonalUp)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.DiagonalDown);
        }

        private bool CheckForConnectionAt(GameState gameState, int columnNumber, int rowNumber, SearchPattern searchPattern)
        {
            var sequenceColor = gameState.Columns[columnNumber][rowNumber];
            var connected = 1;
            var colToCheck = columnNumber;
            var rowToCheck = rowNumber;
            var rowDelta = 0;
            var colDelta = 0;
            var checkedReverseDirection = false;
            switch (searchPattern)
            {
                case SearchPattern.Horizontal:
                    colDelta = 1;
                    break;
                case SearchPattern.Vertical:
                    rowDelta = 1;
                    break;
                case SearchPattern.DiagonalUp:
                    rowDelta = 1;
                    colDelta = 1;
                    break;
                case SearchPattern.DiagonalDown:
                    rowDelta = -1;
                    colDelta = 1;
                    break;
            }

            while (connected < 4)
            {
                rowToCheck = rowToCheck + rowDelta;
                colToCheck = colToCheck + colDelta;
                if (gameState.Columns.ElementAtOrDefault(colToCheck)?.ElementAtOrDefault(rowToCheck) == sequenceColor)
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
}