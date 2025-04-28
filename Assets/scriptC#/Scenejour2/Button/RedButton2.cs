using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class RedButton2 : MonoBehaviour
{
    public AudioSource soundbutton;
    public AudioSource audioSource2;
    public GameObject canvas;
    public bool isPressed = false;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable simpleInteractable;
    private Vector3 initialPosition;

    private void Start()
    {
        simpleInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

        if (simpleInteractable != null)
        {
            simpleInteractable.selectEntered.AddListener(OnButtonPressed);
        }
        else
        {
            Debug.LogError("⚠️ XRSimpleInteractable manquant sur le cube !");
        }

        // Assurez-vous que le canvas est désactivé au départ
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        isPressed = true;        
        // Afficher le canvas
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
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
    public void setGrabbable(bool grabbable)
    {
        if (simpleInteractable != null)
        {
            simpleInteractable.enabled = grabbable;  // Assurez-vous que grabbable est un booléen
            Debug.Log(grabbable ? "🔵 est maintenant attrapable." : "🔴 Le casque n'est plus attrapable.");
        }
        else
        {
            Debug.LogError("⚠️ XRGrabInteractable manquant sur l'objet !");
        }
    }
}
