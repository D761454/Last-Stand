using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject m_controls_Settings_Panel;
    public GameObject m_Loading_Screen;
    public GameObject m_respawn_Panel;
    public GameObject m_player_UI_Panel;
    bool isCSPanelOpen = false;
    bool loading = false;

    public void LoadLevel1()
    {
        StartCoroutine(LoadAsyncScene());
    }

    public void OpenAndCloseSettingsPanel()
    {
        isCSPanelOpen = !isCSPanelOpen;
        m_controls_Settings_Panel.SetActive(isCSPanelOpen);
    }

    public void OpenDeathScreen()
    {
        m_respawn_Panel.SetActive(true);
        m_player_UI_Panel.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad1 = SceneManager.LoadSceneAsync(1);

        loading = true;
        m_Loading_Screen.SetActive(loading);

        while (!asyncLoad1.isDone)
        {
            yield return null;
        }
    }
}
