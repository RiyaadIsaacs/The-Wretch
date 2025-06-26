using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Trigger for the enemies to walk to and attack the player
    public bool triggerAggro = false;

    // Transform of the player, assigned in the inspector, for enemies to target
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float health = 100f;
    [SerializeField] private float enemyAttack = 10f;

    // Attack variables
    private PlayerControl playerInRange;
    private float attackDelay = 1f; // adjust this value to control the attack delay
    private float timeSinceLastAttack = 0f;

    private void Update()
    {
        // First, check if the player still exists. If not, do nothing.
        if (playerTransform == null)
        {
            return;
        }
        if (triggerAggro)
        {
            float enemySpeed = 5f * Time.deltaTime; // adjust this value to control the enemy's speed
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed);

            timeSinceLastAttack += Time.deltaTime;
            if (timeSinceLastAttack >= attackDelay)
            {
                playerInRange.PlayerTakeDamage(enemyAttack);
                playerInRange = null;
                timeSinceLastAttack = 0f;
            }
        }
    }

    public void EnemyTakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Trigger for attack range
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = other.GetComponent<PlayerControl>();
        }
    }

    // Trigger for leaving attack range
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInRange = null;
        }
    }
}
