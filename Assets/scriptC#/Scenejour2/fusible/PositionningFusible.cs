using UnityEngine;

public class PositionningFusible : MonoBehaviour
{
    public Transform positionFinale;
    public bool enPosition = false; // Variable pour savoir si le fusible est positionné
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fuse"))
        {
            // Repositionne le fusible
            other.transform.position = positionFinale.position;
            other.transform.rotation = positionFinale.rotation;
            enPosition = true; // Met à jour l'état du fusible
            // Optionnel : désactiver le grab (si tu veux empêcher de reprendre le fusible)
            var grabInteractable = other.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }

            // Optionnel : tu peux aussi désactiver la physique si tu veux que le fusible reste en place
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }
}
