using System;
using Scene.World;
using Unity.VisualScripting;
using UnityEngine;

public class Horse : MonoBehaviour
{
    private bool _isPlayerEnter = false;
    private bool _isPlayerRide = false;
    
    public GameObject Sheat;
    private GameObject _target;
    
    void Start()
    {
        Debug.Log("Horse");   
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerEnter && Input.GetKeyDown(KeyCode.Space))
        {
            if (!_target) return;

            if (!_isPlayerRide)
            {
                // 탑승 시킴
                // _target.transform.parent = Sheat.transform;
                Scene.World.PlayerController playerController = _target.GetComponent<Scene.World.PlayerController>();
                if (playerController)
                {
                    playerController.SetRide(gameObject);
                    _isPlayerRide = true;
                }
            }
            else
            {
                // _target.transform.parent = null;
                // _isPlayerRide = false;
            }
        }
    }
    
    // q: 색상이 활성화 안되면 인식이 안되는데, 이유는 찾아봐야 할 듯.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerEnter = true;
        }
        _target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
        _isPlayerEnter = false;
        }
        _target = collision.gameObject;
    }
}
