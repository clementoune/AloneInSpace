using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceOverManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI dialogueText;
    public string message = "Bienvenue à bord du vaisseau F2-33 ! Je suis votre intelligence artificielle de bord, conçu pour vous assister dans cette mission d'exploration du système solaire.\r\n\r\nVotre vaisseau, une navette de classe Stellarion, est équipé des dernières technologies pour analyser les planètes et leurs mystères.\r\n\r\nPour commencer, explorez votre environnement. Déplacez-vous dans la navette et retrouvez votre tableau de bord au centre du vaisseau afin d’accéder aux commandes principales.\r\n\r\nBonne mission, Capitaine.";
    public float delayBeforeStart = 2f;
    public float displayDuration = 10f;
    public int wordsPerChunk = 5;
    public float fadeOutDuration = 2f; // Durée du fondu de sortie

    void Start()
    {
        StartCoroutine(PlayAudioWithText());
    }

    IEnumerator PlayAudioWithText()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        if (audioSource.clip == null)
        {
            Debug.LogWarning("Aucun clip audio assigné à AudioSource !");
            yield break;
        }

        string[] words = message.Split(' ');
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(true);
        audioSource.Play();

        for (int i = 0; i < words.Length; i += wordsPerChunk)
        {
            string chunk = string.Join(" ", words, i, Mathf.Min(wordsPerChunk, words.Length - i));
            dialogueText.text = chunk;
            yield return new WaitForSeconds(displayDuration);
        }

        yield return new WaitForSeconds(audioSource.clip.length - displayDuration);

        // Début du fondu de sortie
        yield return StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color textColor = dialogueText.color;

        while (elapsedTime < fadeOutDuration)
        {
            textColor.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            dialogueText.color = textColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        dialogueText.gameObject.SetActive(false);
        dialogueText.color = new Color(textColor.r, textColor.g, textColor.b, 1f); // Réinitialiser la transparence
    }
}
