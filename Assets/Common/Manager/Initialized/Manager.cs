using UnityEngine;

// fix: 다중 상속이 불가능하여, MonoBehaviour를 전달하기 위해 상속받게 됨.
public class Manager : MonoBehaviour
{
    // notice: 순서상 글로벌 싱글톤이 우선 생성되어야 하므로 씬이 로드 된 이후 실행되도록 한다.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Initialize()
    {
        
    }
}