using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void ResumeGame(GameObject pauseMenu)
    {
        Time.timeScale = 1.0f;
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    public void SoundEffect()
    {
        AudioManager.Instance.PlayEffect(AudioManager.Effects.Button);
    }
}
