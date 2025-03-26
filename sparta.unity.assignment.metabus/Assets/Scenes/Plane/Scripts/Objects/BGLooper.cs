using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 프리로드, 동적 로드: 맵이 작으면 연산이 적은 프리로드가 낫다.
// 오브젝트 풀링: 자주 생성되고 삭제되는 객체를 미리 만들어두고 재사용하는 기법
public class BGLooper : MonoBehaviour
{
    // 그려져 있는 배경의 갯수
    public int numBgCount = 5;
    // 장애물 갯수
    public int obstacleCount = 8;
    // 초기 값 지정(진행 시간(스코어)이나 레벨에 따른 난이도가 생길 수도)
    public Vector3 obstacleLastPosition  = Vector3.zero;
    
    void Start()
    {
        // 검색을 통한 탐색 - 성능은 좋지 않은 편
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        
        // 맨 처응ㅁ 위치와, 장애물 갯수를 파악해서
        obstacleLastPosition = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;
        
        for(int i = 0; i < obstacleCount; i++)
        {
            // 랜덤으로 위치를 그린다.
            obstacleLastPosition = obstacles[i].SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // lean: collision.name 등으로 게임 오브젝트 이름 파악 가능
        
        // 배경 충돌 시
        if (collision.CompareTag("Background"))
        {
            // q: 방스 콬라이더로 형 변환(박싱은 안 발생하는 것 같으나, 성능에 문제가 있는 지?
            // 사이즈를 통해 width를 구함
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            // 현재 위치에서 x 위치만 변경
            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;
            return;
        }
        
        Obstacle obstacle = collision.GetComponent<Obstacle>();
        if (obstacle)
        {
            obstacleLastPosition = obstacle.SetRandomPlace(obstacleLastPosition, obstacleCount);
        }
    }
}