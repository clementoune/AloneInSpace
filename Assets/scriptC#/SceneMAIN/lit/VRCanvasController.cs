using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit; // Assurez-vous d'inclure ce namespace pour XR interaction
using System.Collections;

public class VRCanvasController : MonoBehaviour
{
    public Image fadeImage; // Image noire pour l'effet de fondu
    public float fadeDuration = 2f; // Durée du fondu
    public float darkDuration = 3f; // Durée pendant laquelle le Canvas reste sombre
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable; // Référence à l'objet interactable

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>(); // Récupère la référence à l'XRGrabInteractable
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrab); // Ajoute un écouteur pour l'événement selectEntered
            Debug.Log("Listener ajouté à l'objet interactable.");
        }
        else
        {
            Debug.LogError("L'objet ne possède pas de XRGrabInteractable.");
        }
    }

    // Fonction appelée lors de l'interaction de l'objet
    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("Objet attrapé !"); // Message de débogage pour vérifier si l'événement est bien déclenché
        // Démarre le fondu et fait l'effet sombre pendant 3 secondes
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

        // Phase 2: Attendre 3 secondes avec l'écran sombre
        Debug.Log("Canvas sombre pendant 3 secondes.");
        yield return new WaitForSeconds(darkDuration);

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
