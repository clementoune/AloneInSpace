using UnityEngine;
using UnityEngine.InputSystem;

public class GestionDeplacementSpatialVR : MonoBehaviour
{
    public float vitesseDeplacement = 1f;
    public float gravite = 9.81f;

    private CharacterController characterController;
    private Vector3 vitesse = Vector3.zero;

    private PlayerInput playerInput;
    private InputAction moveAction;

    private Vector2 mouvementInput;
    private bool estDansZoneGravite = true;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        moveAction.performed += ctx => mouvementInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => mouvementInput = Vector2.zero;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogError("CharacterController non trouvé !");
            enabled = false;
        }
    }

    void Update()
    {
        // Mouvement basé sur le regard
        Vector3 inputDirection = new Vector3(mouvementInput.x, 0f, mouvementInput.y);
        Vector3 deplacement = Camera.main.transform.TransformDirection(inputDirection);
        deplacement.y = 0f;
        deplacement.Normalize();

        // Gravité
        if (estDansZoneGravite)
        {
            if (characterController.isGrounded)
            {
                vitesse.y = -0.1f;
            }
            else
            {
                vitesse.y -= gravite * Time.deltaTime;
            }
        }
        else
        {
            vitesse.y = 0f; // Pas de gravité dans l’espace
        }

        Vector3 mouvementTotal = (deplacement * vitesseDeplacement + Vector3.up * vitesse.y) * Time.deltaTime;
        characterController.Move(mouvementTotal);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZoneDeDeplacementInterieur"))
        {
            estDansZoneGravite = true;
            Debug.Log("Entrée dans la zone de gravité.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ZoneDeDeplacementInterieur"))
        {
            estDansZoneGravite = false;
            vitesse.y = 0f;
            Debug.Log("Sortie de la zone de gravité.");
        }
    }
}
