using UnityEngine;

public class RocketAutoLaunch : MonoBehaviour
{
    public Rigidbody rocketRigidbody;  // Assigner le Rigidbody de la fusée
    public float launchForce = 1500f;   // Force de lancement pour reculer
    public float delayBeforeLaunch = 10f;  // Délai avant le recul (5 secondes)

    void Start()
    {
        // Vérifier si le Rigidbody est bien assigné
        if (rocketRigidbody == null)
        {
            rocketRigidbody = GetComponent<Rigidbody>();
        }

        // Geler la vitesse de la fusée pour la rendre immobile pendant 5 secondes
        rocketRigidbody.linearVelocity = Vector3.zero;
        rocketRigidbody.angularVelocity = Vector3.zero;

        // Lancer la fusée après un délai de delayBeforeLaunch secondes
        Invoke("LaunchRocket", delayBeforeLaunch);
    }

    void LaunchRocket()
    {
        Debug.Log(" Décollage... Recul !");

        // Assurer que la fusée ne soit pas cinématique et prête à recevoir des forces
        rocketRigidbody.isKinematic = false;

        // Appliquer une force pour reculer : on utilise -transform.forward pour obtenir la direction opposée
        Vector3 launchDirection = transform.forward; // Reculer dans la direction opposée à l'avant de la fusée

        // Appliquer la force dans cette direction pour que la fusée recule
        rocketRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
    }
}
