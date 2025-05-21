using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [System.Serializable]
    public class SceneButton
    {
        public Button button;
        public string sceneName; 
    }

    public SceneButton[] sceneButtons;

    void Start()
    {
        foreach (var sb in sceneButtons)
        {
            string sceneToLoad = sb.sceneName;

            if (sceneToLoad.ToUpper() == "EXIT")
            {
                sb.button.onClick.AddListener(QuitGame);
            }
            else
            {
                sb.button.onClick.AddListener(() => LoadScene(sceneToLoad));
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene name is empty.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit(); 
#endif
    }
}
