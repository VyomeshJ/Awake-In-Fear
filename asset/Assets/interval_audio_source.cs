using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interval_audio_source : MonoBehaviour
{
    // Start is called before the first frame update
    public float time_delay;
    public AudioSource self_audioSource;
    void Start()
    {
        self_audioSource = GetComponent<AudioSource>();
        StartCoroutine(roar());
    }
    IEnumerator roar()
    {
        yield return new WaitForSeconds(time_delay);
        self_audioSource.Play();
        StartCoroutine(roar());
    }
}
