using UnityEngine;

public class enemywalk : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;

        rb.gravityScale = 0;
    }

    void Update()
    {
        Vector2 movement = currentPoint.position - transform.position;

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            rb.linearVelocity = Vector2.zero;
            if (currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }
            else
            {
                currentPoint = pointB.transform;
            }
            flip();
        }
        else
        {
            rb.linearVelocity = new Vector2(speed * Mathf.Sign(movement.x), 0);
        }
    }

    void flip()
    {
        // Always flip localScale.x after switching currentPoint
        Vector3 localScale = transform.localScale;
        localScale.x = -localScale.x;
        transform.localScale = localScale;
    }


    void OnDrawGizmos()
    {
        if (pointA != null) Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        if (pointB != null) Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        if (pointA != null && pointB != null) Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
