using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class save_trigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("saving game");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>().SaveGame_event();
        }
    }
}
