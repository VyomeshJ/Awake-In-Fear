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

    public void rumble()
    {
        self_AudioSource.clip = AudioManager.Door_Locked_Sound;
        self_AudioSource.Play();
    }
    public void openDoor()
    {
        
        Debug.Log("open door");
        DoorInAniState = true;
        DoorOpen = true;
        Debug.Log(DoorOpen);
        //doorSound.Play();
        door.Play("Open");
        self_AudioSource.clip = AudioManager.DoorOpenSound;
        self_AudioSource.Play();
    }

    public void closeDoor()
    {
        Debug.Log("door closed");
        DoorOpen = false;
        Debug.Log(DoorOpen);
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
        if (DoorStateLocked) gameObject.tag = "Untagged";
        else gameObject.tag = "InteractableObject";

    }
}
