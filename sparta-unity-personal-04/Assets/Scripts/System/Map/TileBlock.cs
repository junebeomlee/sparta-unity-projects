using System;
using UnityEngine;

public enum BlockType
{
    Ground,
    Water,
}

public enum ObstacleType
{
    
}

// moveable, attack 같은 상태 가짐
public class TileBlock: MonoBehaviour
{
    public int movingCost = 1;
    public BlockType blockType;

    private Renderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }
}