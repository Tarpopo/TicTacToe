using System;
using Tools;

public static class MoveCalculator
{
    private static bool IsMovesLeft(Signs?[,] board, int gridSize)
    {
        for (int i = 0; i < gridSize; i++)
        for (int j = 0; j < gridSize; j++)
            if (board[i, j] == null)
                return true;
        return false;
    }

    private static T[] GetMassToOneDimensional<T>(T[,] mass)
    {
        var newMass = new T[mass.GetLength(0) * mass.GetLength(1)];
        var previous = 0;
        for (var i = 0; i < mass.GetLength(0); i++)
        {
            for (var j = 0; j < mass.GetLength(1); j++)
            {
                newMass[previous] = mass[i, j];
                previous++;
            }
        }

        return newMass;
    }

    private static T[,] GetMassToTwoDimensional<T>(T[] mass, int size)
    {
        var newMass = new T[size, size];
        var previous = 0;
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                newMass[i, j] = mass[previous];
                previous++;
            }
        }

        return newMass;
    }

    private static int Evaluate(Signs?[,] board, Signs player, Signs opponent)
    {
        var winChecker = Toolbox.Get<WinChecker>();
        if (winChecker.CheckWin(GetMassToOneDimensional(board), player)) return +10;
        if (winChecker.CheckWin(GetMassToOneDimensional(board), opponent)) return -10;
        return 0;
    }

    private static int Minimax(Signs?[,] board, int depth, bool isMax, Signs player, Signs opponent, int gridSize)
    {
        int score = Evaluate(board, player, opponent);

        if (score == 10) return score;

        if (score == -10) return score;

        if (IsMovesLeft(board, gridSize) == false) return 0;


        if (isMax)
        {
            int best = -1000;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (board[i, j] == null)
                    {
                        board[i, j] = player;
                        best = Math.Max(best, Minimax(board, depth + 1, !isMax, player, opponent, gridSize));
                        board[i, j] = null;
                    }
                }
            }

            return best;
        }

        else
        {
            int best = 1000;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (board[i, j] == null)
                    {
                        board[i, j] = opponent;
                        best = Math.Min(best, Minimax(board, depth + 1, !isMax, player, opponent, gridSize));
                        board[i, j] = null;
                    }
                }
            }

            return best;
        }
    }

    public static int FindBestMove(Signs?[] cells, Signs player, Signs opponent, int gridSize)
    {
        int bestVal = -1000;
        var bestCellIndex = 0;
        var previous = 0;
        var board = GetMassToTwoDimensional(cells, gridSize);
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (board[i, j] == null)
                {
                    board[i, j] = player;
                    var moveVal = Minimax(board, 0, false, player, opponent, gridSize);
                    board[i, j] = null;
                    if (moveVal > bestVal)
                    {
                        bestVal = moveVal;
                        bestCellIndex = previous;
                    }
                }

                previous++;
            }
        }

        return bestCellIndex;
    }
}