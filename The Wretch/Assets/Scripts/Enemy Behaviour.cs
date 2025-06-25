using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Trigger for the enemies to walk to and attack the player
    private bool triggerAggro;

    // Empty Object for spawn point for the enemies
    [SerializeField] private GameObject enemySpawnPoint;

    // Empty Object for attack range - may not be needed 
    [SerializeField] private GameObject attackRange;

    // Transform of the player, assigned in the inspector, for enemies to target
    [SerializeField] private Transform playerTransform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Move the enemy to the spawn point and cause them to attack the player
            transform.position = enemySpawnPoint.transform.position;
            triggerAggro = true;
        }
    }

    // WORK IN PROGRESS
    private void Update()
    {
        if (triggerAggro)
        {
            float enemySpeed = 10 * Time.deltaTime; // adjust this value to control the enemy's speed
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, enemySpeed);

            // You can also add a check to see if the enemy is close enough to attack
            if (Vector3.Distance(transform.position, playerTransform.position) < 1f) // adjust this value to control the attack range
            {
                // Attack the player!
            }
        }
    }
}
