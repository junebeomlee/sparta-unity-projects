using UnityEngine;

class Movement
{
    private readonly Transform _target;
    private readonly Stats _stats;

    public Movement(Transform target, Stats stats)
    {
        _target = target;
        _stats = stats; 
    }

    public void Move(Vector3 direction)
    {
        _target.position += direction * (_stats.Speed * Time.deltaTime);
    }
}

class KeyBindings
{
    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}

// monoBehaviour에 직접 기능이 들어가면 위치 선정도 어렵고 깔끔하지 않아 분리한다.
public class PlayerController : MonoBehaviour
{
    private Movement _movement;
    // learn: 필드에서 직접 값을 등록하는 것은 추상적 형태를 받는 유연성에서 떨어지나, 이처럼 명료한 관계를 가질 때는 유효한 방식이다.
    private readonly KeyBindings _keyBindings = new KeyBindings();
    private Stats _stats;
    
    [HideInInspector] public ResourceController ResourceController;

    void Start()
    {
        // 의존 관계 생성(플레이어 스크립트를 호출할 때 Stats 컴포넌트가 필수가 되어버림
        _stats = GetComponent<Stats>();
        ResourceController = new ResourceController(_stats);
        // stats 자체를 넣어야 런타임에서 인스펙터 내 값을 변경해도 인지 가능
        _movement = new Movement(transform, _stats);

    }
    void Update()
    {
        Vector2 input = _keyBindings.GetInput(); // 🔥 키 바인더에서 입력 가져오기
        _movement.Move(input);
    }
}