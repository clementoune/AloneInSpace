using System.Collections;
using UnityEngine;
using TMPro;

public class HideTitle : MonoBehaviour
{
    public TextMeshProUGUI titleText; // Référence au texte UI

    void Start()
    {
        if (titleText != null)
        {
            StartCoroutine(HideAfterSeconds(3f)); // Lance la coroutine
        }
        else
        {
            Debug.LogError("⚠️ Aucun TextMeshProUGUI assigné !");
        }
    }

    IEnumerator HideAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        titleText.gameObject.SetActive(false);
    }
}
