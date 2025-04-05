using UnityEngine;
  // Si tu utilises l'XR Interaction Toolkit

public class CableConnectionVR : MonoBehaviour
{
    private bool isHeld = false;
    private string correctTag;  // Le tag du bon connecteur

    private void Start()
    {
        // Le câble doit avoir le même tag que le connecteur correct
        correctTag = gameObject.tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isHeld && other.CompareTag(correctTag))
        {
            Debug.Log("Bonne connexion !");
            AttachToConnector(other.transform);
        }
    }

    private void AttachToConnector(Transform connector)
    {
        // Fixe la position du câble sur le connecteur
        transform.position = connector.position;
        transform.rotation = connector.rotation;

        // Désactive les interactions
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().enabled = false;

        // Effet visuel / son
        GetComponent<Renderer>().material.color = Color.green;  // Change la couleur du câble
    }

    // XR Interaction : Détecte si le joueur tient le câble
    public void OnSelectEnter()
    {
        isHeld = true;
    }

    public void OnSelectExit()
    {
        isHeld = false;
    }
}
