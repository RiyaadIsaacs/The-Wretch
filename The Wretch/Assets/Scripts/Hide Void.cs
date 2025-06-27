using UnityEngine;

public class HideVoid : MonoBehaviour
{
    [SerializeField] private GameObject Void;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Void.SetActive(false);

            if (!Void.activeSelf)
            {
                Void.SetActive(true);
            }
        }
    }
}
