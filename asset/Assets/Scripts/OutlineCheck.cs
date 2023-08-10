using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineCheck : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        if(player.GetComponent<PlayerScript>().PointingToObj == gameObject)
        {
            gameObject.GetComponent<Outline>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }
}
