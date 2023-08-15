using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public AudioSource aud;
    public GameObject Player;
    public float range;
    private void Update()
    {
        if(Vector2.Distance(transform.position, Player.transform.position) < range)
        {
            //aud.volume = (range - Vector2.Distance(transform.position, Player.transform.position))/range;
        }
    }
}
