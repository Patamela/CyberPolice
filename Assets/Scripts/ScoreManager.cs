using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI component that displays the score

    public void AddScore(int score)
    {
        scoreText.text = score.ToString("00000"); // Update the score text with the new score
    }
}
