using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class GreenButton : MonoBehaviour
{
    public AudioSource soundbutton;
    public VRCanvasController canvasController; // 🔗 Référence vers le script VRCanvasController

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Vector3 initialPosition;

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
            Debug.LogError("⚠️ XRGrabInteractable manquant sur le bouton !");
        }
    }

    private void OnButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("🟢 Bouton Pressé, arrêt de l'alarme...");

        if (soundbutton != null)
        {
            soundbutton.Play();
        }

        // 🛑 Appeler StopAlarm si le canvasController est bien défini
        if (canvasController != null)
        {
            canvasController.StopAlarm();
        }
        else
        {
            Debug.LogWarning("❌ Aucun VRCanvasController assigné !");
        }

        StartCoroutine(AnimateButtonPress());
    }

    private IEnumerator AnimateButtonPress()
    {
        transform.localPosition += new Vector3(0, -0.01f, 0);
        yield return new WaitForSeconds(0.2f);
        transform.localPosition = initialPosition;
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log("🟡 Hover sur le bouton !");
    }
}