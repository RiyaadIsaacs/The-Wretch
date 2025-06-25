using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    // Trigger if player falls off the bridge, restart the game or show a game over screen depending on ui
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.respawnPlayer(); // Respawn player for now but can add a game over screen

            Debug.Log("Player died");
        }
    }
}
