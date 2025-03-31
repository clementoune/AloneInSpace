using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")) // Vérifie si la main touche le bouton
        {
            LoadMainScene();
        }
    }

    public void LoadMainScene()
    {
        Debug.Log("Chargement de la scène main...");
        SceneManager.LoadScene("main"); // Charge la scène "decolage"
    }
}
