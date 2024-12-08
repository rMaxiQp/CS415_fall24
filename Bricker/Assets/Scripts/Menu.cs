using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        GameManager.Instance.NewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.MainMenu();
    }
}
