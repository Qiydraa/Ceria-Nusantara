using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [System.Serializable]
    public class SceneButton
    {
        public Button button;
        public string sceneName; // isi "EXIT" jika ingin keluar dari aplikasi
    }

    public SceneButton[] sceneButtons;

    void Start()
    {
        foreach (var sb in sceneButtons)
        {
            string sceneToLoad = sb.sceneName;

            // Tambahkan listener ke button
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
        UnityEditor.EditorApplication.isPlaying = false; // Stop game in editor
#else
        Application.Quit(); // Quit game in build
#endif
    }
}
