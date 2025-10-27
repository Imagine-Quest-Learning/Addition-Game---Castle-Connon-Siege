using UnityEngine;

public class FloatAnimationGamePage : MonoBehaviour
{
    public float floatAmount = 0.2f;
    public float floatSpeed = 2f;
    public float rotateSpeed = 15f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + new Vector3(0f, Mathf.Sin(Time.time * floatSpeed) * floatAmount, 0f);
        transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }
}
