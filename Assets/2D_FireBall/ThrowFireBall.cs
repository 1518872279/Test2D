using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireBall : MonoBehaviour
{
    private bool initialized = false;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (initialized)
            transform.Translate(speed * Time.deltaTime, 0f, 0f);
    }

    public void Init(Vector2 startPos, Vector2 targetPos, float speed, float lifeTime)
    {
        Vector2 direction = (targetPos - startPos).normalized;
        transform.position = startPos;
        this.speed = speed;
        Invoke(nameof(DestroyBall), lifeTime);

        initialized = true;

        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ * Mathf.Rad2Deg);
    }

    void DestroyBall()
    {
        Destroy(gameObject);
    }
}
