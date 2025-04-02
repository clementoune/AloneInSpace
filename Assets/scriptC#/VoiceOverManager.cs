using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceOverManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI dialogueText; // Texte affich� � l'�cran
    public string message = "Bienvenue � bord du vaisseau F2-33 ! Je suis votre intelligence artificielle de bord, con�u pour vous assister dans cette mission d'exploration du syst�me solaire.\r\n\r\nVotre vaisseau, une navette de classe Stellarion, est �quip� des derni�res technologies pour analyser les plan�tes et leurs myst�res.\r\n\r\nPour commencer, explorez votre environnement. D�placez-vous dans la navette et retrouvez votre tableau de bord au centre du vaisseau afin d�acc�der aux commandes principales.\r\n\r\nBonne mission, Capitaine."; // Texte � afficher
    public float delayBeforeStart = 2f; // D�lai avant de commencer
    public float displayDuration = 10f; // Dur�e d'affichage par groupe de mots (augmente pour ralentir)
    public int wordsPerChunk = 5; // Nombre de mots affich�s � la fois

    void Start()
    {
        StartCoroutine(PlayAudioWithText());
    }

    IEnumerator PlayAudioWithText()
    {
        yield return new WaitForSeconds(delayBeforeStart);

        string[] words = message.Split(' '); // D�coupe le message en mots
        dialogueText.text = ""; // Efface le texte avant l'affichage
        dialogueText.gameObject.SetActive(true);
        audioSource.Play();

        for (int i = 0; i < words.Length; i += wordsPerChunk)
        {
            string chunk = string.Join(" ", words, i, Mathf.Min(wordsPerChunk, words.Length - i)); // S�lectionne 5 mots � afficher
            dialogueText.text = chunk; // Affiche les mots actuels
            yield return new WaitForSeconds(displayDuration); // Attente avant d'afficher les suivants
        }

        yield return new WaitForSeconds(audioSource.clip.length - displayDuration); // Attend la fin du son s'il est plus long

        dialogueText.gameObject.SetActive(false); // Cacher le texte apr�s la lecture
    }
}
