using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public bool triggered;
    public bool triggerDone;
    public int triggerNum;
    int triggeredNumState;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveVariables.trigger_to_trigger >= triggerNum && triggered)
        {
            triggered = true;
            triggerDone = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!triggerDone) triggered = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
