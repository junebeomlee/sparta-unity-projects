using UnityEngine;

// 네임 스페이스로 동일한 이름에 대한 제한을 해결할 수 있지만,
namespace Meta
{
    public class PlayerController : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("이거 잘 안되네..");
        }

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            
            // Space 클래스
            // Vector3는 파라미터가 4개이며 default 값에 따라 생략될 수 있음.
            transform.Translate(new Vector3(horizontal, vertical) * Time.deltaTime);
        }
    }

}
