using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the highest unlocked level from PlayerPrefs, default to 1 if not set
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Get the Layout Group gameObject with all the levels buttons
        Transform levels = transform.GetChild(5);

        for(int i=0; i<levels.childCount; i++)
        {
            Button levelButton = levels.GetChild(i).GetComponent<Button>();
            Debug.Log("FinalLevelUnlocked:" + GameProgressManager.Instance.FinalLevelUnlocked());
            if (i == 2 && GameProgressManager.Instance.FinalLevelUnlocked())
            {
                levelButton.interactable = true;
            }
            else
            {
                levelButton.interactable = (i < unlockedLevel);
            }
                
        }
    }
}
