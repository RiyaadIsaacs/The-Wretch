using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Check collision of player and pick up object and activate effect
    private void OnCollisionEnter(Collision collision)
    {
        // Check name of object and activate effect
        switch (collision.gameObject.name)
        {
            // Check if the player has the wheel for drawbridge
            case "Wheel pickup":
                Destroy(gameObject);
                PlayerControl.instance.hasWheel = true;
                break;

            // Increase player attack on picking up a sword
            case "Sword pickup":
                Destroy(gameObject);
                PlayerControl.instance.playerAttack += 10;
                break;

            // Heal player to Max HP on picking up a Heart
            case "Health pickup":
                Destroy(gameObject);
                PlayerControl.instance.currentPlayerHealth = PlayerControl.instance.playerHealthMax;
                break;
                
            // Activate UI that says player wins
            case "Tentacle pickup":
                Destroy(gameObject);

                break;
        }
    }
}
