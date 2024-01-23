using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLang();
    [DllImport("__Internal")]
    public static extern bool ShowAd();
    [DllImport("__Internal")]
    public static extern void RateGame();

    [SerializeField] private GameObject menuScene;
    [SerializeField] private GameObject gameScene;
    [SerializeField] private GameObject gameOverScene;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text savesText;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Button rateBtn;
    public string curLang;

    private bool _isWatched = false;

    public static SettingsHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //try { curLang = GetLang(); } catch { }
        curLang = GetLang();
    }

    public void ChangeAudioVolume()
    {
        AudioListener.volume = audioSlider.value;
    }

    public void StartGame()
    {
        ObjectHandler.score = 0;
        ObjectHandler.saves = 0;
        gameScene.SetActive(true);
        menuScene.SetActive(false);
        gameOverScene.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScene.SetActive(true);
        gameScene.SetActive(false);
        highScoreText.text = $"{highScoreText.text.Split(" ")[0]} {highScoreText.text.Split(" ")[1]} {ObjectHandler.highScore}";
        scoreText.text = $"{scoreText.text.Split(" ")[0]} {scoreText.text.Split(" ")[1]} {ObjectHandler.score}";
        savesText.text = $"{savesText.text.Split(" ")[0]} {savesText.text.Split(" ")[1]} {ObjectHandler.saves}";
    }

    public void RestartGame()
    {
        if (_isWatched) StartGame();
        else
        {
            AudioListener.pause = true;
            //try { ShowAd(); } catch { }
            ShowAd();
            StartCoroutine(Watch());
        }
    }

    public IEnumerator Watch()
    {
        _isWatched = true;
        yield return new WaitForSeconds(60f);
        _isWatched = false;
    }

    public void Rate()
    {
        //try { RateGame(); } catch { }
        RateGame();
        rateBtn.interactable = false;
    }

    public void UnPause()
    {
        AudioListener.pause = false;
    }
}
