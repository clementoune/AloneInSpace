using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CableConnectionVR : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Material cableMaterial;
    public float snapDistance = 0.2f;

    private bool isConnected = false;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>().selectExited.AddListener(OnCableReleased);
    }

    void OnCableReleased(SelectExitEventArgs args)
    {
        ConnectCable();
    }

    void ConnectCable()
    {
        if (Vector3.Distance(transform.position, endPoint.position) < snapDistance &&
            lineRenderer.material == cableMaterial &&
            endPoint.GetComponent<MeshRenderer>().material == cableMaterial)
        {
            isConnected = true;
            Debug.Log("Câble connecté !");
        }
        else
        {
            Debug.Log("Mauvaise connexion !");
        }
    }

    void Update()
    {
        if (!isConnected)
        {
            lineRenderer.SetPosition(1, transform.position); // Met à jour l'extrémité du câble
        }
    }
}