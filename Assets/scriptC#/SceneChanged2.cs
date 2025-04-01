using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger2 : MonoBehaviour
{
    void Start()
    {
        // Appelle la fonction "ChangeScene2" apr�s 15 secondes
        Invoke("ChangeScene", 15f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("main");
    }
}
