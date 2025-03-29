using UnityEngine;

// Controls the cannon behavior in the gamepage
public class CannonController : MonoBehaviour
{
    // Direction vector from cannon to mouse
    private Vector2 direction;

    [Header("References")]
    public Transform FirePoint;
    public GameObject CannonBall;
    public GameObject pointPrefab;

    [Header("Trajectory Settings")]
    public int NumberOfPoints = 40;
    public float SpaceBetweenPoints = 0.1f; // Time interval between trajectory points

    [Header("Fire Settings")]
    public float FireForce = 30f;         // Force applied to the cannonball

    [Header("Gravity Settings")]
    public bool OverrideGlobalGravity = false;
    public float CustomGravityY = -5f;

    private GameObject[] points;          // Array to store instantiated trajectory points
    public static bool canShoot = true;

    void Start()
    {
        canShoot = true;

        // Set custom gravity if enabled
        if (OverrideGlobalGravity)
            Physics2D.gravity = new Vector2(0f, CustomGravityY);

        // Instantiate trajectory points
        points = new GameObject[NumberOfPoints];
        for (int i = 0; i < NumberOfPoints; i++)
            points[i] = Instantiate(pointPrefab, FirePoint.position, Quaternion.identity);
    }

    void Update()
    {
        // Do not update while game is paused
        if (Time.timeScale == 0f)
            return;

        // Rotate cannon to face mouse position
        RotateCannonToMouse();

        // Fire cannonball on mouse click if allowed
        if (Input.GetMouseButtonDown(0) && canShoot)
            Fire();

        // Update the position of trajectory points
        for (int i = 0; i < NumberOfPoints; i++)
        {
            float t = i * SpaceBetweenPoints;
            points[i].transform.position = CalculateTrajectoryPoint(t);
        }
    }

    // Rotates the cannon towards the mouse cursor
    void RotateCannonToMouse()
    {
        Vector2 cannonPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - cannonPos;
        transform.right = direction;
    }

    // Instantiates and fires a cannonball
    void Fire()
    {
        GameObject ball = Instantiate(CannonBall, FirePoint.position, FirePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb)
            rb.linearVelocity = direction.normalized * FireForce;
    }

    // Calculates the position of a point on the trajectory at time t
    Vector2 CalculateTrajectoryPoint(float t)
    {
        Vector2 startPos = FirePoint.position;
        Vector2 initialVelocity = direction.normalized * FireForce;
        return startPos + initialVelocity * t + 0.5f * Physics2D.gravity * (t * t);
    }
}
