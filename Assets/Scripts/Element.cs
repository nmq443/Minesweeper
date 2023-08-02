using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Element : MonoBehaviour
{
    // Is this a mine
    bool mine;

    // Different textures
    public Sprite[] emptyTextures;
    public Sprite mineTexture;

    public bool IsMine
    {
        get
        {
            return mine;
        }
    }

    void Start()
    {
        mine = UnityEngine.Random.value < 0.16;
        
        int x = (int) transform.position.x;
        int y = (int) transform.position.y;

        Playfield.elements[x, y] = this;
    }

    public void loadTexture(int adjacentCount)
    {
        if (mine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
        }
    }        

    public bool isCovered()
    {
        return GetComponent<SpriteRenderer>().sprite.texture.name == "default";
    }

    private void OnMouseUpAsButton()
    {
        if (mine)
        {
            // TODO: uncover all mines
            // game over
            Playfield.uncoverMines();
            Debug.Log("you lose");
        } else
        {
            // show adjacent mine number
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;

            loadTexture(Playfield.adjacentMines(x, y));

            // if recursive dfs is preferred then use this line of code
            // uncover area without mines
            Playfield.FFuncoverDFS(x, y, new bool[Playfield.w, Playfield.h]);

            if (Playfield.isFinished())
            {
                Debug.Log("You win");
            }
        }
    }

    
}
