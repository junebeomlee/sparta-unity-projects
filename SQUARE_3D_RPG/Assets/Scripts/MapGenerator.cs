using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Lazy Generator
public class MapGenerator : MonoBehaviour
{
    // scriptable 형태로 저장하기
    public GameObject Ground;
    public GameObject Water;
    public GameObject Grass;
    public GameObject Rock;
    
    // how: 기준점을 중앙으로 둔다면 너비를 계산해서 절반을 빼야한다. - 퓨어하지 않으므로 컴포지션을 통해 추가 계산되도록 한다.
    
    [Range(0, 10)] public int ColumnLen = 4;
    [Range(0, 10)] public int RowLen = 4;

    // private void OnDrawGizmos()
    // {
        // Debug.Log("OnDrawGizmos");
    // }

    // 높이의 기준점
    public int GroundDepth = 0;
    void Start()
    {
        List<GameObject> tiles = new List<GameObject>() { Ground, Water, Grass, Rock };
        
        // 3D 화면에선 y축은 z가 대신한다.
        var map = 
            from coordX in Enumerable.Range(0, ColumnLen)
            from coordY in Enumerable.Range(0, RowLen)
            select (coordX, coordY);

        foreach (var (coordX, coordY) in map)
        {
            // 메소드(동사) 안에 동작하므로 명사 생략(currentTileType)하는 식으로 줄이기
            int currentType = Random.Range(0, tiles.Count);
            // Y축은 동일
            GameObject tile = Instantiate(tiles[currentType], new Vector3(coordX, GroundDepth, coordY), Quaternion.identity);
            // 높이는 최소 높이 1과의 차감 후 절반 값이 높아져야 같은 시작점을 가지게 된다.
            // tile.transform.localScale = new Vector3(1.0f, Random.Range(1, 4), 1.0f); // 예시로 y축을 2배로 늘리기
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
