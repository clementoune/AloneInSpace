using UnityEngine;

public class CableRenderer : MonoBehaviour
{
    public Transform startPoint;  // R�f�rence au connecteur de d�part
    public Transform endPoint;    // R�f�rence au connecteur d'arriv�e
    private LineRenderer lineRenderer;

    void Start()
    {
        // Ajouter un LineRenderer au GameObject
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Un c�ble a deux points (d�part & arriv�e)

        // Param�tres du c�ble
        lineRenderer.startWidth = 0.02f; // �paisseur du c�ble
        lineRenderer.endWidth = 0.02f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Shader de base
        lineRenderer.startColor = Color.red; // Couleur du c�ble
        lineRenderer.endColor = Color.red;
    }

    void Update()
    {
        // Met � jour la position du c�ble en temps r�el
        if (startPoint != null && endPoint != null)
        {
            lineRenderer.SetPosition(0, startPoint.position);
            lineRenderer.SetPosition(1, endPoint.position);
        }
    }
}
