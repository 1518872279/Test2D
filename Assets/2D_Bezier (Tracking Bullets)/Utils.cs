using UnityEngine;

public static class Utils
{
    /// <summary> 计算二次贝塞尔曲线上的点 </summary>
    public static Vector2 Bezier(float t, Vector2 a, Vector2 b, Vector2 c)
    {
        Vector2 ab = Vector2.Lerp(a, b, t);
        Vector2 bc = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(ab, bc, t);
    }

    /// <summary> 用于生成贝塞尔曲线中间的控制点（带弯曲扰动） </summary>
    public static Vector2 GetMiddlePosition(Vector2 a, Vector2 b)
    {
        Vector2 m = Vector2.Lerp(a, b, 0.1f);
        Vector2 normal = Vector2.Perpendicular(a - b).normalized;
        float rd = Random.Range(-2f, 2f);
        float curveRatio = 0.3f;
        return m + (a - b).magnitude * curveRatio * rd * normal;
    }

    /// <summary> 获取鼠标在世界坐标的位置（适用于2D） </summary>
    public static Vector2 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
