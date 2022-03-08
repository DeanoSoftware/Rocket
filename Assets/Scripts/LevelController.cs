using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : Singleton<LevelController>
{
    private static int menuBuildIndex = 0;
    private static int trophiesBuildIndex = 1;
    private static int level1BuildIndex = 2;

    public void PlayGame()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(level1BuildIndex);
    }

    public void GoToMenu()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(menuBuildIndex);
    }

    public void GoToTrophies()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(trophiesBuildIndex);
    }
}
