using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger2 : MonoBehaviour
{
    void Start()
    {
        // Appelle la fonction "ChangeScene2" après 20 secondes
        Invoke("ChangeScene", 20f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("main");
    }
}
