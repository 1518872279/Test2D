using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    [SerializeField] private Color color = new Color(191 / 255f, 36 / 255f, 0); // 激光颜色
    [SerializeField] private float colorIntensity = 4.3f; // 颜色强度
    private float beamColorEnhance = 1;

    [SerializeField] private float maxLength = 100; // 激光的最大长度
    [SerializeField] private float thickness = 9; // 激光的厚度
    [SerializeField] private float noiseScale = 3.14f; // 激光的噪声比例
    [SerializeField] private GameObject startVFX; // 激光起始效果
    [SerializeField] private GameObject endVFX; // 激光结束效果

    private LineRenderer lineRenderer; // 用于绘制激光的LineRenderer

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        // 设置激光颜色和材质
        lineRenderer.material.color = color * colorIntensity;
        lineRenderer.material.SetFloat("_LaserThickness", thickness);
        lineRenderer.material.SetFloat("_LaserScale", noiseScale);

        // 设置粒子效果的发光颜色
        ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in particles)
        {
            Renderer r = p.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnhance));
        }
    }

    private void Start()
    {
        UpdateEndPosition(); // 初始更新激光结束位置
    }

    private void Update()
    {
        UpdateEndPosition(); // 每帧更新激光结束位置
    }

    // 更新激光的起始和终止位置
    public void UpdatePosition(Vector2 startPosition, Vector2 direction)
    {
        direction = direction.normalized; // 归一化方向
        transform.position = startPosition;

        // 计算激光旋转角度
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0, 0, rotationZ * Mathf.Rad2Deg);
    }

    // 更新激光的终点位置
    private void UpdateEndPosition()
    {
        Vector2 startPosition = transform.position;
        float rotationZ = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);

        float length = maxLength; // 初始长度为最大长度
        float laserEndRotation = 180;

        if (hit) // 如果发生碰撞
        {
            // 计算碰撞点到起点的距离
            length = (hit.point - startPosition).magnitude;

            // 设置激光终点为碰撞点
            endVFX.transform.position = hit.point;
            
            // 计算激光的旋转角度，使其朝向碰撞法线
            laserEndRotation = Vector2.Angle(direction, hit.normal);
            Debug.Log("Laser hit " + hit.collider.gameObject);
        }
        else // 如果没有碰撞，激光终点为最大长度
        {
            endVFX.transform.position = startPosition + direction * maxLength;
        }

        // 更新 LineRenderer 的终点位置
        lineRenderer.SetPosition(1, new Vector2(length, 0));

        // 保持起始粒子效果的相对位置
        //startVFX.transform.position = startPosition + (Vector2)transform.right * -10f;
        startVFX.transform.position = startPosition;
        // 应用激光的旋转到终点效果
        endVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
    }
}
