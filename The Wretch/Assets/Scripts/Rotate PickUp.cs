using UnityEngine;

public class RotatePickUp : MonoBehaviour
{
    // speed of rotation - SerializeField so it can easily be set in the inspector
    [SerializeField] private float rotationSpeed = 10f;

    // Get the enum for the type of pick up
    [SerializeField] public PickUpType pickUps;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the y axis at 10 degrees per second
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}