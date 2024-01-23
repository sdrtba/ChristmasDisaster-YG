using System.Collections;
using System.Runtime.InteropServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string LoadExtern();
    [DllImport("__Internal")]
    private static extern void SaveExtern(int date);

    [SerializeField] private GameObject effect;
    [SerializeField] private Text scoreText;
    public static int highScore;
    public static int score;
    public static int saves;

    private void Start()
    {
        //try { LoadExtern(); } catch { }
        LoadExtern();
    }

    private void OnEnable()
    {
        scoreText.text = scoreText.text.Split(" ")[0] + " 0";
    }

    private void OnMouseDown()
    {
        score += 1;
        scoreText.text = scoreText.text.Split(" ")[0] + $" {score}";
        Instantiate(effect, transform.position, quaternion.identity);
        StartCoroutine(Squash());
    }

    private IEnumerator Squash()
    {
        gameObject.transform.localScale = new Vector3(1.9f, 1.9f, 1.9f);
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.localScale = new Vector3(2f, 2f, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (score > highScore)
        {
            highScore = score;
            //try { SaveExtern(highScore); } catch { }
            SaveExtern(highScore);
        }
        SettingsHandler.Instance.GameOver();
    }

    public void Load(int _value)
    {
        highScore = _value;
    }
}
