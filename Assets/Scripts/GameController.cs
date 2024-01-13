using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // 3 modes: 9x9, 16x16, 16x30
    [SerializeField] Cell cellPrefab;
    [SerializeField] private int WIDTH;
    [SerializeField] private int HEIGHT;
    private Cell[,] cells;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text gameOverText;
    public bool isFinished = false;

    private void Start() {
        cells = new Cell[HEIGHT, WIDTH];
        for (int x = 0; x < HEIGHT; ++x) {
            for (int y = 0; y < WIDTH; ++y) {
                Cell c = Instantiate(cellPrefab, new Vector3((-4+x), (-4+y), -1), Quaternion.identity);
                c.Initiate();
                cells[x, y] = c;
            }
        }
    }

    public void GameOver(bool won) {
        if (!won) {
            gameOverText.text = "You lose!";
            gameOver.SetActive(true);
            isFinished = true;
        } else {
            bool isOver = true;
            foreach(Cell cell in cells) {
                if (cell == null) continue;
                if (cell.isCovered() && !cell.isMine())
                    isOver = false;
            }
            if (isOver) {
                gameOver.SetActive(true);
                gameOverText.text = "You won!";
                isFinished = true;
            }
        }
    }

    public void FFUncover(int x, int y, bool[,] visited) {
        if (x >= 0 && y >= 0 && x < HEIGHT && y < WIDTH && cells[x, y] != null) {
                if (visited[x, y] == false) {
                int[,] directions = {{-1, 0}, {1, 0}, {0, 1}, {0, -1}};
                cells[x, y].loadTexture(adjacentMines(x, y));
                visited[x, y] = true;

                // if close to a mine then do not need to find more
                if (adjacentMines(x, y) > 0)
                    return;
                
                // dfs
                for (int i = 0; i < 4; ++i) {
                    FFUncover(x + directions[i, 0], y + directions[i, 1], visited);
                }
            }
        }
    }

    public void uncoverAllMines() {
        foreach (Cell cell in cells) {
            if (cell == null) 
                continue;
            if (cell.isMine())
                cell.loadTexture(0);
        }
    }
    public int adjacentMines(int x, int y) {
        int r = 0;

        int[,] directions = {{-1, 0}, {1, 0}, {0, 1}, {0, -1},
                             {-1, 1}, {1, -1}, {1, 1}, {-1, -1}};
        for (int i = 0; i < 8; ++i) {
            int nextX = x + directions[i, 0];
            int nextY = y + directions[i, 1];
            if (mineAt(nextX, nextY))
                ++r;
        }
        
        return r;
    }

    private bool mineAt(int x, int y) {
        if (x >= 0 && y >= 0 && x < HEIGHT && y < WIDTH)
            return cells[x, y].isMine();
        return false;
    }

    // getter
    public int getWidth() {
        return WIDTH;
    }

    public int getHeight() {
        return HEIGHT;
    }
}
