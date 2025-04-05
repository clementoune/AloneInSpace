using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceOverManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI dialogueText;

    [TextArea(5, 10)]
    public string message = "Bienvenue � bord du vaisseau F2-33 ! Je suis votre intelligence artificielle de bord, con�u pour vous assister dans cette mission d'exploration du syst�me solaire.\r\n\r\nVotre vaisseau, une navette de classe Stellarion, est �quip� des derni�res technologies pour analyser les plan�tes et leurs myst�res.\r\n\r\nPour commencer, explorez votre environnement. D�placez-vous dans la navette et retrouvez votre tableau de bord au centre du vaisseau afin d�acc�der aux commandes principales.\r\n\r\nBonne mission, Capitaine.";

    public float delayBeforeStart = 2f;
    public int wordsPerChunk = 5;
    public float fadeOutDuration = 2f;

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

        // Calcul du temps entre chaque bloc de mots
        float totalChunks = Mathf.Ceil((float)words.Length / wordsPerChunk);
        float timeBetweenChunks = audioSource.clip.length / totalChunks;

        for (int i = 0; i < words.Length; i += wordsPerChunk)
        {
            string chunk = string.Join(" ", words, i, Mathf.Min(wordsPerChunk, words.Length - i));
            dialogueText.text = chunk;
            yield return new WaitForSeconds(timeBetweenChunks);
        }

        // Attente restante si l'affichage s'est termin� avant la fin du clip
        float remainingTime = audioSource.clip.length - (timeBetweenChunks * totalChunks);
        if (remainingTime > 0)
            yield return new WaitForSeconds(remainingTime);

        // Lancer le fondu de sortie
        yield return StartCoroutine(FadeOutText());
    }

    IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color originalColor = dialogueText.color;

        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            dialogueText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // R�initialiser
        dialogueText.gameObject.SetActive(false);
        dialogueText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}
