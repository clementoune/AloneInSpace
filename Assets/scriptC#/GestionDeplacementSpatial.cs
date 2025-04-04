using UnityEngine;

public class GestionDeplacementSpatial : MonoBehaviour
{
    public float vitesseDeplacement = 5f;
    public float forceGravite = 9.81f; // Force de la gravité normale
    private bool peutSeDeplacer = false;
    private CharacterController characterController;
    private Vector3 vitesseVerticale = Vector3.zero; // Pour simuler la gravité

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Applique toujours une petite gravité pour le CharacterController
        if (!characterController.isGrounded && !peutSeDeplacer)
        {
            vitesseVerticale.y -= forceGravite * Time.deltaTime;
        } else if (peutSeDeplacer) {
            vitesseVerticale = Vector3.zero; // Pas de gravité dans l'espace
        } else if (characterController.isGrounded) {
            vitesseVerticale.y = -0.1f; // Petite force vers le bas pour rester au sol
        }

        Vector3 finalMouvement = Vector3.zero;

        if (peutSeDeplacer)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            finalMouvement = transform.TransformDirection(direction * vitesseDeplacement * Time.deltaTime);
            // Pas d'ajout de vitesseVerticale ici quand on est en déplacement libre
        } else {
            // Applique la gravité normale quand on ne peut pas se déplacer spatialement
            finalMouvement = vitesseVerticale * Time.deltaTime;
        }

        characterController.Move(finalMouvement);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("ZoneDeDeplacementExterieur"))
        {
            peutSeDeplacer = true;
            Debug.Log("Tu es sorti du vaisseau, déplacement spatial activé !");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ZoneDeDeplacementExterieur"))
        {
            peutSeDeplacer = false;
            Debug.Log("Tu es rentré dans le vaisseau, déplacement spatial désactivé.");
            vitesseVerticale = Vector3.zero;
        }
    }
}