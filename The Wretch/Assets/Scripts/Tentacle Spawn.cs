using UnityEngine;

public class TentacleSpawn : MonoBehaviour
{
    // Reference to the boss in inspector
    private EnemyBehaviour boss;

    // Reference to the empty at the spawn location
    [SerializeField] private GameObject spawnLocation;

    // Prefab for the tentacle to spawn
    [SerializeField] private GameObject tentaclePrefab;

    // Method to spawn the tentacle prefab when the boss dies
    public void OnBossDeath()
    {
        // Spawn the tentacle prefab at the specified location
        Instantiate(tentaclePrefab, spawnLocation.transform.position, Quaternion.identity);
    }
}
