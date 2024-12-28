using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Bullet fire point")]
    public Transform firePoint; // Merminin çıkış noktası
    [Header("Bullet fire rate")]
    public float fireRate = 0.5f; // Ateş etme aralığı
    [Header("Max bullets")]
    public int maxBullets = 10; // Maksimum mermi sayısı

    private float fireCooldown = 0f;
    private int currentBullets;
    private ObjectPool objectPool;

    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool>();
        currentBullets = maxBullets;
    }

    void Update()
    {
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }
    }

    public void Fire()
    {
        if (fireCooldown <= 0 && currentBullets > 0)
        {
            GameObject bullet = objectPool.GetBullet();
            if (bullet != null)
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 10f; // Hızı ayarlayın
                currentBullets--;
                fireCooldown = fireRate;
            }
        }
    }

    public void Reload(int bullets)
    {
        currentBullets += bullets;
        if (currentBullets > maxBullets)
        {
            currentBullets = maxBullets;
        }
    }
}