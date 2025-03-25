using UnityEngine;

public class CannonController : MonoBehaviour
{
    private Vector2 direction;

    [Header("References")]
    public Transform FirePoint;
    public GameObject CannonBall;
    public GameObject pointPrefab;

    [Header("Trajectory Settings")]
    public int NumberOfPoints = 40;
    public float SpaceBetweenPoints = 0.1f;

    [Header("Fire Settings")]
    public float FireForce = 30f;

    [Header("Gravity Settings")]
    public bool OverrideGlobalGravity = false;
    public float CustomGravityY = -5f;

    private GameObject[] points;
    public static bool canShoot = true;

    void Start()
    {
        if (OverrideGlobalGravity)
            Physics2D.gravity = new Vector2(0f, CustomGravityY);

        points = new GameObject[NumberOfPoints];
        for (int i = 0; i < NumberOfPoints; i++)
            points[i] = Instantiate(pointPrefab, FirePoint.position, Quaternion.identity);
    }

    void Update()
    {
        RotateCannonToMouse();

        if (Input.GetMouseButtonDown(0) && canShoot)
            Fire();

        for (int i = 0; i < NumberOfPoints; i++)
        {
            float t = i * SpaceBetweenPoints;
            points[i].transform.position = CalculateTrajectoryPoint(t);
        }
    }

    void RotateCannonToMouse()
    {
        Vector2 cannonPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - cannonPos;
        transform.right = direction;
    }

    void Fire()
    {
        GameObject ball = Instantiate(CannonBall, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb)
            rb.linearVelocity = direction.normalized * FireForce;
    }

    Vector2 CalculateTrajectoryPoint(float t)
    {
        Vector2 startPos = FirePoint.position;
        Vector2 initialVelocity = direction.normalized * FireForce;
        return startPos + initialVelocity * t + 0.5f * Physics2D.gravity * (t * t);
    }
}
