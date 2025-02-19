using UnityEngine;

// 잘려진 부분 오브젝트 삭제
public class DestroyZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // 이름으로 비교
        if (collision.gameObject.name.Equals("Rubble"))
        {
            Destroy(collision.gameObject);
        }
    }
}