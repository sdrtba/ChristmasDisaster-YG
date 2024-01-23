using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LanguageHandler : MonoBehaviour
{
    [SerializeField] private string _ru;
    [SerializeField] private string _en;

    void Start()
    {
        StartCoroutine(Translate());
    }

    private IEnumerator Translate()
    {
        yield return new WaitForSeconds(0.0001f);
        if (SettingsHandler.Instance.curLang == "ru")
        {
            GetComponent<Text>().text = _ru;
        }
        else
        {
            GetComponent<Text>().text = _en;
        }
    }
}
