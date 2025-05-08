using UnityEngine;

public class BulletBezier : MonoBehaviour
{
    public float travelTime = 1.0f;
    private float timer;

    private Vector2 startPos;
    private Vector2 midPos;
    private Vector2 endPos;

    private bool isActive = false;

    public void Init(Vector2 start, Vector2 end)
    {
        startPos = start;
        endPos = end;
        midPos = Utils.GetMiddlePosition(start, end);

        timer = 0f;
        isActive = true;
        /*
        Vector2 direction = (end - start).normalized;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ * Mathf.Rad2Deg);
        */
        transform.position = start;

        Debug.Log($"[BulletBezier] Initialized from {start} to {end} via {midPos}");
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;
        float t = timer / travelTime;

        if (t >= 1f)
        {
            transform.position = endPos;
            Debug.Log("[BulletBezier] Reached target, destroying bullet.");
            Destroy(gameObject);
            return;
        }

        // Calculate the tangent direction and apply rotation
        Vector2 tangent = Utils.BezierTangent(t, startPos, midPos, endPos);
        float rotationZ = Mathf.Atan2(tangent.y, tangent.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        transform.position = Utils.Bezier(t, startPos, midPos, endPos);
    }
}
