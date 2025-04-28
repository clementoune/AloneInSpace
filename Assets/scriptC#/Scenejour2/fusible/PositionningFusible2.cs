using UnityEngine;

public class PositionningFusible2 : MonoBehaviour
{
    public Transform positionFinale;
    public AudioSource VoixTrigger;
    public RedButton2 redButton; // Référence au script RedButton

    public bool enPosition = false; // Variable pour savoir si le fusible est positionné

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fuse"))
        {
            enPosition = true;
            // Repositionne le fusible
            other.transform.position = positionFinale.position;
            other.transform.rotation = positionFinale.rotation;
            var grabInteractable = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
}
