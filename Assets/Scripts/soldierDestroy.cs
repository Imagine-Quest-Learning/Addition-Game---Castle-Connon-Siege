using UnityEngine;

public class SoldierDestroy : MonoBehaviour
{
    [Header("HP Settings")]
    public int maxHP = 1;
    private int currentHP;

    [Header("Explosion Prefab")]
    public GameObject boomPrefab;

    private void Start()
    {
        currentHP = maxHP;
        if (boomPrefab == null)
            boomPrefab = Resources.Load<GameObject>("Boom1");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ball"))
            return;

        collision.gameObject.tag = "UsedBall";

        float collisionMagnitude = collision.relativeVelocity.magnitude;
        int damage = (int)(collisionMagnitude * 8);
        currentHP -= damage;

        if (currentHP <= 0)
        {
            PlayExplosionEffect();
            CheckAnswerAndShowDialog();
            Destroy(gameObject);
        }
    }

    private void CheckAnswerAndShowDialog()
    {
        ShieldAnswer shield = GetComponent<ShieldAnswer>();
        if (shield == null) return;

        if (QABoardManager.Instance != null)
            QABoardManager.Instance.CheckAnswer(shield.answer);
    }

    private void PlayExplosionEffect()
    {
        Debug.Log("PlayExplosionEffect called");
        if (boomPrefab == null) return;

        GameObject explosion = Instantiate(boomPrefab, transform.position, Quaternion.identity);
        Debug.Log("Explosion prefab instantiated: " + explosion.name);
        if (!explosion) return;

        Animator boomAnimator = explosion.GetComponent<Animator>();
        if (boomAnimator != null)
        {
            Debug.Log("Animator found: " + (boomAnimator != null));
            boomAnimator.SetTrigger("explode");
        }


        Destroy(explosion, 5f);
    }
}
