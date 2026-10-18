using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void TryAgain()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void Quit()
    {
        Application.Quit();
    }
}
