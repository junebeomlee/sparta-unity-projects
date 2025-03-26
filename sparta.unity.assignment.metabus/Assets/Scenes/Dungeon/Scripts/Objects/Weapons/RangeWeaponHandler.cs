using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{

    // 생성 위치
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    
    // 발사체 순서
    [SerializeField] private int bulletIndex;
    public int BulletIndex {get{return bulletIndex;}}
  
    // 발사체 크기
    [SerializeField] private float bulletSize = 1;
    public float BulletSize {get{return bulletSize;}}
    
    [SerializeField] private float duration;
    public float Duration {get{return duration;}}
    
    [SerializeField] private float spread;
    public float Spread {get{return spread;}}
    
    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot {get{return numberofProjectilesPerShot;}}
    
    [SerializeField] private float multipleProjectilesAngel;
    public float MultipleProjectilesAngel {get{return multipleProjectilesAngel;}}
    
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor {get{return projectileColor;}}
    
    private ProjectileManager projectileManager;

    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        // 애니메이션 발생
        base.Attack();
        
        float projectilesAngleSpace = multipleProjectilesAngel;
        int numberOfProjectilesPerShot = numberofProjectilesPerShot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * multipleProjectilesAngel;

        // 총알 갯수만큼 생성
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    //발사체 생성
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        // 매니저가 모든 총알 관리
        projectileManager.ShootBullet(this, projectileSpawnPosition.position, RotateVector2(_lookDirection, angle));
    }
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}