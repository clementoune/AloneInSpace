using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceOverManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI dialogueText;
    public string message = "Bienvenue � bord du vaisseau F2-33 ! Je suis votre intelligence artificielle de bord, con�u pour vous assister dans cette mission d'exploration du syst�me solaire.\r\n\r\nVotre vaisseau, une navette de classe Stellarion, est �quip� des derni�res technologies pour analyser les plan�tes et leurs myst�res.\r\n\r\nPour commencer, explorez votre environnement. D�placez-vous dans la navette et retrouvez votre tableau de bord au centre du vaisseau afin d�acc�der aux commandes principales.\r\n\r\nBonne mission, Capitaine.";
    public float delayBeforeStart = 2f;
    public float displayDuration = 10f;
    public int wordsPerChunk = 5;
    public float fadeOutDuration = 2f; // Dur�e du fondu de sortie

    void Start()
    {
        StartCoroutine(PlayAudioWithText());
    }

    IEnumerator PlayAudioWithText()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        if (audioSource.clip == null)
        {
            Debug.LogWarning("Aucun clip audio assign� � AudioSource !");
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

        // D�but du fondu de sortie
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
        dialogueText.color = new Color(textColor.r, textColor.g, textColor.b, 1f); // R�initialiser la transparence
    }
}
