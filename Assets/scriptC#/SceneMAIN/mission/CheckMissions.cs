using UnityEngine;
using UnityEngine.UI;

public class CheckMissions : MonoBehaviour{
    public Toggle monCheckMark1;
    public Toggle monCheckMark2;
    public Toggle monCheckMark3;
    public EquiperCasqueVR scriptCasqueVR; 
    public LevierScript scriptLevier;
    public AudioSource terminer;

    void Start()
    {
        if (monCheckMark1 != null)
        {
            monCheckMark1.isOn=false;
        }
        if (monCheckMark2 != null)
        {
            monCheckMark2.isOn=false;
        }
        if (monCheckMark3 != null)
        {
            monCheckMark3.isOn=false;
        }
    }

    void Update()
    {
        if (scriptCasqueVR != null)
        {
            if (scriptCasqueVR.AEteEquipe)
            {
                Debug.Log("Le casque a été équipé !");
                if (monCheckMark1 != null)
                {
                    monCheckMark1.isOn=true;
                }
            }

            if (scriptCasqueVR.AEteRepose)
            {
                Debug.Log("Le casque a été reposé !");
                if (monCheckMark2 != null)
                {
                    monCheckMark2.isOn=true;
                }
            }
            if(scriptLevier.estActiver)
            {
                Debug.Log("Le vaisseau a démarré !");
                if (monCheckMark3 != null)
                {
                    monCheckMark3.isOn=true;
                }
            }else if(scriptLevier.estActiver&&scriptCasqueVR.AEteEquipe&&scriptCasqueVR.AEteRepose)
            {
                Debug.Log("Mission terminée !");
                if (terminer != null)
                {
                    terminer.Play();
                }
                else
                {
                    Debug.LogWarning("🔇 Aucun son assigné pour la mission terminée !");
                }
            } 
        }
    }
}