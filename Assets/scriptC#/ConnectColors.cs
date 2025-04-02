using UnityEngine;
using System.Collections.Generic;

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

    // Nouvelle variable pour stocker les informations de connexion
    private Dictionary<Color, (Transform start, Transform end)> connections = new Dictionary<Color, (Transform start, Transform end)>();

    void Start()
    {
        // Stocke les informations de connexion
        connections[jaune1.GetComponent<MeshRenderer>().material.color] = (jaune1, jaune2);
        connections[vert1.GetComponent<MeshRenderer>().material.color] = (vert1, vert2);
        connections[rouge1.GetComponent<MeshRenderer>().material.color] = (rouge1, rouge2);
        connections[bleu1.GetComponent<MeshRenderer>().material.color] = (bleu1, bleu2);
        connections[violet1.GetComponent<MeshRenderer>().material.color] = (violet1, violet2);
    }

    // Fonction pour créer un câble à la demande
    public void CreateCable(Transform startPoint)
    {
        // Récupère les informations de connexion
        (Transform start, Transform end) = connections[startPoint.GetComponent<MeshRenderer>().material.color];

        // Crée un objet pour le câble
        GameObject cable = new GameObject("Cable_" + start.name + "_" + end.name);
        cable.transform.parent = transform;

        // Ajoute le composant LineRenderer
        LineRenderer lineRenderer = cable.AddComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        // Définit les points de départ et d'arrivée
        lineRenderer.SetPosition(0, start.position);
        lineRenderer.SetPosition(1, start.position); // Le câble commence au point de départ

        // Applique un matériau de la même couleur que les points de connexion
        Material cableMaterial = start.GetComponent<MeshRenderer>().material;
        lineRenderer.material = cableMaterial;

        // Ajoute le composant XRGrabInteractable pour permettre la saisie du câble
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable = cable.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // Ajoute le script CableConnectionVR pour gérer la connexion
        CableConnectionVR connectionScript = cable.AddComponent<CableConnectionVR>();
        connectionScript.startPoint = start;
        connectionScript.endPoint = end;
        connectionScript.cableMaterial = cableMaterial;
    }
}