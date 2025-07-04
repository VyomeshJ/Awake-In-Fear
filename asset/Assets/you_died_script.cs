using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class you_died_script : MonoBehaviour
{
    public GameObject GoToMainMenuButton;
    public GameObject GoToGameButton;
    public GameObject transition_obj;
    public GameObject credits_obj;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //File.Delete(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() + ".txt");
    }
    public void end_ani()
    {
        GoToMainMenuButton.SetActive(true);
        if(GoToGameButton != null)
        {
            GoToGameButton.SetActive(true);
        }
        if(credits_obj != null)
        {
            credits_obj.SetActive(true);
        }
        
    }

    public void go_to_main_menu()
    {
        transition_obj.SetActive(true);
        transition_obj.GetComponent<Animator>().Play("close");
        StartCoroutine(wait_to_main_menu());
    }
    public void restart_game()
    {
        transition_obj.SetActive(true);
        transition_obj.GetComponent<Animator>().Play("close");
        StartCoroutine(wait_to_game());
    }
    IEnumerator wait_to_game()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Scene_A");
    }
    IEnumerator wait_to_main_menu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
