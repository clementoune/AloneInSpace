using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceOverManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI dialogueText; // Texte affiché à l'écran
    public string message = "Bienvenue à bord du vaisseau F2-33 ! Je suis votre intelligence artificielle de bord, conçu pour vous assister dans cette mission d'exploration du système solaire.\r\n\r\nVotre vaisseau, une navette de classe Stellarion, est équipé des dernières technologies pour analyser les planètes et leurs mystères.\r\n\r\nPour commencer, explorez votre environnement. Déplacez-vous dans la navette et retrouvez votre tableau de bord au centre du vaisseau afin d’accéder aux commandes principales.\r\n\r\nBonne mission, Capitaine."; // Texte à afficher
    public float delayBeforeStart = 2f; // Délai avant de commencer
    public float displayDuration = 10f; // Durée d'affichage par groupe de mots (augmente pour ralentir)
    public int wordsPerChunk = 5; // Nombre de mots affichés à la fois

    void Start()
    {
        StartCoroutine(PlayAudioWithText());
    }

    IEnumerator PlayAudioWithText()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        string[] words = message.Split(' '); // Découpe le message en mots
        dialogueText.text = ""; // Efface le texte avant l'affichage
        dialogueText.gameObject.SetActive(true);
        audioSource.Play();

        for (int i = 0; i < words.Length; i += wordsPerChunk)
        {
            string chunk = string.Join(" ", words, i, Mathf.Min(wordsPerChunk, words.Length - i)); // Sélectionne 5 mots à afficher
            dialogueText.text = chunk; // Affiche les mots actuels
            yield return new WaitForSeconds(displayDuration); // Attente avant d'afficher les suivants
        }

        yield return new WaitForSeconds(audioSource.clip.length - displayDuration); // Attend la fin du son s'il est plus long

        dialogueText.gameObject.SetActive(false); // Cacher le texte après la lecture
    }
}
