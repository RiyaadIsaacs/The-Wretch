using UnityEngine;

public class HideVoid : MonoBehaviour
{
    [SerializeField] private GameObject Void;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Void.SetActive(false);
        }
    }
}