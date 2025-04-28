using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class YellowButton : MonoBehaviour
{
    public AudioSource soundbutton;
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
        // 🔊 Lancer le son si la source audio est définie
        if (soundbutton != null)
        {
            soundbutton.Play();
        }
        else
        {
            Debug.LogWarning("🔇 Aucun sondbutton assigné !");
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
}
