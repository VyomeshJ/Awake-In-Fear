using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButton : MonoBehaviour
{
    public GameObject door;
    public bool opened;
    public void OpenDoor()
    {
        if (!door.GetComponent<doorScript>().DoorInAniState)
        {
            if (opened)
            {
                opened = false;
                door.GetComponent<doorScript>().openDoor();
            }
            else
            {
                opened = true;
                door.GetComponent<doorScript>().closeDoor();

            }
        }
         
    }
}
