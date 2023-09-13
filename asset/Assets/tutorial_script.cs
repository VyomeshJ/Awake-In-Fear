using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class tutorial_script : MonoBehaviour
{
    public TextMeshProUGUI text_obj;
    public string[] pages;
    public int current_page;
    public GameObject continue_txt;
    public GameObject continue_last_txt;
    public InputMaster inputMaster;
    public bool last_page;
    public bool finished_page;
    public GameObject Load_Panel;
    private void OnEnable()
    {
        inputMaster.Player.Enable();
        inputMaster.Player.Enter.performed += Next_Page;
    }
    private void OnDisable()
    {
        inputMaster.Player.Disable();
    }
    private void Awake()
    {
        inputMaster = new InputMaster();
    }
    public void Start()
    {
        continue_txt.SetActive(false);
        continue_last_txt.SetActive(false);
        Read_Txt(pages[0]);
    }
    void Next_Page(InputAction.CallbackContext ctx)
    {
        if(finished_page)
        {
            if(last_page)
            {
                Load_Panel.GetComponent<Animator>().Play("close");
            }
            finished_page = false;
            continue_txt.SetActive(false);
            
            if (current_page == pages.Length - 2)
            {
                last_page = true;
            }
            if (current_page < (pages.Length - 1))
            {
                current_page += 1;
                Read_Txt(pages[current_page]);
            }
        }
        
    }
    
    public void Read_Txt(string txt)
    {
        text_obj.text = "";
        text_obj.text = txt;
        if(last_page)
        {
            continue_last_txt.SetActive(true);
        }
        else
        {
            continue_txt.SetActive(true) ;
        }
        finished_page = true;
    }
}
