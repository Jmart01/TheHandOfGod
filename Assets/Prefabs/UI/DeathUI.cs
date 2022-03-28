using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    public void OnRestartBtnClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitBtnClicked()
    {
        Application.Quit();
    }
}
