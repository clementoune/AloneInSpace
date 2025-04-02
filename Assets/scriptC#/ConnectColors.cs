using UnityEngine;


public class ConnectColorsVR : MonoBehaviour
{
    public Transform jaune1;
    public Transform jaune2;
    public Transform vert1;
    public Transform vert2;
    public Transform rouge1;
    public Transform rouge2;
    public Transform bleu1;
    public Transform bleu2;
    public Transform violet1;
    public Transform violet2;

    void Start()
    {
        Connect(jaune1, jaune2);
        Connect(vert1, vert2);
        Connect(rouge1, rouge2);
        Connect(bleu1, bleu2);
        Connect(violet1, violet2);
    }

    void Connect(Transform startPoint, Transform endPoint)
    {
        // Crée un objet pour le câble
        GameObject cable = new GameObject("Cable_" + startPoint.name + "_" + endPoint.name);
        cable.transform.parent = transform;

        // Ajoute le composant LineRenderer
        LineRenderer lineRenderer = cable.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // Définit les points de départ et d'arrivée
        lineRenderer.SetPosition(0, startPoint.position);
        lineRenderer.SetPosition(1, endPoint.position);

        // Applique un matériau de la même couleur que les points de connexion
        Material cableMaterial = startPoint.GetComponent<MeshRenderer>().material;
        lineRenderer.material = cableMaterial;

        // Ajoute le composant XRGrabInteractable pour permettre la saisie du câble
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = cable.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Ajoute le script CableConnectionVR pour gérer la connexion
        CableConnectionVR connectionScript = cable.AddComponent<CableConnectionVR>();
        connectionScript.startPoint = startPoint;
        connectionScript.endPoint = endPoint;
        connectionScript.cableMaterial = cableMaterial;
    }
}