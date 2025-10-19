using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public float speed = 1f;

    public bool canMove = true;

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    void Update()
    {
        if (!canMove) return;
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        transform.position += movement * Time.deltaTime;
    }
}
