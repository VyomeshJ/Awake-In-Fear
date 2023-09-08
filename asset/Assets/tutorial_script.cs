using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class tutorial_script : MonoBehaviour
{
    public TextMeshProUGUI text_obj;
    public string[] pages;
    public int current_page;
    public GameObject continue_txt;
    public GameObject continue_last_txt;
    public InputMaster inputMaster;
    public bool last_page;
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
    }
    void Next_Page(InputAction.CallbackContext ctx)
    {
        continue_txt.SetActive(false);
        if (current_page < (pages.Length - 1))
        {
            current_page += 1;
            StartCoroutine(Read_Txt(pages[current_page]));
        }
        if(current_page == pages.Length)
        {
            last_page = true;
        }
    }
    IEnumerator Read_Txt(string txt)
    {
        text_obj.text = "";
        for(int i =  0; i <= txt.Length; i++)
        {
            text_obj.text = txt.Substring(0, (i + 1));
            yield return new WaitForSeconds(0.1f);
        }
        text_obj.text = txt;
        if(last_page)
        {
            continue_last_txt.SetActive(true);
        }
        else
        {
            continue_txt.SetActive(true) ;
        }
    }
}
