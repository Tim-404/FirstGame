using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Navigation: Handles all events for using buttons, mainly scene changes
/// </summary>
public class Navigation : MonoBehaviour
{
    // Scene names
    private string homeScene = "Home";
    private string listScene = "LevelsList";

    public void ToScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// Loads the next build scene
    /// </summary>
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Reloads the currect scene
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Loads the home screen
    /// </summary>
    public void ToHome()
    {
        SceneManager.LoadScene(homeScene);
    }

    /// <summary>
    /// Loads the levels list screen
    /// </summary>
    public void ToLevelsList()
    {
        SceneManager.LoadScene(listScene);
    }

    /// <summary>
    /// Closes the application
    /// </summary>
    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
