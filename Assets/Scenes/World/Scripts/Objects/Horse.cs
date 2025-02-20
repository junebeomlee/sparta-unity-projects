using System;
using Scene.World;
using UnityEngine;

public class Horse : MonoBehaviour
{
    private bool _isPlayerEnter = false;
    private bool _isPlayerRide = false;
    
    private GameObject _target;
    
    void Start()
    {
        Debug.Log("Horse");   
    }

    // Update is called once per frame
    void Update()
    {
        // 타고 나선 trigger out으로 처리되어 ride 여부로 확인
        if ((_isPlayerRide || _isPlayerEnter) && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Horse");
            if (!_target) return;

            Scene.World.PlayerController playerController = _target.GetComponent<Scene.World.PlayerController>();

            if (!_isPlayerRide)
            {
                // 탑승 시킴
                // _target.transform.parent = Sheat.transform;
                if (playerController)
                {
                    playerController.SetRide(gameObject);
                    _isPlayerRide = true;
                }
            }
            else
            {
                Debug.Log("내리기");
                playerController.GetOffVehicle(gameObject);
                _isPlayerRide = false;
            }
        }
    }
    
    // q: 색상이 활성화 안되면 인식이 안되는데, 이유는 찾아봐야 할 듯.
    // 타고 있는 동안은 더 이상 발생하지 않기는 함.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Horse");
            _isPlayerEnter = true;
            _target = collision.gameObject;
        }
    }

    // 타면서 out으로 인식하게 됨
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Horse out");
            _isPlayerEnter = false;
            // 타겟이 외부에서 처리되면, 탑승과 함께 타겟을 잃는다.
            // _target = null;
        }
    }
}
