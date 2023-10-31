using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioSourceScript : MonoBehaviour
{
    public AudioClip start_clip;
    public AudioClip loop_clip;
    public AudioClip game_clip;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "SaveMenu" || SceneManager.GetActiveScene().name == "TutorialScene")
        {
            if(gameObject.GetComponent<AudioSource>().clip.name != "loop")
            {
                gameObject.GetComponent<AudioSource>().clip = loop_clip;
                gameObject.GetComponent<AudioSource>().loop = true;
                gameObject.GetComponent<AudioSource>().Play();
            }
         
        }
        //else if(SceneManager.GetActiveScene().name == "Scene_A")
        //{
            //if (gameObject.GetComponent<AudioSource>().clip.name != "game")
            //{
                //gameObject.GetComponent<AudioSource>().clip = game_clip;
               /// gameObject.GetComponent<AudioSource>().loop = true;
                //gameObject.GetComponent<AudioSource>().Play();
           // }
                
        //}
        else
        {
            gameObject.GetComponent<AudioSource>().Stop();
        }
    }

}
