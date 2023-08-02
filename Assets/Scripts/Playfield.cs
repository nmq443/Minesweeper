using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Playfield 
{
    // The Grid itself
    public static int w = 32;
    public static int h = 16;
    public static Element[,] elements = new Element[w, h];

    public static void uncoverMines()
    {
        foreach(Element element in elements) 
        { 
            if (element == null) continue;
            if (element.IsMine)
            {
                element.loadTexture(0);
            }    
        }
    }

    public static bool mineAt(int x, int y)
    {
        // Coordination in range? Then check for mine
        if (x >= 0 && y >= 0 && x < w && y < h && elements[x, y] != null)
        {
            return elements[x, y].IsMine;
        }
        return false;
    }

    public static int adjacentMines(int x, int y)
    {
        int count = 0;
        int[,] dir = { { -1, 0 }, { 1, 0 }, { 0, 1 }, { 0, -1 }, 
                       { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } };
        for (int i = 0; i < 8; i++)
        {
            int xCor = dir[i, 0] + x;
            int yCor = dir[i, 1] + y;
            if (mineAt(xCor, yCor))
                count++;
        }
        return count;
    }

    public static void FFuncoverDFS(int x, int y, bool[,] visited)
    {
        // Coordinates in Range?
        if (x >= 0 && y >= 0 && x < w && y < h && elements[x, y] != null)
        {
            // visited already?
            if (visited[x, y])
                return;

            // uncover element
            elements[x, y].loadTexture(adjacentMines(x, y));

            // close to a mine? then no more work needed here
            if (adjacentMines(x, y) > 0)
                return;

            // set visited flag
            visited[x, y] = true;

            // recursion
            FFuncoverDFS(x - 1, y, visited);
            FFuncoverDFS(x + 1, y, visited);
            FFuncoverDFS(x, y - 1, visited);
            FFuncoverDFS(x, y + 1, visited);
        }
    }

    public static bool isFinished()
    {
        foreach(Element element in elements)
        {
            if (element == null)
                continue;
            if (element.isCovered() && !element.IsMine)
            {
                return false;
            }
        }
        return true;
    }
}
