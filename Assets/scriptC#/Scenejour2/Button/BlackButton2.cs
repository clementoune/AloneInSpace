using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class BlackButton2 : MonoBehaviour
{
    public EquiperCasqueVR2 casque; // Référence au casque
    public AudioSource audioSource; // Référence au son à jouer
    public RedButton2 redButton; // Référence au bouton rouge
    public AudioSource VoixTrigger; // Référence au son de la voix à jouer

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable simpleInteractable;
    private Vector3 initialPosition; // Position de base du bouton

    private void Start()
    {
        simpleInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        initialPosition = transform.localPosition;
        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("⚠️ XRSimpleInteractable manquant sur le cube !");
        }
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        // Vérifier si le casque est déjà équipé avant de procéder
        if (!casque.estEquipe)
        {
            Debug.Log("⚠️ Le casque n'est pas encore équipé !");
            // Optionnellement, jouer un son d'avertissement si défini
            if (audioSource != null)
            {
                audioSource.Play();
            }
            return; // Ne pas continuer si le casque n'est pas équipé
        }

        // 🔊 Lancer le son si la source audio est définie
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("🔇 Aucun AudioSource assigné !");
        }
        

        // ▶️ Animation d'appui physique du bouton
        StartCoroutine(AnimateButtonPress());

        // Repositionner le casque si la référence existe
        if (casque != null)
        {
            casque.RepositionnerCasque();
        }
        else
        {
            Debug.LogError("❌ Aucun casque assigné dans l'inspecteur !");
        }
    }

    private IEnumerator AnimateButtonPress()
    {
        // Descendre le bouton
        transform.localPosition += new Vector3(0, -0.01f, 0);
        yield return new WaitForSeconds(0.2f); // Durée de l'appui
        // Revenir à la position initiale
        transform.localPosition = initialPosition;
    }
}
