using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExitConfirmationUI : MonoBehaviour
{
    public GameObject panelKonfirmasi;
    public Button buttonKeluar;
    public Button buttonYa;
    public Button buttonTidak;

    void Start()
    {
        panelKonfirmasi.SetActive(false);

        buttonKeluar.onClick.AddListener(ShowExitPanel);
        buttonYa.onClick.AddListener(KeluarAplikasi);
        buttonTidak.onClick.AddListener(HideExitPanel);
    }

    void ShowExitPanel()
    {
        panelKonfirmasi.SetActive(true);
    }

    void HideExitPanel()
    {
        panelKonfirmasi.SetActive(false);
    }

    void KeluarAplikasi()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
