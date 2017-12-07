using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonAction : MonoBehaviour
{
    public int index;

    public void handleExit()
    {
        SceneManager.LoadScene(index);
    }
}
