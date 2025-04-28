using UnityEngine;

public class RespawnOnTouchFilet : MonoBehaviour
{
    public Transform respawnPoint; // Tu peux glisser un empty GameObject ici dans l'inspecteur

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (respawnPoint == null)
        {
            // Si aucun respawnPoint assigné, utiliser la position actuelle
            respawnPoint = new GameObject("DefaultRespawnPoint").transform;
            respawnPoint.position = transform.position;
            respawnPoint.rotation = transform.rotation;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FiletDeSecurite"))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Remet l'objet à sa position de respawn
        transform.position = respawnPoint.position;
        transform.rotation = respawnPoint.rotation;

        // Remet les vitesses à zéro
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
