using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    AudioClip audioClip;
    private int movePoint = 4;
    
    public Queue<(int movePoint, Vector2Int position)> moveableTransforms = new();
    
    private MapGeneratorSystem _mapGeneratorSystem;
    private EnemyManager _enemyManager;
    
    
    HashSet<Vector2Int> visited = new();
    
    private List<Vector2Int> attackDirections = new(){
        new (0, 1), new(1, 0), new (0, -1), new(-1, 0)

    };
    HashSet<Vector2Int> attackableTiles = new();
    
    HashSet<GameObject> attackableTargets = new();
    
    public Vector2Int nextPosition;



    public Vector3 targetPosition;
    public float speed = 5f;
    private bool isMoving = false;
    
    
    private List<Vector2Int> _arrowDirections { get; } = new()
    {
        new (0, 1), new(1, 0), new (0, -1), new(-1, 0)
    };

    private void Awake()
    {
        Controls controls = new Controls();
        
        _mapGeneratorSystem = FindObjectOfType<MapGeneratorSystem>();
        _enemyManager = FindObjectOfType<EnemyManager>();

        var outline = GetComponent<Outline>();
        
        controls.Enable();
        controls.Player.Click.performed += ctx =>
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out RaycastHit hit)) { return; }
            
            if (hit.transform == transform)
            {
                // 소리 재생
                GlobalManager.GetManager<AudioManager>().GenerateSoundFrom(transform, "Sounds/click");
                outline.enabled = true;


                moveableTransforms.Clear();
                var sekeletonPosition = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.z));
                moveableTransforms.Enqueue((movePoint, sekeletonPosition));
                GetMoveableTile();

                
                return;
            }
            
            // moveableTransforms.ForEach(move => move.GetComponent<Renderer>().material.color = Color.white); // 색상 초기화
            moveableTransforms.Clear();
            
            outline.enabled = false;
        };
    }

    // position에서 벡터로 필요
    public void GetMoveableTile()
    {

        while (moveableTransforms.Count > 0)
        {
            Debug.Log(1);

            var (remainMovePoint, currentPos) = moveableTransforms.Dequeue();
            
            _arrowDirections.ForEach(arrow =>
            {

                var nextPos = new Vector2Int(currentPos.x + arrow.x, currentPos.y + arrow.y);
            
                // fix: new Vector 라서 존재하는 값으로 인식하지 않음
                // 중복 체크 : 이 타일이 이미 추가되어 있어도 그 다음 타일 조사가 필요한 상황이 발생함
                if (visited.Contains(nextPos)) return;

                // if (remainMovePoint != 1)
                // {
                //     var tile = _mapGeneratorSystem.GetTileByCoord(selectedPosition.x, selectedPosition.y);
                //     if (!tile) return;
                //
                //     cost = remainMovePoint - tile.GetComponent<TileBlock>().movingCost;
                //     GetMoveableTile(cost, selectedPosition);
                //
                // }
                // return;
            
                var curTile = _mapGeneratorSystem.GetTileByCoord(nextPos.x, nextPos.y);
                if (!curTile) return;  // 타일이 없으면 스킵
                
                int cost = curTile.GetComponent<TileBlock>().movingCost;
                int nextMovePoint = remainMovePoint - cost;
                
                if (nextMovePoint >= 0)
                {
                    moveableTransforms.Enqueue((nextMovePoint, nextPos));
                    visited.Add(nextPos);
                    curTile.GetComponent<Renderer>().material.color = Color.green;
                    
                   
                }
            });
        }
        
        Debug.Log(visited.Count);
        foreach (var vector2Int in visited)
        {
            foreach (var attackDir in attackDirections)
            {
                Vector2Int attackPos = vector2Int + attackDir;
                if (!attackableTiles.Contains(attackPos))  // 중복 방지
                {
                    var attackTile = _mapGeneratorSystem.GetTileByCoord(attackPos.x, attackPos.y);
                    if (!attackTile) continue;  // 타일이 없으면 스킵
                    attackTile.GetComponent<Renderer>().material.color = Color.red;
            
                    attackableTiles.Add(attackPos);
                }
            }
        }
       

        foreach (var v2 in attackableTiles)
        {
            var e = _enemyManager.GetEnemyByPosition(v2);
            if (e)
            {
                Debug.Log(e);
                attackableTargets.Add(e);
            }
        }

        if (attackableTargets.Count != 0)
        {
            var target = attackableTargets.First();

            var distace = Vector3.Distance(transform.position, target.transform.position);
            
            // 한칸씩 이동 필요
            while (distace > 1f)
            {
                
                Debug.Log(distace);

                // 가장 가까운 타일
                var closestTile = _arrowDirections
                    .Select(arrow =>
                    {
                        var nextPos = new Vector3Int(Mathf.FloorToInt(transform.position.x) + arrow.x, 0,
                            Mathf.FloorToInt(transform.position.z) + arrow.y);

                        return new
                        {
                            Position = nextPos,
                            Distance = Vector3.Distance(nextPos, target.transform.position)
                        };
                    })
                    .OrderBy(tile => tile.Distance)
                    .First();

                transform.position = closestTile.Position;
                
                distace = Vector3.Distance(transform.position, target.transform.position);
            }
            // MoveToTarget(closestTile.Position);
            
            // 이동 완료 공격 시작
            transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);

            GlobalManager.GetManager<CameraManager>().Shake();
            target.GetComponent<Controller>().SetDamage(110);
        }
        
        
     
    }
    
    public void TriggerMove(Vector3 target)
    {
        targetPosition = target;
        isMoving = true; // 이동 시작
        MoveToTarget(targetPosition); // 이동 시작
    }


    private void MoveToTarget(Vector3 targetPosition)
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            // 목표 지점에 도달하면 이동 멈춤
            if (transform.position == targetPosition)
            {
                isMoving = false; // 이동 끝
            }
        }
    }


    private void Painting()
    {
        MapGeneratorSystem mapGeneratorSystem = FindObjectOfType<MapGeneratorSystem>();
        // Debug.Log(mapGeneratorSystem._tiles.Count);
        // var tile = mapGeneratorSystem._tiles.Find(tile => tile.transform.localPosition == new Vector3(0, 0, 1));
        // Debug.Log(tile);
        // tile.GetComponent<MeshRenderer>().material.color = Color.green;
        // tile.AddComponent<Outline>().OutlineColor = Color.green;
    }
}
