using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckMissions2 : MonoBehaviour
{
    public Toggle monCheckMark1;
    public Toggle monCheckMark2;
    public Toggle monCheckMark3;
    public EquiperCasqueVR2 scriptCasqueVR; 
    public LevierScript2 scriptLevier;
    public AudioSource terminer;
    public VRCanvasController2 cycleJourNuit;

    public static bool finishedday=false;
    public static bool dejaValider = false;

    public TextMeshProUGUI texteCheckMark1;
    public TextMeshProUGUI texteCheckMark2;
    public TextMeshProUGUI texteCheckMark3;

    // Tableaux pour les missions, chaque élément doit être un composant TextMeshProUGUI que vous connectez dans l'éditeur Unity
    public TextMeshProUGUI[] missionsJour = new TextMeshProUGUI[3];  // Taille du tableau défini à 3


    void Start()
    {
            missionsJour[0].text = "Equiper le casque";
            missionsJour[1].text = "Reposer le casque";
            missionsJour[2].text = "Demarrer le vaisseau";
            texteCheckMark1.text = missionsJour[0].text;
            texteCheckMark2.text = missionsJour[1].text;
            texteCheckMark3.text = missionsJour[2].text;
            Debug.Log("Jour 1 : " + missionsJour[0].text + ", " + missionsJour[1].text + ", " + missionsJour[2].text);

        // Initialisation des checkmarks à l'état désactivé
        if (monCheckMark1 != null)
        {
            monCheckMark1.isOn = false;
        }
        if (monCheckMark2 != null)
        {
            monCheckMark2.isOn = false;
        }
        if (monCheckMark3 != null)
        {
            monCheckMark3.isOn = false;
        }
    }

    void Update()
    {
        if(!dejaValider)ValiderMissions();
    }
    //a modif
    // Méthode pour valider les missions lorsque le bouton est cliqué
    public void ValiderMissions()
    {
        if (scriptCasqueVR != null)
        {
            if (scriptCasqueVR.AEteEquipe)
            {
                Debug.Log("Le casque a été équipé !");
                if (monCheckMark1 != null)
                {
                    monCheckMark1.isOn = true;
                }
            }

            if (scriptCasqueVR.AEteRepose)
            {
                Debug.Log("Le casque a été reposé !");
                if (monCheckMark2 != null)
                {
                    monCheckMark2.isOn = true;
                }
            }

            if (scriptLevier.estActiver)
            {
                Debug.Log("Le vaisseau a démarré !");
                if (monCheckMark3 != null)
                {
                    monCheckMark3.isOn = true;
                }
            }
            Debug.Log("état des missions : " + scriptLevier.estActiver + ", " + scriptCasqueVR.AEteEquipe + ", " + scriptCasqueVR.AEteRepose);
            if (scriptLevier.estActiver && scriptCasqueVR.AEteEquipe && scriptCasqueVR.AEteRepose)
            {
                if (terminer != null)
                {
                    terminer.Play();
                    dejaValider = true;
                    finishedday = true;
                    
                }
            }
        }
    }
}
