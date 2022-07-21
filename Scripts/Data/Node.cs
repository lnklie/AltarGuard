using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public bool isWall;
    public Node ParentNode;

    // g : 시작으로부터 이동했던 거리, h: |가로| + |세로| 장애물 무시하여 목표까지의 거리
    public int x, y, G, H;
    public int F { get { return G + H; } }
    public Node(bool _isWall, int _x, int _y)
    {
        isWall = _isWall;
        x = _x;
        y = _y;
    }
}
