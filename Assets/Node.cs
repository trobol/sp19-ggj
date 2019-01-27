﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;
    public int gridY;

    public bool isWall;
    public Vector3 Position;

    public Node Parent;

    public int gCost;
    public int hCost;

    public int FCost { get { return gCost + hCost; } }

    public Node(bool a_Iswall, Vector3 a_pos, int a_gridX, int a_gridY)
    {
        isWall = a_Iswall;
        Position = a_pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }

}
