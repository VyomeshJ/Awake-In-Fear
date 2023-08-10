using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject AskPrompt;
    public EventSystem eventSystem;
    public GameObject GameLoadObj;
    public GameObject ManageUI;
    public bool PromptOpen;
    public void pauseGame(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SaveVariables.GamePaused = true;
        Time.timeScale = 0;
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.transform.Find("PauseUI").gameObject.SetActive(true);
        //eventSystem.SetSelectedGameObject(gameObject.transform.Find("PauseUI").Find(ResumeButton))

    }
    public void ExitGame(){
        AskPrompt.SetActive(true);
        PromptOpen = true;
    }
    public void Prompt_Yes(){
        Time.timeScale = 1;
        GameLoadObj.GetComponent<GameLoad>().SaveGame();
        ManageUI.GetComponent<ManageUI>().SceneChange("MainMenu");
    }
    public void Prompt_No(){
        Time.timeScale = 1;
        ManageUI.GetComponent<ManageUI>().SceneChange("MainMenu");
    }
    public void resumeGame(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SaveVariables.GamePaused = false;
        Time.timeScale = 1;
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.transform.Find("PauseUI").gameObject.SetActive(false);
    }
}
