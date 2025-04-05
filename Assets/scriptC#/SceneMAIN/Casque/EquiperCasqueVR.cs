using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EquiperCasqueVR : MonoBehaviour
{
    public Transform pointAttach; // Tête du joueur
    public Transform socleCasque; // Socle où replacer le casque
    public AudioSource sonCasqueEquipe; // Son lorsqu'on met le casque
    public RedButton redButton; // Référence au script RedButton
    public AudioSource VoixTrigger;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool estEquipe = false;
    private bool aEquiperLecasque = false;
    private bool aReposer = false;

    public bool AEteEquipe { get { return aEquiperLecasque; } private set { aEquiperLecasque = value; } }
    public bool AEteRepose { get { return aReposer; } private set { aReposer = value; } }

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        setGrabbable(false);  // Désactiver le grab interactable au départ
        
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(EquiperCasqueSurTete);
        }
        else
        {
            Debug.LogError("⚠️ XRGrabInteractable manquant sur le casque !");
        }
    }

    private void EquiperCasqueSurTete(SelectExitEventArgs args)
    {
        if (redButton != null && !redButton.isPressed)
        {
            VoixTrigger.Play();
            Debug.Log("🔊 Voix déclenchée !");
            return;
        }

        if (!estEquipe)
        {
            Debug.Log("🎧 Casque équipé !");
            transform.SetParent(pointAttach);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            estEquipe = true;
            grabInteractable.enabled = false;  // Désactive la fonctionnalité de grab
            AEteEquipe = true;

            if (sonCasqueEquipe != null)
            {
                sonCasqueEquipe.Play();
            }
            Debug.Log("etat du casque : " + AEteEquipe);
        }
    }

    public void RepositionnerCasque()
    {
        Debug.Log("🔄 Tentative de repositionnement du casque...");

        if (estEquipe)
        {
            Debug.Log("📌 Casque repositionné sur le socle !");
            estEquipe = false;
            aReposer = true;  // Le casque a été reposé
            transform.SetParent(null);
            transform.position = socleCasque.position;
            transform.rotation = socleCasque.rotation;
            grabInteractable.enabled = true;  // Réactive le grab interactable
            Debug.Log("🔄 Le casque a été reposé !");
        }
        else
        {
            Debug.Log("⚠️ Le casque n'était pas équipé, repositionnement inutile.");
        }
    }

    public void setGrabbable(bool grabbable)
    {
        if (grabInteractable != null)
        {
            grabInteractable.enabled = grabbable;  // Assurez-vous que grabbable est un booléen
            Debug.Log(grabbable ? "🔵 Le casque est maintenant attrapable." : "🔴 Le casque n'est plus attrapable.");
        }
        else
        {
            Debug.LogError("⚠️ XRGrabInteractable manquant sur l'objet !");
        }
    }
}
