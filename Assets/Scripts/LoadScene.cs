using UnityEngine;
using UnityEngine.SceneManagement;

// Used by various buttons to load different scenes
public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
