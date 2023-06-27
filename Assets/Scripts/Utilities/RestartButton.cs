using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
