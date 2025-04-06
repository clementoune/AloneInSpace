using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class VRCanvasController2 : MonoBehaviour
{
    public static int numjours = 1;
    public Image fadeImage;
    public TextMeshProUGUI messageText;
    public float fadeDuration = 2f;
    public float darkDuration = 3f;
    public AudioSource pasPret;
    public AudioSource audioSource;
    public RedButton2 redButton;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable interactable;

    void Start()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnGrab);
            Debug.Log("Listener ajouté à l'objet interactable.");
        }
        else
        {
            Debug.LogError("L'objet ne possède pas de XRGrabInteractable.");
        }

        messageText.gameObject.SetActive(false);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        if (!CheckMissions.finishedday)
        {
            Debug.Log("🚫 La journée n'est pas encore terminée !");
            pasPret.Play();
            return;
        }

        Debug.Log("✅ La journée est terminée, on peut aller se coucher !");
        audioSource.Play();
        StartCoroutine(FadeToDark());
        Debug.Log("Nombre de jours : " + numjours);
    }

    IEnumerator FadeToDark()
    {
        float elapsedTime = 0f;
        while (elapsedTime < darkDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / darkDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        numjours++;
        messageText.text = "Jour " + numjours;
        messageText.gameObject.SetActive(true);
        SceneManager.LoadScene("jour2");
    }
}
