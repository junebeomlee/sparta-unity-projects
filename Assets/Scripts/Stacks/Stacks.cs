using UnityEngine;

public class TheStack : MonoBehaviour
{
    private const float BoundSize = 3.5f;   // 블록의 기본 크기
    private const float MovingBoundsSize = 3f;  // 블록이 움직일 수 있는 한계
    private const float StackMovingSpeed = 5.0f; // 스택이 움직이는 속도
    private const float BlockMovingSpeed = 3.5f; // 블록이 움직이는 속도
    private const float ErrorMargin = 0.1f; // 블록이 정확하게 맞춰져야 하는 기준
    
    public GameObject originBlock = null; // 블록의 원본 프리팹 (유니티에서 설정 필요)
    private Vector3 prevBlockPosition; // 이전 블록의 위치 저장
    private Vector3 desiredPosition; // 블록이 목표로 하는 위치
    
    
    // fix: parents 관련 내용 때문인지 비율로 증가하는 경우 발생
    // private Vector3 stackBounds = new Vector3(1, 1, 1); // 블록의 크기
    private Vector3 stackBounds = new Vector3(BoundSize, 1, BoundSize); // 블록의 크기

    Transform lastBlock = null; // 마지막으로 생성된 블록의 Transform 정보
    float blockTransition = 0f; // 블록 이동 상태를 추적하는 변수
    float secondaryPosition = 0f; // 블록이 움직이는 보조 변수

    int stackCount = -1; // 스택에 쌓인 블록 개수
    int comboCount = 0; // 연속해서 성공한 개수 (사용되지 않음)
    
    public Color prevColor;
    public Color nextColor;
    
    bool isMovingX = true;
    
    void Start()
    {
        if(originBlock == null) { Debug.Log("OriginBlock is NULL"); return; }
    
        prevColor = GetRandomColor(); // 랜덤 색상 설정
        nextColor = GetRandomColor();
    
        prevBlockPosition = Vector3.down; // 첫 번째 블록 위치 초기화
        Spawn_Block(); // 처음 블록 생성
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 클릭 시
        {
            Spawn_Block(); // 블록 생성
        }

        // 처음 스택 제외
        if (stackCount != -1)
        {
            MoveBlock();
        }
        
        
        // 러프되면 자연스러운 움직임을 위한
        transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        // transform.position = new Vector3(transform.position.x, desiredPosition.y, transform.position.z);

    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    bool Spawn_Block()
    {
        if (lastBlock)
        {
            prevBlockPosition = lastBlock.localPosition; // 이전 블록의 위치 저장
        }
    
        GameObject newBlock = Instantiate(originBlock); // 새로운 블록 생성
        if(!newBlock) { Debug.Log("NewBlock Instantiate Failed!"); return false; }
        ColorChange(newBlock); // 블록 색상 변경
    
        Transform newTrans = newBlock.transform;
        newTrans.parent = this.transform; // 부모를 현재 오브젝트로 설정
        newTrans.localPosition = prevBlockPosition + Vector3.up; // 이전 블록 위에 배치
        newTrans.localRotation = Quaternion.identity; // 회전 없음
        newTrans.localScale = stackBounds; // 크기 설정

        stackCount++; // 스택 개수 증가

        desiredPosition = Vector3.down * stackCount; // 스택이 내려가야 할 위치 업데이트
        blockTransition = 0f; // 블록 이동 초기화

        lastBlock = newTrans; // 현재 블록을 마지막 블록으로 저장
        
        isMovingX = !isMovingX;

        return true;
    }
    
    Color GetRandomColor()
    {
        float r = Random.Range(100f, 250f) / 255f;
        float g = Random.Range(100f, 250f) / 255f;
        float b = Random.Range(100f, 250f) / 255f;

        return new Color(r, g, b);
    }
    
    // 랜덤으로 컬러 생성
    void ColorChange(GameObject selectedObject)
    {
        Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

        Renderer selectedRenderer = selectedObject.GetComponent<Renderer>();

        if(!selectedRenderer)
        {
            Debug.Log("Renderer is NULL!");
            return;
        }

        selectedRenderer.material.color = applyColor;
        // 카메라의 배경색을 변경
        Camera.main!.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

        if(applyColor.Equals(nextColor) == true)
        {
            prevColor = nextColor;
            nextColor = GetRandomColor();
        }
    }
    
    void MoveBlock()
    {
        blockTransition += Time.deltaTime * BlockMovingSpeed;
    
        float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

        // X축과 Z축으로의 이동
        if (isMovingX)
        {
            lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
        }
        else
        {
            lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
        }
    }

    public void PlaceBlock()
    {
           Vector3 lastPosition = lastBlock.localPosition;

           if (isMovingX)
           {
               float deltaX = prevBlockPosition.x - lastPosition.x;
           }
           else
           {
               
           }
    }
}
