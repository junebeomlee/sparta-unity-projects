using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    // 싱글톤
    private static ProjectileManager instance;
    public static ProjectileManager Instance => instance;

    // 발사체 목록
    [SerializeField] private GameObject[] projectilePrefabs;
    
    // 충돌 액션 FX
    [SerializeField] private ParticleSystem impactParticleSystem;

    private void Awake()
    {
        instance = this;
    }
    
    public void ShootBullet(RangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction )
    {
        GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
        GameObject obj = Instantiate(origin,startPostiion, Quaternion.identity);
        
        //
        ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        projectileController.Init(direction, rangeWeaponHandler, this);
    }

    public void createImpactParticleSystem(Vector3 position, RangeWeaponHandler weaponHandler)
    {
        impactParticleSystem.transform.position = position;
        ParticleSystem.EmissionModule emission = impactParticleSystem.emission;
        emission.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));
        
        ParticleSystem.MainModule mainModule = impactParticleSystem.main;
        // 총알 크기에 따른
        mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
        impactParticleSystem.Play();
    }
}