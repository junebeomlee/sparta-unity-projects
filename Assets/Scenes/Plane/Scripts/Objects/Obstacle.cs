using System;
using Scene.Plane;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

// 꼭 붙지 않고, 각각 생성할 순 없을까?
public class Obstacle : MonoBehaviour
{
    // 한 장애물의 높이 범위
    public float highPosY = 4f;
    public float lowPosY = -4f;

    // 최소와 최대 간극
    public float holeSizeMin = 3f;
    public float holeSizeMax = 4f;

    public Transform topObject;
    public Transform bottomObject;

    // 장애물 간의 간격
    public float widthPadding = 4f;

    // 이전의 위치를 받아서 간격에 따라 배치
    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // 간극 지정, 랜덥을 통해 최소부터 최대까지
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        
        // 자식 요소이므로 
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        
        // 위치 조정은 랜덤으로
        placePosition.y = Random.Range(lowPosY, highPosY);
        
        transform.position = placePosition;
        
        return placePosition;
    }

    // 통과 시 포인트 증가
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(1);
        }
    }

}