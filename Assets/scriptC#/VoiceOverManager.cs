using UnityEngine;
using TMPro;
using System.Collections;

public class VoiceOverManager : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI dialogueText; // Texte affich� � l'�cran
    public string message = "Bienvenue dans le jeu !"; // Texte � afficher
    public float delayBeforeStart = 2f; // D�lai avant de commencer

    void Start()
    {
        StartCoroutine(PlayAudioWithText());
    }

    IEnumerator PlayAudioWithText()
    {
        // Attente avant de commencer
        yield return new WaitForSeconds(delayBeforeStart);

        // Afficher le texte et jouer le son
        dialogueText.text = message;
        dialogueText.gameObject.SetActive(true);
        audioSource.Play();

        // Attendre la fin du son
        yield return new WaitForSeconds(audioSource.clip.length);

        // Cacher le texte apr�s le son
        dialogueText.gameObject.SetActive(false);
    }
}
