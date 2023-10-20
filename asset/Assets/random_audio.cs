using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class random_audio : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] audioClips;
    void Start()
    {
        StartCoroutine(random_audio_start());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator random_audio_start()
    {
        float rand_num = Random.Range(3, 10);
        int rand_index = Random.Range(0, 3);
        Debug.Log("yo " + rand_index);
        gameObject.GetComponent<AudioSource>().clip = audioClips[rand_index];
        gameObject.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(audioClips[rand_index].length);
        yield return new WaitForSeconds(rand_num);
        StartCoroutine(random_audio_start());
    }
}
