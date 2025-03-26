블로그 해당 프로젝트 소개 : https://velog.io/@junebeom/TIL0326-3D-%EB%B0%A9%EC%B9%98%ED%98%95-%EA%B2%8C%EC%9E%84-%EA%B2%8C%EC%9E%84-%EC%86%8D-%EB%94%94%EC%9E%90%EC%9D%B8-%ED%8C%A8%ED%84%B4%EA%B3%BC-%EA%B8%B8-%EC%B0%BE%EA%B8%B0-%EC%95%8C%EA%B3%A0%EB%A6%AC%EC%A6%98

# 프로젝트 구성

장르 : 방치형 + 턴제  
테마 : 보통 RPG 유럽 배경(에셋을 기준으로)
플랫폼 : PC + Mobile(가로 화면 위주)

기획 : 방치형 게임의 특성상 자유도가 부족한 점으로 인해 전략적 요소를 가미하여 턴제와 각 종 캐릭터와 속성 등의 개념 추가

---
과제 요소

# 통화 시스템
<img width="513" alt="스크린샷 2025-03-26 오전 11 49 01" src="https://github.com/user-attachments/assets/340f0591-9887-4f53-b179-9800b8741863" />

몬스터가 죽으면 인벤토리 매니저(싱글톤)에 토탈 골드를 추가합니다. 캐릭터 스탯 등이나 가변적인 인 게임 요소는 이벤트 매니저를 통해서 간접적으로 접근하여 의존성을 낮췄습니다.

__이벤트 매니저 클래스__
```cs
public class EventManager: MonoBehaviour
{
    private List<observed> observedElements = new List<observed>();

    public void Subscribe(string type, Action<string> callback)
    {
        observedElements.Add(new observed { type = type, callback = callback });
    }

    public void UnSubscribe(observed observed)
    {
        observedElements.Remove(observed);
    }
    
    public void Notify(string typeValue, string changedValue)
    {
        var elements = observedElements.FindAll(observed => observed.type == typeValue);

        foreach (var element in elements)
        {
            element.callback(changedValue);
        }
    }
}
```

__전투 페이지 UI__
```cs

public class BattleInfoPage: MonoBehaviour
{
    private EventManager _eventManager;
    public TMP_Text goldText;
    
    public void Start()
    {
        _eventManager = GlobalManager.GetManager<EventManager>();
        _eventManager.Subscribe(InventoryManager.Gold, (value) => { goldText.text = value; });
    }

```

---

플레이어 AI

블럭 단위 지형이 가변적 요소를 가지고 있습니다. 인접 타일 검사하여 이동 포인트를 비교해 최종 이동 거리와 공격 범위 확인 뒤 공격 가능한 대상 등을 파악합니다.

```cs
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
```

---

#맵 생성 알고리즘

<img width="273" alt="스크린샷 2025-03-25 오후 1 56 52" src="https://github.com/user-attachments/assets/0aa0bcda-4d60-4606-88db-f8fd9841d5a9" />

셀룰러 오토마타 알고리즘을 통해 인접 타일 중 Water인 부분을 찾아 연결하려는 시도를 했습니다.


```cs

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
                }
            }

        }
    }
```

---

# SO 파일을 활용한 플레이어 리소스 체크
SO파일을 기준으로 스탯을 초기화하여 관리할 수 있도록 처리했습니다.

스탯 SO 파일
```cs
public enum StatType {
    Health,
    Power,
}

[System.Serializable]
public class Stat
{
    public StatType type;
    public int value;
}

// 중복 또는 누락 발생할 수 있음
[CreateAssetMenu(menuName = "SO/statistic")]
public class StatisticSO : ScriptableObject
{
    public List<Stat> Stats;
}
```

리소스 핸들러에서 초기화
```cs
public class ResourceHandler: MonoBehaviour
{
    private Controller _controller; // 중재자 패턴으로 핸들러 관리
    
    public StatisticSO statistic;
    private List<Condition> _conditions = new();

    // public ResourceHandler()
    
    private void Awake()
    {
        _controller = GetComponent<Controller>();
        
        foreach (var stat in statistic.Stats)
        {
            _conditions.Add(new Condition(stat.type, stat.value));
        }
```
