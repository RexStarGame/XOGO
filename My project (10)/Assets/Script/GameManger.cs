using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public enum Turn { Player, AI1, AI2 }
    public Turn currentTurn = Turn.Player;

    private string playerHero;
    private string ai1Hero;
    private string ai2Hero;

    private string[,] board = new string[3, 3]; // 3x3 grid
    private bool gameActive = true;

    public void SetupPlayers(string humanHero, List<string> aiHeroes)
    {
        playerHero = humanHero;
        ai1Hero = aiHeroes[0];
        ai2Hero = aiHeroes[1];

        currentTurn = Turn.Player;
        gameActive = true;
    }

    void Update()
    {
        if (!gameActive) return;

        if (currentTurn == Turn.AI1)
        {
            MakeAIMove(ai1Hero);
            currentTurn = Turn.AI2;
        }
        else if (currentTurn == Turn.AI2)
        {
            MakeAIMove(ai2Hero);
            currentTurn = Turn.Player;
        }
    }

    public void PlayerMove(int x, int y)
    {
        if (currentTurn != Turn.Player || board[x, y] != null) return;

        board[x, y] = playerHero;
        Debug.Log($"Player placed {playerHero} at {x},{y}");

        if (CheckWin(playerHero)) EndGame(playerHero);

        currentTurn = Turn.AI1;
    }

    void MakeAIMove(string aiHero)
    {
        Vector2Int move = FindBestMove();
        board[move.x, move.y] = aiHero;
        Debug.Log($"AI ({aiHero}) placed at {move.x},{move.y}");

        if (CheckWin(aiHero)) EndGame(aiHero);
    }

    Vector2Int FindBestMove()
    {
        // Simple AI: Pick first available cell
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if (board[x, y] == null)
                    return new Vector2Int(x, y);
            }
        }
        return Vector2Int.zero; // fallback
    }

    void EndGame(string winner)
    {
        Debug.Log($"Game Over! {winner} wins!");
        gameActive = false;
    }

    bool CheckWin(string hero)
    {
        // Rows, cols, diagonals
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == hero && board[i, 1] == hero && board[i, 2] == hero) return true;
            if (board[0, i] == hero && board[1, i] == hero && board[2, i] == hero) return true;
        }
        if (board[0, 0] == hero && board[1, 1] == hero && board[2, 2] == hero) return true;
        if (board[0, 2] == hero && board[1, 1] == hero && board[2, 0] == hero) return true;
        return false;
    }
}
