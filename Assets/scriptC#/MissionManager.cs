using UnityEngine;
using UnityEngine.UI;
using System;

public class MissionManager : MonoBehaviour
{
    // Références aux Toggles et aux missions
    public Toggle missionToggle1;
    public Toggle missionToggle2;
    public Toggle missionToggle3;
    public Text missionText1;
    public Text missionText2;
    public Text missionText3;

    private string[] missions = {
        "Trouver l'artefact",
        "Activer les générateurs",
        "Fuir la base"
    };

    private bool[] missionsCompleted = { false, false, false };

    void Start()
    {
        // Initialisation des missions
        UpdateMissionUI();
    }

    void UpdateMissionUI()
    {
        // Met à jour l'UI selon l'état des missions
        missionText1.text = missions[0];
        missionText2.text = missions[1];
        missionText3.text = missions[2];

        // Assurer que les cases sont cochées si la mission est terminée
        missionToggle1.isOn = missionsCompleted[0];
        missionToggle2.isOn = missionsCompleted[1];
        missionToggle3.isOn = missionsCompleted[2];
    }

    // Appelée quand une mission est terminée (peut être déclenchée par un événement dans ton jeu)
    public void CompleteMission(int missionIndex)
    {
        if (missionIndex >= 0 && missionIndex < missions.Length)
        {
            missionsCompleted[missionIndex] = true;
            UpdateMissionUI(); // Met à jour l'interface
        }
    }

    // Fonction pour réinitialiser les missions (par exemple, quand le joueur redémarre le jeu)
    public void ResetMissions()
    {
        for (int i = 0; i < missionsCompleted.Length; i++)
        {
            missionsCompleted[i] = false;
        }
        UpdateMissionUI(); // Réinitialise l'interface
    }
}
