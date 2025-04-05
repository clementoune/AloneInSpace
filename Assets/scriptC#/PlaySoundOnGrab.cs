using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class PlaySoundOnGrab : MonoBehaviour
{
    public AudioSource soundbutton;
    public AudioSource audioSource2;
    public GameObject canvas;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Vector3 initialPosition; // Position de base du bouton

    private void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        initialPosition = transform.localPosition;

        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnButtonPressed);
            grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        }
        else
        {
            Debug.LogError("⚠️ XRGrabInteractable manquant sur le cube !");
        }

        // Assurez-vous que le canvas est désactivé au départ
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("🟢 Bouton Pressé, repositionnement du casque...");

        // 🔊 Lancer le son si la source audio est définie
        if (soundbutton != null)
        {
            soundbutton.Play();
        }
        else
        {
            Debug.LogWarning("🔇 Aucun sondbutton assigné !");
        }

        // 🔊 Lancer le son si la source audio est définie
        if (audioSource2 != null)
        {
            audioSource2.Play();
        }
        else
        {
            Debug.LogWarning("🔇 Aucun audioSource2 assigné !");
        }

        // Afficher le canvas
        if (canvas != null)
        {
            canvas.SetActive(true);
        }

        // ▶️ Animation d'appui physique du bouton
        StartCoroutine(AnimateButtonPress());
    }

    private IEnumerator AnimateButtonPress()
    {
        // Descendre le bouton
        transform.localPosition += new Vector3(0, -0.01f, 0);
        yield return new WaitForSeconds(0.2f); // Durée de l'appui
        // Revenir à la position initiale
        transform.localPosition = initialPosition;
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Vous pouvez ajouter des effets de survol ici
        Debug.Log("🟡 Hover sur le bouton !");
    }
}
