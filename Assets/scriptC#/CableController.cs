using UnityEngine;

public class CableRenderer : MonoBehaviour
{
    public Transform startPoint;  // Référence au connecteur de départ
    public Transform endPoint;    // Référence au connecteur d'arrivée
    private LineRenderer lineRenderer;

    void Start()
    {
        // Ajouter un LineRenderer au GameObject
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Un câble a deux points (départ & arrivée)

        // Paramètres du câble
        lineRenderer.startWidth = 0.02f; // Épaisseur du câble
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Shader de base
        lineRenderer.startColor = Color.red; // Couleur du câble
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        // Met à jour la position du câble en temps réel
        if (startPoint != null && endPoint != null)
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }
}
