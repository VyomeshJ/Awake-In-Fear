using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safe_script : MonoBehaviour
{
    public GameObject keypad;
    public bool safe_opened;
    public AudioManagerScript AudioManager;
    public void open_keypad()
    {
        if (!safe_opened)
        {
            keypad.SetActive(true);
            keypad.GetComponent<Keypad>().safe = gameObject;
        }
    }
    public void open_safe()
    {
        gameObject.GetComponent<Animator>().Play("safe_open");
        gameObject.name = "opened_safe";
        gameObject.tag = "Untagged";
    }
    void Start()
    {
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
    }
}
