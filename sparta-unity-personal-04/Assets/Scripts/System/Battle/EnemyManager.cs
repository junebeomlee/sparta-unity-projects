using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyManager: MonoBehaviour
{
    
    public List<GameObject> enemyList = new();
    private List<GameObject> _currentEnemies = new();

    private void Awake()
    {
        
    }

    private void Start()
    {
        enemyList.ForEach(enemy =>
        {
            var monster = Instantiate(enemy, Vector3.zero, Quaternion.identity);
            _currentEnemies.Add(monster);
            
        });
    }

    public GameObject GetEnemyByPosition(Vector2Int v2)
    {
        return _currentEnemies.Find(enemy => Mathf.Approximately(enemy.transform.position.x, v2.x) && Mathf.Approximately(enemy.transform.position.y, v2.y));
    }
}