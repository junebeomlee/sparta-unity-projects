using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using OpenCover.Framework.Model;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapGeneratorSystem: MonoBehaviour
{
    // private static readonly string prefabAddress = "Grass";  // 그룹 이름과 주소를 포함한 주소

    public List<GameObject> groundTiles;
    public Vector2Int gridSize;
    public Dictionary<GameObject, Vector2> _tiles = new();

    private int edgePercent = 20;

    // notice: 여러개일 수도 있음
    // public Transform focusPosition;

    // public List<Class> list = new() { MapGeneratorSystem };

    // [SerializeField] private Vector2 startPosition;

    private void Start()
    {
        var rowEdge = Mathf.RoundToInt(gridSize.x / 100f * edgePercent);
        var colEdge = Mathf.RoundToInt(gridSize.y / 100f * edgePercent);

        for (int col = 0; col < gridSize.y; col++)
        {
            for (int row = 0; row < gridSize.x; row++)
            {

                GameObject tile;
                
                // bool isEdge = (row < rowEdge || row >= gridSize.x - rowEdge || col < colEdge || col >= gridSize.y - colEdge);
                
                // if (Random.value < 0.2f) tile = Instantiate(groundTiles[2], transform); // 물 타일 else
                tile = Instantiate(groundTiles[Random.Range(0, groundTiles.Count - 1)], transform, true);

                tile.transform.localPosition = new Vector3(row, 0, col);
            
                _tiles.Add(tile, new Vector2(row, col));
            }
        }
        
        // Debug.Log(_tiles.Count);
     
        Thread.Sleep(1000);
        RunCellularAutomata();
    }
    
    
    private void RunCellularAutomata()
    {
        // 셀룰러 오토마타 적용: 반복 횟수 설정
        for (int iteration = 0; iteration < 5; iteration++) // 5번 반복하여 물 연결
        {

            // 전체 다시 조사 발생
            for (int col = 0; col < gridSize.y; col++)
            {
                for (int row = 0; row < gridSize.x; row++)
                {
                    bool isWater = IsWater(row, col);
                    int waterNeighborCount = GetWaterNeighborCount(row, col);

                    // 물이 되도록 설정하는 규칙
                    if (!isWater && waterNeighborCount >= 2) // 주변 물이 4개 이상일 경우
                    {
                        Debug.Log(1);
                        
                        var currTile = GetTileByCoord(row, col);
                        Destroy(currTile);
                        currTile = Instantiate(groundTiles[2]);
                        currTile.transform.localPosition = new Vector3(row, transform.position.y, col);
                        currTile.GetComponent<MeshRenderer>().material.color = Color.blue;

                        // newTileStates[new Vector2(row, col)] = true; // 물로 설정
                    }
                    else if (isWater && waterNeighborCount < 2) // 물이지만 주변 물이 4개 미만일 경우
                    {
                        Debug.Log(2);
                        
                        // var currTile = GetTileByCoord(row, col);
                        // Destroy(currTile);
                        // currTile = Instantiate(groundTiles[0]);
                        //
                        // currTile.transform.localPosition = new Vector3(row, transform.position.y, col);
                        // currTile.GetComponent<MeshRenderer>().material.color = Color.green;

                        // newTileStates[new Vector2(row, col)] = false; // 물이 아니게 설정
                    }
                    else
                    {
                        // Debug.Log(3);
                        // newTileStates[new Vector2(row, col)] = isWater;
                    }
                }
            }

        }
    }
    
    
    private bool IsValidPosition(int row, int col)
    {
        return row >= 0 && col >= 0 && row < gridSize.x && col < gridSize.y;
    }
    private bool IsWater(int row, int col)
    {
        GameObject tile = _tiles.FirstOrDefault(t => t.Value == new Vector2(row, col)).Key;
        // 너무 많은 비용 발생할 수 있음
        return tile && tile.GetComponent<TileBlock>().blockType == BlockType.Water;
    }
    private int GetWaterNeighborCount(int row, int col)
    {
        int waterCount = 0;
        
        Vector2[] directions = {
            new(0, 1), new(0, -1),
            new(1, 0), new(-1, 0)
        };

        foreach (var direction in directions)
        {
            int newRow = row + (int)direction.x;
            int newCol = col + (int)direction.y;
            if (IsValidPosition(newRow, newCol) && IsWater(newRow, newCol))
            {
                waterCount++;
            }
        }
        return waterCount;
    }
    

    [CanBeNull]
    public GameObject GetTileByCoord(int row, int col)
    {
        return _tiles.FirstOrDefault(tile => tile.Value == new Vector2(row, col)).Key;
    }
    
}


public interface ITile
{
    // public string Name { get; }
}

public class GroundTile
{
    public static string Name = "Ground";
}

public class WaterTile
{
    public static string Name = "Water";
}