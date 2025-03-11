using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> topographies;
    
    public int width = 16;
    public int height = 16;

    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        GameObject[,] map = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GameObject tilePrefab = topographies[Random.Range(0, topographies.Count)];
                Vector3 position = new Vector3(x, 0, z);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity);
                map[x, z] = tile;
            }
        }
    }
}
