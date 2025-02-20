using System;
using Scene.World;
using UnityEngine;

// horse 와 마찬가지로 pivot만 다르며 유사함
public class Helmet : MonoBehaviour
{
    private bool _isPlayerEnter = false;
    private bool _isPlayerEquip = false;
    
    // 플레이어 인식에 대해
    private GameObject _target;
    

    // Update is called once per frame
    void Update()
    {
        // 타고 나선 trigger out으로 처리되어 ride 여부로 확인
        if (_isPlayerEnter && Input.GetKeyDown(KeyCode.X))
        {
            if (!_target) return;

            Scene.World.PlayerController playerController = _target.GetComponent<Scene.World.PlayerController>();

            if (!_isPlayerEquip && playerController)
            {
                playerController.SetEquip(gameObject);
                _isPlayerEquip = true;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerEnter = true;
            _target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _isPlayerEnter = false;
        }
    }
}