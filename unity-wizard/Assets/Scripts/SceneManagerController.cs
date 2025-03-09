using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void LoadSeneByString(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
