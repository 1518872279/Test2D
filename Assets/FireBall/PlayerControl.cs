using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject fireballPrefab;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            ThrowFireball();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
    }

    void ThrowFireball()
    {
        Vector2 startPos = transform.position;
        Vector2 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        GameObject obj = Instantiate(fireballPrefab);
        obj.GetComponent<ThrowFireBall>().Init(startPos, targetPos, 10f, 2f);
    }
}
