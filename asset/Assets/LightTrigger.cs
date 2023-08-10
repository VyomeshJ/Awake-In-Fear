using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;   
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
