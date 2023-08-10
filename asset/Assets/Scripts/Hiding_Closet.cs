using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiding_Closet : MonoBehaviour
{
    public bool Closet_Opened;
    public bool ani = false;
    public AudioClip Closet_Open_Sound;
    public AudioClip Closet_Close_Sound;

    public void Open_Close()
    {
        Debug.Log("open closet");
        if (!ani)
        {
            ani = true;
            if (!Closet_Opened)
            {
                gameObject.GetComponent<AudioSource>().clip = Closet_Open_Sound;
                gameObject.GetComponent<AudioSource>().Play();
                Closet_Opened = true;
                gameObject.GetComponent<Animator>().Play("clsopenanim");
            }
            else
            {
                gameObject.GetComponent<AudioSource>().clip = Closet_Close_Sound;
                gameObject.GetComponent<AudioSource>().Play();
                Closet_Opened = false;
                gameObject.GetComponent<Animator>().Play("clsanim");
            }
        }
    }
    
    public void Ani_End()
    {
        ani = false;
    }
}
