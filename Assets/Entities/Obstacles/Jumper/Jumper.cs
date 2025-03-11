using UnityEngine;

public class Jumper : MonoBehaviour
{
    // question: 플레이어 충동 시 가속 정도를 곱하는 형태를 띄어야하는 지?
    public float JumpForce = 10f;
    void OnCollisionEnter(Collision collision)
    {
        // if (!collision.gameObject.CompareTag("Player")) return;

        // try 형태로 다시 관리
        // var playerController = collision.gameObject.GetComponent<PlayerController>();

        // fix: 충돌이 먼저 발생할 경우 isGround 인식 안됨
        // if (!playerController.isGround) return;

        // 위로 날리기
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.Impulse);
        Managers.Get<AudioManager>().PlaySFX("DM-CGS-07");
    }
}
