using UnityEngine;

public class ShowVoid : MonoBehaviour
{
    [SerializeField] private GameObject Void;

    private void OnTriggerExit(Collider other)
{
    if (other.tag == "Player")
    {
        Void.SetActive(true);
    }
}
}
