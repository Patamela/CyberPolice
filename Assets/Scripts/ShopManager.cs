using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private int scorePlayer = 50000;
    [SerializeField] private Button btnUnlocked;
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private TextMeshProUGUI textTotalPoints;

    public void Update()
    {
        textTotalPoints.SetText(PlayerPrefs.GetInt("COIN_KEY").ToString("00000"));
    }

    public void BuyCharacter()
    {
        if(ScoreManager.Instance == null)
        {
            Debug.LogError("ScoreManager cannot be found.");
            return;
        }

        bool purchaseCompleted = ScoreManager.Instance.SpendCurrency(scorePlayer);

        if (purchaseCompleted)
        {
            Debug.Log("¡Purchased Character!");
            btnUnlocked.interactable = true;
            textAmount.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Insufficient coins.");
        }
    }
}
