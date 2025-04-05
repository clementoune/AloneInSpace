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
    // Propriété pour savoir si le casque a été équipé
    public bool AEteEquipe { get { return aEquiperLecasque; } private set { aEquiperLecasque = value; } }

    // Propriété pour savoir si le casque a été reposé
    public bool AEteRepose { get { return aReposer; } private set { aReposer = value; } }

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
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
        if(redButton != null && !redButton.isPressed)
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
            grabInteractable.enabled = false;
            AEteEquipe = true; // Utilisation de la propriété

            if (sonCasqueEquipe != null)
            {
                sonCasqueEquipe.Play();
            }
        }
    }

    public void RepositionnerCasque()
    {
        Debug.Log("🔄 Tentative de repositionnement du casque...");

        if (estEquipe)
        {
            Debug.Log("📌 Casque repositionné sur le socle !");
            Debug.Log("🔄 Le casque a été reposé !");
            AEteRepose = true; // Le casque a été reposé
            estEquipe = false;
            transform.SetParent(null);
            transform.position = socleCasque.position;
            transform.rotation = socleCasque.rotation;
            grabInteractable.enabled = true;
        }
        else
        {
            Debug.Log("⚠️ Le casque n'était pas équipé, repositionnement inutile.");
        }
    }
}