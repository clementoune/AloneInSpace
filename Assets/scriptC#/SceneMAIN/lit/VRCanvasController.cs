using UnityEngine;
using UnityEngine.UI; // Namespace pour Image
using TMPro; // Namespace pour TextMeshPro
using UnityEngine.XR.Interaction.Toolkit; // Assurez-vous d'inclure ce namespace pour XR interaction
using System.Collections;

public class VRCanvasController : MonoBehaviour
{
    public Image fadeImage; // Image noire pour l'effet de fondu
    public TextMeshProUGUI messageText; // Texte MeshPro invisible à afficher pendant le fondu
    public float fadeDuration = 2f; // Durée du fondu
    public float darkDuration = 3f; // Durée pendant laquelle le Canvas reste sombre
    public AudioSource pasPret; 
    public AudioSource audioSource; // Référence à l'AudioSource pour jouer le son
    public RedButton redButton; // Référence au bouton rouge

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable; // Référence à l'objet interactable

    void Start()
    {
        // Récupère la référence à l'XRGrabInteractable
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrab); // Ajoute un écouteur pour l'événement selectEntered
            Debug.Log("Listener ajouté à l'objet interactable.");
        }
        else
        {
            Debug.LogError("L'objet ne possède pas de XRGrabInteractable.");
        }

        // Cache le texte au début
        messageText.gameObject.SetActive(false);
    }

    // Fonction appelée lors de l'interaction de l'objet
    void OnGrab(SelectEnterEventArgs args)
    {
        if(redButton != null && !redButton.isPressed)
        {
            pasPret.Play(); 
            Debug.Log("🔴 Le bouton rouge n'est pas encore pressé !");
            return; // Ne pas continuer si le bouton rouge n'est pas pressé
        }
        Debug.Log("Objet attrapé !"); // Message de débogage pour vérifier si l'événement est bien déclenché
        // Démarre le fondu et fait l'effet sombre pendant 3 secondes
        audioSource.Play(); 
        StartCoroutine(FadeToDark());
    }

    // Coroutine qui gère l'effet de fondu à l'état sombre
    IEnumerator FadeToDark()
    {
        Debug.Log("Début du fondu vers le sombre.");
        // Phase 1: Transition vers le fondu sombre (3 secondes)
        float elapsedTime = 0f;
        while (elapsedTime < darkDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / darkDuration); // Lerp pour faire un fondu
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Affiche le texte lorsque l'écran devient sombre
        messageText.gameObject.SetActive(true);
        messageText.text = "Jour 2"; // Changez le texte si nécessaire
        Debug.Log("Canvas sombre pendant 3 secondes.");

        // Attendre 3 secondes avec l'écran sombre
        yield return new WaitForSeconds(darkDuration);

        // Cache le texte après 3 secondes
        messageText.gameObject.SetActive(false);

        // Phase 3: Fondu vers la transparence (2 secondes)
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Fondu terminé.");
    }
}
