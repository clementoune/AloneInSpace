using UnityEngine;

public class PositionningFusible2 : MonoBehaviour
{
    public Transform positionFinale;

    public bool enPosition = false; // Variable pour savoir si le fusible est positionné


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fuse"))
        {
            Debug.Log("Fusible détecté dans le trigger.");
            enPosition = true;
            // Repositionne le fusible
            other.transform.position = positionFinale.position;
            other.transform.rotation = positionFinale.rotation;

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
