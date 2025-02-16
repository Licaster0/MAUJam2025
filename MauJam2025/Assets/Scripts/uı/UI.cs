using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseMenuUI;

    void Start()
    {
        SwitchTo(inGameUI);
    }

    public void ResumeGame()
    {
        SwitchTo(inGameUI);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            SwitchWithKeyTo(pauseMenuUI);

    }

    public void SwitchTo(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            // bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null;


            // if (!fadeScreen)
            //     transform.GetChild(i).gameObject.SetActive(false);
        }


        if (_menu != null)
        {
            _menu.SetActive(true);
        }

        if (_menu == inGameUI)
        {
            PauseGame(false);
        }
        else
        {
            PauseGame(true);
        }
    }


    public void SwitchWithKeyTo(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
            //     return;
        }

        SwitchTo(inGameUI);
    }
    public void PauseGame(bool _pause)
    {
        if (_pause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
