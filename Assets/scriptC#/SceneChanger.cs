using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("main"); // Charge la scène "main"
    }
}
