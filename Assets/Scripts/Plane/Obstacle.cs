using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

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

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoleSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoleSize);
        bottomObject.localPosition = new Vector3(0, -halfHoleSize);

        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);
        
        transform.position = placePosition;
        
        return placePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            PlaneGameManager.Instance.AddScore(1);
        }
    }

}