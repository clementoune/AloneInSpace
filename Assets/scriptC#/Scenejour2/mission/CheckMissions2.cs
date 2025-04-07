using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckMissions2 : MonoBehaviour
{
    public Toggle monCheckMark1;
    public Toggle monCheckMark2;
    public Toggle monCheckMark3;
    public EquiperCasqueVR2 scriptCasqueVR;
    public MiseEnCommun scriptMiseEnCommun;
    public GreenButton2 scriptButtonGrenne;
    public AudioSource terminer;
    public VRCanvasController2 cycleJourNuit;

    public static bool finishedday = false;
    public static bool dejaValider = false;

    public TextMeshProUGUI texteCheckMark1;
    public TextMeshProUGUI texteCheckMark2;
    public TextMeshProUGUI texteCheckMark3;

    // Tableaux pour les missions, chaque élément doit être un composant TextMeshProUGUI que vous connectez dans l'éditeur Unity
    public TextMeshProUGUI[] missionsJour = new TextMeshProUGUI[3];  // Taille du tableau défini à 3


    void Start()
    {
        missionsJour[0].text = "Eteindre l alarme";
        missionsJour[1].text = "Tirer sur des asteroides pour les detruire";
        missionsJour[2].text = "Retrouver les fusibles et les replacer";
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
        if (!dejaValider) ValiderMissions();
    }
    //a modif
    // Méthode pour valider les missions lorsque le bouton est cliqué
    public void ValiderMissions()
    {
        if (scriptCasqueVR != null)
        {
            if (scriptButtonGrenne.estActiver)
            {
                Debug.Log("L'alarme est eteinte'!");
                if (monCheckMark1 != null)
                {
                    monCheckMark1.isOn = true;
                }
            }
            if (scriptCasqueVR.AEteEquipe)
            {
                Debug.Log("Le casque a été équipé !// en refonte");
                if (monCheckMark2 != null)
                {
                    monCheckMark2.isOn = true;
                }
            }

            if (scriptMiseEnCommun.Check)
            {
                Debug.Log("Les fusibles ont été positionnés !");
                if (monCheckMark3 != null)
                {
                    monCheckMark3.isOn = true;
                }
            }
            Debug.Log("état des missions : " + scriptButtonGrenne.estActiver + ", " + scriptCasqueVR.AEteEquipe + ", " + scriptMiseEnCommun.Check);
            if (scriptButtonGrenne.estActiver && scriptMiseEnCommun.Check)
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