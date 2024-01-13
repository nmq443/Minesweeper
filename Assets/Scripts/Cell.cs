using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] Sprite defaultTexture;
    [SerializeField] Sprite mineTexture;
    [SerializeField] Sprite[] numberTextures;
    private GameObject gameController;
    private bool mine = false;

    public void Initiate() {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        mine = UnityEngine.Random.value < 0.15;
        // mine = (1 < 0);
    }

    public bool isCovered() {
        return GetComponent<SpriteRenderer>().sprite == defaultTexture;
    }

    public bool isMine() {
        return mine;
    }

    public void loadTexture(int adjacentMines) {
        if (mine) {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        } else {
            GetComponent<SpriteRenderer>().sprite = numberTextures[adjacentMines];
        }
    }

    private void OnMouseUpAsButton() {
        if (gameController.GetComponent<GameController>().isFinished)
            return;
        // if is mine => lose
        if (mine) {
            // release all mines
            gameController.GetComponent<GameController>().uncoverAllMines();
            gameController.GetComponent<GameController>().GameOver(false);
        } else {
            Debug.Log("continue");
            int x = Mathf.Abs((int)transform.position.x + 4);
            int y = Mathf.Abs((int)transform.position.y + 4);
            int adjacentMines = gameController.GetComponent<GameController>().adjacentMines(x, y);
            loadTexture(adjacentMines);
            bool[,] visited = new bool[gameController.GetComponent<GameController>().getHeight(), gameController.GetComponent<GameController>().getWidth()];
            gameController.GetComponent<GameController>().FFUncover(x, y, visited);
            gameController.GetComponent<GameController>().GameOver(true);
        }
    }
}
