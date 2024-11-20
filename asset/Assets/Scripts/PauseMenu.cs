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
        Debug.Log("HERLLLLLL2");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SaveVariables.GamePaused = true;
        Time.timeScale = 0;
        gameObject.GetComponent<Image>().enabled = true;
        gameObject.transform.Find("PauseUI").gameObject.SetActive(true);
        

    }
    private void Update()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
    public void ExitGame(){
        AskPrompt.SetActive(true);
        gameObject.transform.Find("PauseUI").gameObject.SetActive(false);
        PromptOpen = true;
        
    }
    public void Prompt_Yes(){
        PlayerPrefs.SetInt(PlayerPrefs.GetInt("SaveNum").ToString() + "_FlashBatteries", SaveVariables.NumBatteries);
        PlayerPrefs.SetFloat(PlayerPrefs.GetInt("SaveNum").ToString() + "_FlashTime", SaveVariables.flashtime);
        Time.timeScale = 1;
        ManageUI.GetComponent<ManageUI>().SceneChange("MainMenu");
    }
    public void Prompt_No(){
        Time.timeScale = 1;
        AskPrompt.SetActive(false);
        PromptOpen = false;
    }
    public void resumeGame(){
        Debug.Log("HERLLLLLL");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SaveVariables.GamePaused = false;
        Time.timeScale = 1;
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.transform.Find("PauseUI").gameObject.SetActive(false);
       
    }
}
