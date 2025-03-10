using UnityEngine;

// fix: 다중 상속이 불가능하여, MonoBehaviour를 전달하기 위해 상속받게 됨.
public class Manager : MonoBehaviour
{
    private void Awake()
    {
        GlobalManager.Register(this);
    }
}   