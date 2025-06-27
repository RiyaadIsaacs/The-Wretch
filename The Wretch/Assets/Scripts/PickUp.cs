using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Check collision of player and pick up object and activate effect
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            RotatePickUp rotatePickUp = collision.gameObject.GetComponent<RotatePickUp>();
            if (rotatePickUp != null)
            {
                PickUpType pickupType = rotatePickUp.pickUps;
                ApplyPickupEffect(pickupType);
            }

            // Destroy pick up object
            Destroy(collision.gameObject);
        }
    }

    private void ApplyPickupEffect(PickUpType pickupType)
    {
        switch (pickupType)
        {
            case PickUpType.WheelPickup:
                PlayerControl.instance.hasWheel = true;
                break;
            case PickUpType.SwordPickup:
                PlayerControl.instance.playerAttack += 10;
                break;
            case PickUpType.HealthPickup:
                PlayerControl.instance.currentPlayerHealth = PlayerControl.instance.playerHealthMax;
                break;
            case PickUpType.TreasurePickup:
                PlayerControl.instance.playerMoney += 100;
                PlayerControl.instance.textComponent.text = PlayerControl.instance.playerMoney.ToString();
                break;

            case PickUpType.TentaclePickup:
                // Activate UI that says player wins
                Debug.Log("Player wins!");
                break;
        }
    }
}
