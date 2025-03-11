using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class DynamicBake : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public float buildInterval = 5f;

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        StartCoroutine(BuildNavMeshAtIntervals());
    }

    void Update()
    {

    }
    
    private IEnumerator BuildNavMeshAtIntervals()
    {
        while (true)
        {
            // NavMesh 빌드
            navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh가 빌드되었습니다!");

            // 일정 시간 대기 (buildInterval 시간 동안 대기)
            yield return new WaitForSeconds(buildInterval);
        }
    }
}
