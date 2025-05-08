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

        transform.position = Utils.Bezier(t, startPos, midPos, endPos);
    }
}
