using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool isGameStarted;
    public static int coinCounter = 0;
    public static int totalCoins = 0;
    public GameObject gameoverPanel;
    public GameObject tapTostart;
    public Text coinsText;
    public Text totalCoinsText;
    public GameObject coinText;
    public GameObject totalCoinText;
    public Text earnedCoinsText;

    private bool coroutineStarted = false;
    public static string path;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;

        coinCounter = 0;
        totalCoins = PlayerPrefs.GetInt("totalcoins");
    }

    void Update()
    {
        if (gameOver)
        {
            if (!coroutineStarted)
            {
                SaveTotalCoinsToFile();
                Time.timeScale = 0; // Oyundaki her şeyin durdurulması, gameover panel çıktığında arka taraf donacak ve oyun bit
                gameoverPanel.SetActive(true);
                hideScores();
                StartCoroutine(EarnedCoinsTextAnim());
                coroutineStarted = true;
            }
        }
        else
        {
            displayScores();

            if (SwipeManager.tap)
            {
                isGameStarted = true;
                TapToStartControl();
                Debug.Log("denemek");
            }
        }
    }

    public static void AddCoins(int amount)
    {
        coinCounter += amount;
        PlayerPrefs.SetInt("totalCoins", amount + PlayerPrefs.GetInt("totalCoins"));
    }
    public static void SaveTotalCoinsToFile()
    {
        GameData data = new GameData
        {
            nickname = MainMenu.nickname,
            totalCoins = PlayerPrefs.GetInt("totalCoins"),
            earnedCoins = coinCounter,
        };
        string json = JsonUtility.ToJson(data);
        json = json.Replace("{", "{\n\t").Replace("}", "\n}").Replace(",", ",\n\t").Replace(":", ": "); 
        path = "E:/Run Tire Run/gameData.json";
        File.AppendAllText(path, json);
    }
    void TapToStartControl()
    {
        tapTostart.SetActive(false);
    }

    void displayScores()
    {
        coinsText.text = "Coins: " + coinCounter;
        totalCoinsText.text = "Total Coins: " + PlayerPrefs.GetInt("totalCoins");
    }

    void hideScores()
    {
        coinText.SetActive(false);
        totalCoinText.SetActive(false);
    }

    IEnumerator EarnedCoinsTextAnim()
    {
        while (true)
        {
            earnedCoinsText.text = "Earned Coins: " + coinCounter;
            yield return new WaitForSecondsRealtime(0.36f); // Gerçek zamanlı bekleme
            earnedCoinsText.text = "";
            yield return new WaitForSecondsRealtime(0.36f); // Gerçek zamanlı bekleme
        }
    }
}