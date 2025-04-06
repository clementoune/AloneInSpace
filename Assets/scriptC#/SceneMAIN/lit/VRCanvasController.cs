using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;

public class VRCanvasController : MonoBehaviour
{
    public static int numjours = 1;
    public Image fadeImage;
    public TextMeshProUGUI messageText;
    public float fadeDuration = 2f;
    public float darkDuration = 3f;
    public AudioSource pasPret;
    public AudioSource audioSource;
    public AudioSource alarmAudioSource; // 🔊 Alarme
    public List<Light> alarmLights; // 💡 Lumières rouges d'alarme
    public RedButton redButton;

    private Coroutine alarmCoroutine;

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

        // Assurez-vous que les lumières d'alarme sont éteintes au début
        foreach (Light light in alarmLights)
        {
            light.enabled = false;
        }
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
        redButton.setGrabbable(true);
        StartCoroutine(FadeToDark());
        Debug.Log("Nombre de jours : " + numjours);

        CheckMissions.finishedday = false;
        FindFirstObjectByType<CheckMissions>().ResetMissionState();
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
        messageText.gameObject.SetActive(true);
        messageText.text = "Jour " + numjours;

        yield return new WaitForSeconds(darkDuration);
        messageText.gameObject.SetActive(false);

        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        StartAlarm(); // 🚨 Lancer l'alarme après le réveil
    }

    private void StartAlarm()
    {
        Debug.Log("🚨 Alarme déclenchée !");
        if (alarmAudioSource != null)
            alarmAudioSource.Play();

        if (alarmCoroutine != null)
            StopCoroutine(alarmCoroutine);

        alarmCoroutine = StartCoroutine(FlashAlarmLights());
    }

    public void StopAlarm()
    {
        Debug.Log("🟢 Alarme arrêtée.");
        if (alarmAudioSource != null)
            alarmAudioSource.Stop();

        if (alarmCoroutine != null)
            StopCoroutine(alarmCoroutine);

        foreach (Light light in alarmLights)
        {
            light.enabled = false;
        }
    }

    private IEnumerator FlashAlarmLights()
    {
        while (true)
        {
            foreach (Light light in alarmLights)
                light.enabled = !light.enabled;

            yield return new WaitForSeconds(0.5f); // vitesse de clignotement
        }
    }
}