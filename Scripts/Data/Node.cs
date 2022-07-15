using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public bool isWall;
    public Node ParentNode;

    // g : �������κ��� �̵��ߴ� �Ÿ�, h: |����| + |����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�
    public int x, y, G, H;
    public int F { get { return G + H; } }
    public Node(bool _isWall, int _x, int _y)
    {
        isWall = _isWall;
        x = _x;
        y = _y;
    }
}
