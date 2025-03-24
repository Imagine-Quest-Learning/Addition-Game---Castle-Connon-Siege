using UnityEngine;

public class SoldierDestroy : MonoBehaviour
{
    [Header("HP Settings")]
    public int maxHP = 1; // Maximum HP of the soldier
    private int currentHP; // Current HP

    [Header("Explosion Prefab")]
    public GameObject boomPrefab; // Explosion prefab loaded from Resources

    private void Start()
    {
        currentHP = maxHP;

        // Load explosion prefab from the Resources folder if not already assigned
        if (boomPrefab == null)
        {
            boomPrefab = Resources.Load<GameObject>("Boom1");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Only react to collisions with the Ball
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Calculate damage based on collision speed
            float collisionMagnitude = collision.relativeVelocity.magnitude;
            int damage = (int)(collisionMagnitude * 8);
            currentHP -= damage;

            // If soldier's HP is depleted, trigger explosion and destroy the soldier
            if (currentHP <= 0)
            {
                PlayExplosionEffect();
                CheckAnswerAndShowDialog();
                Destroy(gameObject);
            }
        }
    }

    // Checks the soldier's answer using the ShieldAnswer component
    private void CheckAnswerAndShowDialog()
    {
        ShieldAnswer shield = GetComponent<ShieldAnswer>();
        if (shield == null)
        {
            return;
        }

        int soldierAnswer = shield.answer;

        if (QABoardManager.Instance != null)
        {
            QABoardManager.Instance.CheckAnswer(soldierAnswer);
        }
    }

    // Instantiates and triggers the explosion animation
    private void PlayExplosionEffect()
    {
        if (boomPrefab == null)
        {
            return;
        }

        GameObject explosion = Instantiate(boomPrefab, transform.position, Quaternion.identity);

        if (explosion == null)
        {
            return;
        }

        Animator boomAnimator = explosion.GetComponent<Animator>();
        if (boomAnimator != null)
        {
            boomAnimator.SetTrigger("explode");
        }

        Destroy(explosion, 1f);
    }
}
