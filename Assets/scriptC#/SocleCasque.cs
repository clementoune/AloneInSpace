using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocleCasque : MonoBehaviour
{
    public EquiperCasqueVR casque; // Référence au casque
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable != null)
        {
            // Ajouter l'écouteur d'événements de sélection (quand l'utilisateur interagit avec le bouton)
            grabInteractable.selectEntered.AddListener(OnButtonPressed);
            grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        }
        else
        {
            Debug.LogError("⚠️ XRGrabInteractable manquant sur le cube !");
        }
    }

    // Cette méthode est appelée quand le cube (le bouton) est pressé
    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("🟢 Bouton Pressé, repositionnement du casque...");
        if (casque != null)
        {
            casque.RepositionnerCasque();
        }
        else
        {
            Debug.LogError("❌ Aucun casque assigné dans l'inspecteur !");
        }
    }

    // Cette méthode est appelée lorsqu'un objet entre en "hover" avec le cube (affichage visuel pour le survol)
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log("🛑 Le joueur survole le cube.");
    }
}
