using System.Collections.Generic;
using UnityEngine;
public static class AIController 
{
    public static (int row, int col) FindNextMove(GameManager.PlayerType[,] board)
    {
        var emptyPoint = GameManager.PlayerType.None;
        var playerPoint = GameManager.PlayerType.PlayerA;
        var AIPoint = GameManager.PlayerType.PlayerB;
        
        //TODO: board의 내용을 보고 다음 수를 계산 후 반환
        
        //승리가 가능한 자리가 있다면 두는 수
        //가로체크
        for (int row = 0; row < 3; row++)
        {
            int count = 0;
            int selectedCol = -1;
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == AIPoint)
                {
                    count++;
                }
                else if (board[row, col] == emptyPoint)
                {
                    selectedCol = col;
                }
            }

            if (count == 2 && selectedCol != -1)
                return (row, selectedCol);
        }
        
        //세로체크
        for (int col = 0; col < 3; col++)
        {
            int count = 0;
            int selectedRow = -1;
            for (int row = 0; row < 3; row++)
            {
                if (board[row, col] == AIPoint)
                {
                    count++;
                }
                else if (board[row, col] == emptyPoint)
                {
                    selectedRow = row;
                }
            }

            if (count == 2 && selectedRow != -1)
                return (selectedRow, col);
        }
        
        //대각선 체크
        if (board[0, 0] == AIPoint && board[1, 1] == AIPoint && board[2, 2] == emptyPoint)
            return (2, 2);
        if (board[0, 0] == AIPoint && board[1, 1] == emptyPoint && board[2, 2] == AIPoint)
            return (1, 1);
        if (board[0, 0] == emptyPoint && board[1, 1] == AIPoint && board[2, 2] == AIPoint)
            return (0, 0);
        
        if (board[0, 2] == AIPoint && board[1, 1] == AIPoint && board[2, 0] == emptyPoint)
            return (2, 0);
        if (board[0, 2] == AIPoint && board[1, 1] == emptyPoint && board[2, 0] == AIPoint)
            return (1, 1);
        if (board[0, 2] == emptyPoint && board[1, 1] == AIPoint && board[2, 0] == AIPoint)
            return (0, 2);
        
        
        //플레이어가 이길 수 있는 자리가 있다면 두는 수
        //가로체크
        for (int row = 0; row < 3; row++)
        {
            int count = 0;
            int selectedCol = -1;
            for (int col = 0; col < 3; col++)
            {
                if (board[row, col] == playerPoint)
                {
                    count++;
                }
                else if (board[row, col] == emptyPoint)
                {
                    selectedCol = col;
                }
            }

            if (count == 2 && selectedCol != -1)
                return (row, selectedCol);
        }
        
        //세로체크
        for (int col = 0; col < 3; col++)
        {
            int count = 0;
            int selectedRow = -1;
            for (int row = 0; row < 3; row++)
            {
                if (board[row, col] == playerPoint)
                {
                    count++;
                }
                else if (board[row, col] == emptyPoint)
                {
                    selectedRow = row;
                }
            }

            if (count == 2 && selectedRow != -1)
                return (selectedRow, col);
        }
        
        //대각선 체크
        if (board[0, 0] == playerPoint && board[1, 1] == playerPoint && board[2, 2] == emptyPoint)
            return (2, 2);
        if (board[0, 0] == playerPoint && board[1, 1] == emptyPoint && board[2, 2] == playerPoint)
            return (1, 1);
        if (board[0, 0] == emptyPoint && board[1, 1] == playerPoint && board[2, 2] == playerPoint)
            return (0, 0);
        
        if (board[0, 2] == playerPoint && board[1, 1] == playerPoint && board[2, 0] == emptyPoint)
            return (2, 0);
        if (board[0, 2] == playerPoint && board[1, 1] == emptyPoint && board[2, 0] == playerPoint)
            return (1, 1);
        if (board[0, 2] == emptyPoint && board[1, 1] == playerPoint && board[2, 0] == playerPoint)
            return (0, 2);
        
        //중앙을 차지하는 수
        if (board[1, 1] == emptyPoint)
            return (1, 1);
        
        // 코너를 차지하는 수
        List<(int row, int col)> corners = new List<(int, int)> { (0, 0), (0, 2), (2, 0), (2, 2) };
        Shuffle(corners);
        foreach (var corner in corners)
        {
            if (board[corner.row, corner.col] == emptyPoint)
                return corner;
        }

        // 가장자리를 차지하는 수
        List<(int row, int col)> edges = new List<(int, int)> { (0, 1), (1, 0), (1, 2), (2, 1) };
        Shuffle(edges);
        foreach (var edge in edges)
        {
            if (board[edge.row, edge.col] == emptyPoint)
                return edge;
        }
        
        return (0, 0);
    }

    private static void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }
}


