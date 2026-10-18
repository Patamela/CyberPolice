using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component that displays the score
    [SerializeField] TextMeshProUGUI gemText;

    int levelScore = 0; // Variable to keep track of the player's score
    int totalScore = 0;

    private const string COIN_KEY = "COIN_KEY";

    public void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        totalScore = PlayerPrefs.GetInt(COIN_KEY, 0);
        levelScore = 0;
    }

    public void OnEnable()
    {
        Coin.OnCoinCollected += AddScore; // Subscribe to the OnCoinCollected event when the script is enabled
        Gems.OnGemCollected  += AddGem;
    }

    public void OnDisable()
    {
        Coin.OnCoinCollected -= AddScore; // Unsubscribe from the OnCoinCollected event when the script is disabled
        Gems.OnGemCollected -= AddGem;
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= 0 || totalScore < amount) return false;

        totalScore -= amount;
        SaveScore();
        return true;
    }

    public void AddScore(int scoreValue)
    {
        levelScore += scoreValue;
        totalScore += scoreValue; // Increment the score by the specified amount
        SaveScore();
        scoreText.text = levelScore.ToString("00000"); // Update the score text with the new score
    }

    public void AddGem(int gem){
        gemText.text = gem.ToString("Gems 0/3");
    }

    private void SaveScore() 
    {
        PlayerPrefs.SetInt("COIN_KEY", totalScore);
        PlayerPrefs.Save();
    }

}
