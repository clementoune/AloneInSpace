using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EquiperCasqueVR : MonoBehaviour
{
    public Transform pointAttach; // Référence à la tête du joueur
    public AudioSource equipSound; // Son à jouer lors de l'équipement du casque

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool estEquipe = false;

    private void Start()
    {
        // Récupérer le XRGrabInteractable et lui assigner une fonction lors du drop
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(EquiperCasque);
    }

    private void EquiperCasque(SelectExitEventArgs args)
    {
        if (!estEquipe)
        {
            transform.SetParent(pointAttach); // Attacher le casque à la tête
            transform.localPosition = Vector3.zero; // Aligner la position
            transform.localRotation = Quaternion.identity; // Aligner la rotation
            estEquipe = true;

            // Jouer le son si une source audio est assignée
            if (equipSound != null)
            {
                equipSound.Play();
            }

            // Désactiver le grab pour éviter de retirer le casque
            grabInteractable.enabled = false;
        }
    }
}
