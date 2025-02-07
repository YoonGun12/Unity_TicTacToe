using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public static class MinimaxAIController
{
    public static (int row, int col)? GetBestMove(GameManager.PlayerType[,] board)
    {
        float bestScore = -1000;
        (int row, int col) bestMove = (-1, -1);
        
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == GameManager.PlayerType.None)
                {
                    board[row, col] = GameManager.PlayerType.PlayerB;
                    var score = DoMinimax(board, 0, false);
                    board[row, col] = GameManager.PlayerType.None;
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = (row, col);
                    }
                }
            }
        }

        if (bestMove != (-1, -1))
        {
            return (bestMove.row, bestMove.col);
        }
        return null;
    }
    
    private static float DoMinimax(GameManager.PlayerType[,] board, int depth, bool isMaximizing)
    {
        //플레이어가 이기면 -10점
        if (CheckGameWin(GameManager.PlayerType.PlayerA, board))
        {
            return -10 + depth; // depth를 고려하여 더 빠른 승리를 선호
        }
        //AI가 이기면 +10점
        if (CheckGameWin(GameManager.PlayerType.PlayerB, board))
        {
            return 10 - depth; // depth를 고려하여 더 빠른 승리를 선호
        }
        //무승부의 경우
        if (IsAllBlocksPlaced(board))
        {
            return 0;
        }

        if (isMaximizing) //AI의 턴에서 가장 점수가 높은(이길 확률이 높을) 수를 선택
        {
            var bestScore = float.MinValue;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == GameManager.PlayerType.None)
                    {
                        board[row, col] = GameManager.PlayerType.PlayerB;
                        var score = DoMinimax(board, depth + 1, false);
                        board[row, col] = GameManager.PlayerType.None;
                        bestScore = Math.Max(bestScore, score);
                    }
                }
            }
            return bestScore;
        }
        else //플레이어의 턴에서 가장 점수가 낮은(AI가 이길 확률이 높을) 수를 선택
        {
            var bestScore = float.MaxValue;
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (board[row, col] == GameManager.PlayerType.None)
                    {
                        board[row, col] = GameManager.PlayerType.PlayerA;
                        var score = DoMinimax(board, depth + 1, true);
                        board[row, col] = GameManager.PlayerType.None;
                        bestScore = Math.Min(bestScore, score);
                    }
                }
            }
            return bestScore;
        }
    }


    /// <summary>
    /// 모든 마커가 보드에 배치 되었는지 확인하는 함수
    /// </summary>
    /// <returns>True: 모두 배치</returns>
    public static bool IsAllBlocksPlaced(GameManager.PlayerType[,] board)
    {
        for (var row = 0; row < board.GetLength(0); row++)
        {
            for (var col = 0; col < board.GetLength(1); col++)
            {
                if (board[row, col] == GameManager.PlayerType.None)
                    return false;
            }

        }
        return true;
    }
    
    //게임의 승패를 판단하는 함수
    public static bool CheckGameWin(GameManager.PlayerType playerType, GameManager.PlayerType[,] board)
    {
        //가로로 마커가 일치하는지 확인
        for (var row = 0; row < board.GetLength(0); row++)
        {
            if (board[row, 0] == playerType && board[row, 1] == playerType && board[row, 2] == playerType)
            {
                return true;
            }
        }
      
        //세로로 마커가 일치하는지 확인
        for (var col = 0; col < board.GetLength(0); col++)
        {
            if (board[0, col] == playerType && board[1, col] == playerType && board[2, col] == playerType)
            {
                return true;
            }
        }
      
        //대각선 마커가 일치하는지 확인
        if (board[0, 0] == playerType && board[1, 1] == playerType && board[2, 2] == playerType)
        {
            return true;
        }

        if (board[0, 2] == playerType && board[1, 1] == playerType && board[2, 0] == playerType)
        {
            return true;
        }

        //모든 경우에 해당하지 않을 때
        return false;
    }
}
