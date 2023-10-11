using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class you_died_script : MonoBehaviour
{
    public GameObject GoToMainMenuButton;
    public GameObject transition_obj;

    void Start()
    {
        File.Delete(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() + ".txt");
    }
    public void end_ani()
    {
        GoToMainMenuButton.SetActive(true);
    }

    public void go_to_main_menu()
    {
        transition_obj.SetActive(true);
        transition_obj.GetComponent<Animator>().Play("close");
        StartCoroutine(wait_to_main_menu());
    }
    IEnumerator wait_to_main_menu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
