using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickTrigger : MonoBehaviour
{
    bool picked_trigger;
    public GameObject doorTriggered;

    public void Picked()
    {
        doorTriggered.GetComponent<doorScript>().PickTriggered();
    }
    
}
