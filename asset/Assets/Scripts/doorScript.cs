using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public Animator door;
    public AudioSource doorSound;
    public bool DoorStateLocked;
    public bool DoorInAniState;
    public bool DoorOpen;
    public bool OpenDoorAtStart;
    
    public AudioClip OpenSound;
    public AudioClip CloseSound;
    public AudioManagerScript AudioManager;
    public AudioSource self_AudioSource;

    public int DOOR_NUM_SAVE;
    public bool save_door;



    public void rumble()
    {
        self_AudioSource.clip = AudioManager.Door_Locked_Sound;
        self_AudioSource.Play();
    }
    public void openDoor()
    {
        Debug.Log(gameObject.name);
        if (save_door)
        {
        
            SaveVariables.door_unlocked[DOOR_NUM_SAVE - 1] = true;
          
        }
       
        if (!DoorStateLocked)
        {
       
            DoorInAniState = true;
            DoorOpen = true;

            //doorSound.Play();
            Debug.Log("finally workinhg");
            door.Play("Open");
            self_AudioSource.clip = AudioManager.DoorOpenSound;
            self_AudioSource.Play();
        }
        else
        {
            self_AudioSource.clip = AudioManager.Door_Locked_Sound;
            self_AudioSource.Play();
        }
    }

    public void closeDoor()
    {
 
        DoorOpen = false;

        DoorInAniState = true;
        door.Play("Close");
        self_AudioSource.clip = AudioManager.DoorCloseSound;
        self_AudioSource.Play();
    }
    public void StopAniState(){
        DoorInAniState = false;
    }

    void Start()
    {
        
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        self_AudioSource = gameObject.GetComponent<AudioSource>();
        if(OpenDoorAtStart) door.Play("OpenedState");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (DOOR_NUM_SAVE != 0 && save_door)
        {
            if (SaveVariables.door_unlocked[0] && DOOR_NUM_SAVE == 1)
            {
                save_door = false;
                DoorStateLocked = false;

                DoorStateLocked = false;
                Debug.Log("close this door");
                closeDoor();

            }
            else if (SaveVariables.door_unlocked[DOOR_NUM_SAVE - 1])
            {
                Debug.Log("&" + gameObject.name);
                save_door = false;
                DoorStateLocked = false;
              
                DoorStateLocked = false;
                
                openDoor();
            }
        }
        //if (DoorStateLocked) gameObject.tag = "Untagged";
        //else gameObject.tag = "InteractableObject";

    }
    public void PickTriggered()
    {
     
        DoorStateLocked = false;
    }
}
