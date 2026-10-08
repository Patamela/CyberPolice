using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the scene named "Levels" when the PlayGame method is called
        SceneManager.LoadScene("Levels");
    }

    public void QuitGame()
    {
        // Quit the application when the QuitGame method is called
        Application.Quit();
    }

    public void CreditsGame()
    {
        // Load the scene named "Credits" when the CreditsGame method is called
        SceneManager.LoadScene("Credits");    
    }

    public void Character()
    {
        // Load the scene named "SelectorCharacter" when the Character method is called
        SceneManager.LoadScene("SelectorCharacter");
    }

    public void SelectCharacter(int characterIndex)
    {
        
        PlayerPrefs.SetInt("SelectedCharacter", characterIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Menu");
    }

    public void SelectLevel(int level)
    {
        // Load the scene named "Level1" when the SelectLevel1 method is called
        SceneManager.LoadScene($"Level{level}");
    }

    public void BackToMenu()
    {
        // Load the scene named "Menu" when the BackToMenu method is called
        SceneManager.LoadScene("Menu");
    }

}
