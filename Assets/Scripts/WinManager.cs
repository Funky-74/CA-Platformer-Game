using UnityEngine;
using UnityEngine.SceneManagement;

public class Winmanager : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");

    }

    public void Quit()
    {
        Application.Quit();
    }
}
