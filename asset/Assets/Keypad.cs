using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public string password;
    public string typed_password;
    public GameObject typing_area;
    public GameObject safe;
    public AudioManagerScript AudioManager;
    AudioSource audio_source;

    private void Start()
    {
        password = SaveVariables.safe_code;
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        audio_source = gameObject.GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SaveVariables.GamePaused = true;
        Time.timeScale = 0;
    }
    private void OnDisable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SaveVariables.GamePaused = false;
        Time.timeScale = 1;
    }


    public void enter_num(string key_num)
    {
        if(typed_password.Length != 4){
            audio_source.clip = AudioManager.Enter_Num_Sound;
            audio_source.Play();
            typed_password += key_num;
            typing_area.GetComponent<TextMeshProUGUI>().text = typed_password;
        }
    }
    public void clear_password()
    {
        typed_password = string.Empty;
        typing_area.GetComponent<TextMeshProUGUI>().text = string.Empty;

    }
    public void submit_password()
    {
        
        if (typed_password == password)
        {
            audio_source.clip = AudioManager.Code_Correct_Sound;
            audio_source.Play();
            typing_area.GetComponent<TextMeshProUGUI>().text = "Correct passwork";
            StartCoroutine(password_submit());
        }
        else
        {
            audio_source.clip = AudioManager.Code_Error_Sound;
            audio_source.Play();
            typing_area.GetComponent<TextMeshProUGUI>().text = "Incorrect passwork";
        }
    } 
    IEnumerator password_submit()
    {
        yield return new WaitForSecondsRealtime(1);
        close_keypad();
        safe.GetComponent<safe_script>().open_safe();
        
    }
    public void close_keypad()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SaveVariables.GamePaused = false;
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
