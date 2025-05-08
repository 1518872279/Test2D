using UnityEngine;

public class AttackController : MonoBehaviour
{
    public GameObject bulletPrefab;      // 拖入你的子弹预制体
    public float cooldown = 0.5f;        // 发射冷却时间

    private float cooldownTimer;

    void Start()
    {
        Debug.Log("[AttackController] Ready to fire.");
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("[AttackController] Left mouse clicked.");
        }

        if (Input.GetMouseButtonDown(0) && cooldownTimer >= cooldown)
        {
            FireBullet();
            cooldownTimer = 0f;
        }
    }

    private void FireBullet()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("[AttackController] bulletPrefab not assigned!");
            return;
        }

        Vector2 start = transform.position;
        Vector2 target = Utils.GetMousePosition();

        Debug.Log($"[AttackController] Firing from {start} to {target}");

        GameObject bulletGO = Instantiate(bulletPrefab, start, Quaternion.identity);
        BulletBezier bullet = bulletGO.GetComponent<BulletBezier>();
        if (bullet == null)
        {
            Debug.LogError("[AttackController] BulletBezier script missing on prefab!");
            return;
        }

        bullet.Init(start, target);
    }
}
