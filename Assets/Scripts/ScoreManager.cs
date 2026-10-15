using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component that displays the score
    [SerializeField] TextMeshProUGUI gemText;

    int score = 0; // Variable to keep track of the player's score


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

    public void AddScore(int scoreValue)
    {
        score += scoreValue; // Increment the score by the specified amount
        scoreText.text = score.ToString("00000"); // Update the score text with the new score
    }

    public void AddGem(int gem){
        gemText.text = gem.ToString("Gems 0/3");
    }

}
