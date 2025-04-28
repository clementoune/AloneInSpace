using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RespawnOnTouchFilet : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;

    void Start()
    {
        // Sauvegarde la position et la rotation de d�part
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("FiletDeSecurite"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Reset position et rotation
        transform.position = initialPosition;
        transform.rotation = initialRotation;

        // Reset vitesse pour éviter que l'objet continue de bouger
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
