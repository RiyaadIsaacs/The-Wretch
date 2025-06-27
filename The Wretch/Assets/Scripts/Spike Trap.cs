using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerControl>().PlayerTakeDamage(5f);
        }
    }
}
