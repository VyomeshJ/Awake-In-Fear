using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_audio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] audioClips;
    public int aud_playing = -1;
    void Start()
    {
        StartCoroutine(random_audio_start());
    }

    // Update is called once per frame
    void Update()
    {
        if(aud_playing == 1)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.1f;
        }
        else if(aud_playing == 2 || aud_playing == 0)
        {
            gameObject.GetComponent<AudioSource>().volume = 0.4f;
        }
        
        
    }
    IEnumerator random_audio_start()
    {
        float rand_num = Random.Range(30, 90);
        int rand_index = Random.Range(0, 3);
        while (rand_index == aud_playing)
        {
            rand_index = Random.Range(0, 3);
        }
        aud_playing = rand_index;
       
        gameObject.GetComponent<AudioSource>().clip = audioClips[rand_index];
        gameObject.GetComponent<AudioSource>().Play();
        //yield return new WaitForSeconds(audioClips[rand_index].length);
        yield return new WaitForSeconds(rand_num);
        StartCoroutine(random_audio_start());
    }
}
