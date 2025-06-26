using UnityEngine;

public class SetBridge : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (PlayerControl.instance.hasWheel)
        {
            Destroy(gameObject);
        }
    }
}
