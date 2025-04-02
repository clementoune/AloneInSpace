using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CableConnectionVR : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Material cableMaterial;

    private bool isConnected = false;

    void Start()
    {
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().selectExited.AddListener(OnCableReleased);
    }

    void OnCableReleased(SelectExitEventArgs args)
    {
        // Le câble est relâché par le joueur
        CheckConnection();
    }

    void CheckConnection()
    {
        // Vérifie si le câble est connecté aux bons points de connexion
        if (Vector3.Distance(transform.position, endPoint.position) < 0.1f &&
            GetComponent<LineRenderer>().material == cableMaterial)
        {
            isConnected = true;
            // Effets visuels et sonores de connexion réussie
        }
    }

    void Update()
    {
        if (!isConnected)
        {
            // Met à jour la position du câble
            GetComponent<LineRenderer>().SetPosition(0, startPoint.position);
            GetComponent<LineRenderer>().SetPosition(1, transform.position);
        }
    }
}