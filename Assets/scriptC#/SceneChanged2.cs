using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChanger2 : MonoBehaviour
{
    public Image fadeImage; // Image noire pour l'effet de fondu
    public float fadeDuration = 2f; // Durée du fondu

    void Start()
    {
        // Démarre avec un fondu d'ouverture
        StartCoroutine(FadeIn());

        // Lance le changement de scène après 20 secondes
        Invoke("StartSceneTransition", 20f);
    }

    void StartSceneTransition()
    {
        StartCoroutine(FadeOutAndChangeScene("main"));
    }

    IEnumerator FadeIn()
    {
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOutAndChangeScene(string sceneName)
    {
        float alpha = 0f;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // Change de scène après le fondu
        SceneManager.LoadScene(sceneName);
    }
}
