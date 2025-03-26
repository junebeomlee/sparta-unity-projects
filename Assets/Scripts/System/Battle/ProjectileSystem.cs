using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectileSystem: MonoBehaviour
{
    private ObjectPool<GameObject> _pool;
    
    public List<GameObject> projectileList;
    

    private GameObject GenerateProjectile()
    {
        return projectileList[0];
    }

    private void RemoveProjectile(GameObject projectile)
    {
        Destroy(projectile.gameObject);
    }
}