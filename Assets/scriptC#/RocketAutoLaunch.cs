using UnityEngine;

public class RocketAutoLaunch : MonoBehaviour
{
    public Rigidbody rocketRigidbody;  // Assigner le Rigidbody de la fusée
    public float launchForce = 1500f;   // Force de lancement pour reculer
    public float delayBeforeLaunch = 15f;  // Délai avant le décollage (15 secondes dans le jeu)
    public AudioSource launchAudio;  // Composant AudioSource pour le son de lancement

    void Start()
    {
        // Vérifier si le Rigidbody est bien assigné
        if (rocketRigidbody == null)
        {
            rocketRigidbody = GetComponent<Rigidbody>();
        }

        // Vérifier si l'AudioSource est bien assigné
        if (launchAudio == null)
        {
            launchAudio = GetComponent<AudioSource>();
        }

        // Geler la vitesse de la fusée pour la rendre immobile pendant 15 secondes
        rocketRigidbody.linearVelocity = Vector3.zero;
        rocketRigidbody.angularVelocity = Vector3.zero;

        // Lancer la fusée après un délai de delayBeforeLaunch secondes
        Invoke("LaunchRocket", delayBeforeLaunch);
    }

    void LaunchRocket()
    {
        Debug.Log("Décollage... !");

        // Jouer le son de lancement
        if (launchAudio != null)
        {
            launchAudio.Play();
        }
        else
        {
            Debug.LogWarning("Aucun AudioSource trouvé sur l'objet de la fusée !");
        }

        // Assurer que la fusée ne soit pas cinématique et prête à recevoir des forces
        rocketRigidbody.isKinematic = false;

        // Appliquer une force pour reculer : on utilise transform.forward pour aller dans la direction avant
        Vector3 launchDirection = transform.forward;

        // Appliquer la force dans cette direction pour que la fusée recule
        rocketRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }
}
