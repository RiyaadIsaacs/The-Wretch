using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    [SerializeField] private EnemyBehaviour[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (EnemyBehaviour enemy in enemies)
            {
                enemy.triggerAggro = true;
            }

            Debug.Log("Enemies in section 1 Aggro");

            Destroy(gameObject);
        }
    }
}
