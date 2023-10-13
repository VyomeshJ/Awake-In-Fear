using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Claims;
using UnityEngine.LowLevel;
using UnityEngine.Rendering;
using System;

public class PlayerScript : MonoBehaviour
{
    public CellNavmesh CellNavmesh;
    public GameObject ManageUI;
    public GameObject you_died_txt;
    public GameObject saving_txt;
    public GameObject game_load;
    public bool GeneratorOpened;
    public Collider ViewCollider;
    public bool set_pos;
    public GameObject FloorDoor;
    public AudioSource shotgun_audio_source;
    public GameObject LightContainer;
    
    public bool electricityopen = false;

    public GameObject wrenchObj;
    int playerfloor;
    public GameObject RemovedLights;
    public AudioSource ClockSound1;

    public bool Move_Null;
    public GameObject trigger3_door;
    public float Base_Mov_Speed;
    public AudioManagerScript AudioManager;

    public LightTrigger trigger1;
    public Light light1;
    public Light light0;
    public LightTrigger trigger2;
    //public Light light2;
    public GameObject lightevent;
    public GameObject triggerobject2;
    public LightTrigger trigger3;
    public BoxCollider collider3;
    public LightTrigger trigger4;
    public GameObject removable;

    public float PlayerAccessRange;
    public Texture cookie;
    public GameObject PicturePuzzleCanvas;
    int Raydistance = 3;
    public GameObject MainCanvas;
    public bool flashon;
    public GameObject FlashLightPrefab;
    public GameObject flashLight;
    public Camera cam;
    public bool PickableObjectFound;
    public bool InteractableObjectFound;
    public GameObject E_Prompt;
    public GameObject F_Prompt;
    public GameObject F_Prompt_Hide;
    public GameObject F_Prompt_fix;
    public GameObject No_Electricity_Prompt;
    public GameObject PointingToObj;
    public doorScript doorScript;
    int keys;
    public AudioSource lockedDoor;
    public GameObject NoteUI;
    public bool holdingnote = false;

    [Header("battery")]
    public TMP_Text batteryNumText;
    public TMP_Text batteryChargeText;

    [Header("Shooting")]
    public bool holdingweapon = false;
    public GameObject thrownProjectile;
    public Transform attackPosition;
    public Transform camPosition;

    float horizontalForce = 50;
    float verticalForce = 1;

    float[] verticalForces = {1.0f, 7f, 10};

    public float totalbullets = 10;
    float bulletcooltime = 0.8f;
    bool throwable = true;
    public GameObject shotgun;
    bool haveShotgun = false;
    public GameObject cameraholder;
    public Animator shotgunanimator;
    bool ReadyZoomOut;
    public GameObject InventoryGUI;

    public Animator fpsanimator;

    [Header("Stamina")]
    public float stamina = 100;
    float maxStamina = 100;
    public Slider staminaSlider;
    float decVal = 8;
    float incVal = 4;
    public bool sprinting;
    
    public CharController_Motor controllerScript;
    
    
    [Header("Player Holder")]
    public GameObject FlashlightHUD;

    public float SoundDecibel;
    public Vector3 PlayerPos;

    public navmeshAI navmeshai;
    
    public GameObject CaffeineSliderObj;
    public EventSystem eventSystem;
    public Sprite[] FilledInv;
    public string currentSelectedItem;
    public Vector3 WarpPosition;
    public bool madeSound;
    int dcounter = 1;
    int Ccounter = 1;
    public Vector3 PositionBfrHiding;
    public bool gun_zoomed_in = false;
    GameObject hiding_obj;
    public GameObject CameraCarrier;
    public bool lockLook;
    public Vector3 lastPos;
    public LightFlick flicker1, flicker2, flicker3, flicker4, flicker5, flicker6;
    public GameObject KeyPromptTxt;

    public void DeathScene()
    {
        ManageUI.GetComponent<ManageUI>().SceneChange("You_Died_Scene");
    }
    private void FixedUpdate()
    {

        Vector3 pos_moved = transform.position - lastPos;
        lastPos = transform.position;
        //Debug.Log(pos_moved.magnitude);
        if(pos_moved.magnitude > 0.001 )
        {
            Move_Null = false;
        }
        else
        {
            Move_Null = true;
        }    
    }
    public void SaveGame_event()
    {
        saving_txt.SetActive(true);
        game_load.GetComponent<GameLoad>().SaveGame();
    }
    public void StopHiding()
    {
        hiding_obj.GetComponent<BoxCollider>().enabled = true;
        gameObject.transform.Find("CameraCarrier").GetComponent<headhbob>().enabled = true;
        transform.position = PositionBfrHiding;
        gameObject.GetComponent<CharacterController>().enabled = true;
        SaveVariables.PlayerHiding_Bed = false;
        SaveVariables.PlayerHiding_Closet = false;
    }
    public void initialize_player_pos()
    {
        Debug.Log("call 1");
        //gameObject.transform.position = SaveVariables.Player_Initial_Pos;
    }
    public IEnumerator initiate_player_pos()
    {
        float counter = 0;
        while(counter < 0.5)
        {
            counter += Time.deltaTime;
            transform.position = SaveVariables.Player_Initial_Pos;
            yield return null;
        }
    }

    void Start(){
        StartCoroutine(initiate_player_pos());

        //gameObject.transform.position = SaveVariables.Player_Initial_Pos;
        
        start_triggers();
        AudioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManagerScript>();
        SaveVariables.CaffeineLevel = 100;
        shotgun.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        //flashLight = Instantiate(FlashLightPrefab, Vector3.zero, Quaternion.identity);
        //flashLight.transform.localRotation = Quaternion.Euler(0, 0, 0);
       

        //flashLight.transform.parent = gameObject.transform.Find("HoldPosition");
        //flashLight.transform.localPosition = new Vector3(0,0,0);
        
        if (!SaveVariables.FlashAvailable) flashLight.SetActive(false);
    }

    public void SoundReset()
    {
        //SoundDecibel = 0;
        //navmeshai.m_IsPatrol = true;

    }
    public void SoundAdd(float sound)
    {
        SoundDecibel = 0f;
        SoundDecibel =+ sound;
        //SoundDecibel = 0;

        //Invoke("SoundReset", 6f);
        //CancelInvoke();
    }


    public void InventoryMenu(){
        if (!SaveVariables.InventoryOpen)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SaveVariables.InventoryOpen = true;
            Time.timeScale = 0f;
            InventoryGUI.SetActive(true);
            eventSystem.SetSelectedGameObject(InventoryGUI.transform.Find("FlashlightHolder").Find("Image").gameObject);
            if (SaveVariables.FlashAvailable == true) InventoryGUI.transform.Find("FlashlightHolder").gameObject.GetComponent<Image>().sprite = FilledInv[0];
            if (SaveVariables.CaffeinePillsAvailable > 0) InventoryGUI.transform.Find("CaffeineHolder").gameObject.GetComponent<Image>().sprite = FilledInv[1];

            if (SaveVariables.Key1Available) InventoryGUI.transform.Find("Key1").gameObject.GetComponent<Image>().sprite = FilledInv[2];
            if (SaveVariables.Key2Available) InventoryGUI.transform.Find("Key2").gameObject.GetComponent<Image>().sprite = FilledInv[3];
            if (SaveVariables.Key3Available) InventoryGUI.transform.Find("Key3").gameObject.GetComponent<Image>().sprite = FilledInv[4];
        }
        else if (SaveVariables.InventoryOpen){
            Debug.Log("close inv");
            
            Time.timeScale = 1f;
            SaveVariables.InventoryOpen = false;
            InventoryGUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void OpenFlashlight(){

        if(SaveVariables.FlashAvailable && currentSelectedItem == "flashlight" && !SaveVariables.InventoryOpen){
            flashon = true;
            FlashlightHUD.SetActive(true);
            batteryChargeText.gameObject.SetActive(true);
            batteryNumText.gameObject.SetActive(true);
            Light flashintensity;
            flashintensity = flashLight.GetComponent<Light>();
            
            flashintensity.cookie = cookie;
            flashintensity.range = 50;
           // Debug.Log(flashintensity.range);
            flashLight.SetActive(true);
            flashintensity.intensity = 60;
        }
    }

    public void Flashlight(){
        
        if (SaveVariables.FlashAvailable && SaveVariables.flashtime > 0 && currentSelectedItem == "flashlight" && !SaveVariables.InventoryOpen){
            if (!flashon)
            {
               

                    flashon = true;
                    flashLight.SetActive(true);
                    gameObject.GetComponent<AudioSource>().clip = AudioManager.FlashLight_On;
                    gameObject.GetComponent<AudioSource>().Play();
                    /*
                    FlashlightHUD.SetActive(true);
                    batteryChargeText.gameObject.SetActive(true);
                    batteryNumText.gameObject.SetActive(true);
                    flashLight.SetActive(true);
                    */

                //InventoryMenu();                
            }
            else{
                flashon = false;
                flashLight.SetActive(false);
                gameObject.GetComponent<AudioSource>().clip = AudioManager.FlashLight_Off;
                gameObject.GetComponent<AudioSource>().Play();
                /*
                FlashlightHUD.SetActive(false);
                batteryChargeText.gameObject.SetActive(false);
                batteryNumText.gameObject.SetActive(false);
                flashLight.SetActive(false);
                if(SaveVariables.InventoryOpen) InventoryMenu();
                */
            }
        }
    }
    
    void cooldownreset()
    {
        throwable = true;
    }
    public void LClick()
    {
        if (holdingweapon && totalbullets > 0 && throwable)
        {
            shotgun_audio_source.Play();
            gameObject.GetComponent<AudioSource>().clip = AudioManager.Shotgun_Shoot;
            gameObject.GetComponent<AudioSource>().Play();
            shootprojectile();

        }
        /*if (holdingweapon)
        {
            shootprojectile();
        }*/
    }
    public void _disableShotgun()
    {
        StartCoroutine(disableShotGun());
    }
    public IEnumerator disableShotGun()
    {
        yield return new WaitForSeconds(0.5f);
        shotgun.SetActive(false);
    }
    
    void shootprojectile()
    {
        throwable = false;
        StartCoroutine(gameObject.transform.Find("CameraCarrier").gameObject.GetComponent<camera_recoil_script>().Shake());
        
        GameObject Bullet = Instantiate(thrownProjectile, attackPosition.position, camPosition.rotation);
        GameObject Bullet1 = Instantiate(thrownProjectile, attackPosition.position, camPosition.rotation);
        GameObject Bullet2 = Instantiate(thrownProjectile, attackPosition.position, camPosition.rotation);
        GameObject Bullet3 = Instantiate(thrownProjectile, attackPosition.position, camPosition.rotation);

        Rigidbody projectilerb = Bullet.GetComponent<Rigidbody>();
        Rigidbody projectilerb1 = Bullet1.GetComponent<Rigidbody>();
        Rigidbody projectilerb2 = Bullet2.GetComponent<Rigidbody>();
        Rigidbody projectilerb3 = Bullet3.GetComponent<Rigidbody>();

        //GameObject Bullets = Instantiate(thrownProjectile, attackPosition.position, camPosition.rotation);
        //Rigidbody projectilerbs = Bullet.GetComponent<Rigidbody>();

        Vector3 projectileforce = camPosition.transform.forward * horizontalForce + transform.up * verticalForces[0];
        Vector3 projectileforce1 = camPosition.transform.forward * horizontalForce + transform.up  * verticalForces[1];
        Vector3 projectileforce2 = camPosition.transform.forward * horizontalForce + transform.up * verticalForces[2];
        

        projectilerb.AddForce(projectileforce, ForceMode.Impulse);
        projectilerb1.AddForce(projectileforce1, ForceMode.Impulse);
        projectilerb2.AddForce(projectileforce2, ForceMode.Impulse);
        projectilerb3.AddForce(projectileforce2, ForceMode.Impulse);




        // projectilerbs.AddForce(projectileforce, ForceMode.Impulse);

        totalbullets--;
       
        Invoke(nameof(cooldownreset), bulletcooltime);

        //DestroyProjectile();

    }
    public void InteractWithObject(){
        if(PointingToObj != null){
            if (PointingToObj.name.Length >= 9 && PointingToObj.name.Substring(0, 9) == "generator" && currentSelectedItem == "wrench")
            {
                if (!GeneratorOpened)
                {
                    GeneratorOpened = true;
                    
                    ElectricityOpen();
                    PointingToObj.transform.Find("Cube").GetComponent<AudioSource>().Play();
                    StartCoroutine(ShowPrompt("Floor 1 door opened"));
                }
                
            }
            Debug.Log(PointingToObj.name);
            if (PointingToObj.name.Length >= 4 && PointingToObj.name.Substring(0, 4) == "safe")
            {
                Debug.Log("open keypad");
                PointingToObj.GetComponent<safe_script>().open_keypad();
            }
            if (PointingToObj.name.Length >= 10 && PointingToObj.name.Substring(0, 10) == "OpenButton"){
                PointingToObj.GetComponent<OpenButton>().OpenDoor();
            }
            
            
            if (PointingToObj.name.Length >= 10 && PointingToObj.name.Substring(0, 10) == "HideCloset"){
               
                gameObject.GetComponent<CharacterController>().enabled = false;
                PointingToObj.GetComponent<BoxCollider>().enabled = false;
                hiding_obj = PointingToObj;
                gameObject.transform.Find("CameraCarrier").GetComponent<headhbob>().enabled = false;
               
                //gameObject.GetComponent<>
                PositionBfrHiding = gameObject.transform.position;
                SaveVariables.PlayerHiding_Closet = true;
                gameObject.transform.position = PointingToObj.transform.Find("HidePosition").position;

                //gameObject.GetComponent<CharacterController>().enabled = true;
                //gameObject.transform.localPosition = PointingToObj.transform.parent.Find("HidePosition").position;
                if (!lockLook)
                {
                    lockLook = true;
                    transform.LookAt(PointingToObj.transform.Find("Lookat"));
                    transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
                    //cameraholder.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            if (PointingToObj.name.Length >= 7 && PointingToObj.name.Substring(0, 7) == "HideBed")
            {

                gameObject.GetComponent<CharacterController>().enabled = false;
                PointingToObj.GetComponent<BoxCollider>().enabled = false;
                hiding_obj = PointingToObj;
                gameObject.transform.Find("CameraCarrier").GetComponent<headhbob>().enabled = false;

                //gameObject.GetComponent<>
                PositionBfrHiding = gameObject.transform.position;
                SaveVariables.PlayerHiding_Bed = true;
                gameObject.transform.position = PointingToObj.transform.Find("HidePosition").position;
                //gameObject.GetComponent<CharacterController>().enabled = true;
                //gameObject.transform.localPosition = PointingToObj.transform.parent.Find("HidePosition").position;
                if (!lockLook)
                {
                    lockLook = true;
                    gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z);
                    cameraholder.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
            if (PointingToObj.name.Length >= 4 && PointingToObj.name.Substring(0,4) == "Door"){
                if (PointingToObj.GetComponent<doorScript>().DoorStateLocked)
                {
                    PointingToObj.GetComponent<doorScript>().rumble();
                }
                if (!PointingToObj.GetComponent<doorScript>().DoorStateLocked && !PointingToObj.GetComponent<doorScript>().DoorInAniState && !PointingToObj.GetComponent<doorScript>().DoorOpen)
                {
                    
                    PointingToObj.GetComponent<doorScript>().openDoor();

                    SoundAdd(20);
                }
                else if(!PointingToObj.GetComponent<doorScript>().DoorStateLocked && !PointingToObj.GetComponent<doorScript>().DoorInAniState && PointingToObj.GetComponent<doorScript>().DoorOpen)
                {
                  
                    PointingToObj.GetComponent<doorScript>().closeDoor();
                    SoundAdd(20);
                }
            }
            if(PointingToObj.name.Length >= 6 && PointingToObj.name.Substring(0, 6) == "Drawer")
            {
                Animator draweranim = PointingToObj.GetComponent<Animator>();
                if (draweranim.GetCurrentAnimatorStateInfo(0).IsName("closedrw"))
                {
                    draweranim.SetBool("opened", true);
                    PointingToObj.GetComponent<AudioSource>().clip = AudioManager.Drawer_Open_Sound;
                    PointingToObj.GetComponent<AudioSource>().Play();
                    //dcounter++;
                    //draweranim.Play("drwanim");

                }

                else if (draweranim.GetCurrentAnimatorStateInfo(0).IsName("drwanim"))
                {
                    draweranim.SetBool("opened", false);
                    PointingToObj.GetComponent<AudioSource>().clip = AudioManager.Drawer_Close_Sound;
                    PointingToObj.GetComponent<AudioSource>().Play();
                    //dcounter++;
                    //draweranim.Play("closedrw");
                }

                

            }
           
            
            if (PointingToObj.name.Length >= 6 && PointingToObj.name.Substring(0,6) == "closet")
            {
                Debug.Log("closet here buddy");
                PointingToObj.GetComponent<Hiding_Closet>().Open_Close();

                /*Animator clsanim = PointingToObj.GetComponent<Animator>();
           
                if (clsanim.GetCurrentAnimatorStateInfo(0).IsName("clsanim"))
                {
                    clsanim.SetBool("Open", true);
               

                }

                else if (clsanim.GetCurrentAnimatorStateInfo(0).IsName("clsopenanim"))
                {
                    clsanim.SetBool("Open", false);
               
                }
                if (PointingToObj.GetComponent<Outline>() != null)
                {
                    PointingToObj.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = PointingToObj.AddComponent<Outline>();
                    PointingToObj.GetComponent<Outline>().OutlineColor = Color.white;
                    PointingToObj.GetComponent<Outline>().OutlineWidth = 10f;
                }*/
            }
            
            

            
        }
    }
    public void SetupInventory(){
        
    }
    public void Num1(){
        if(SaveVariables.GunAvailable && !holdingweapon && SaveVariables.InventoryOpen){
            InventoryMenu();
      
            holdingweapon = true;
            shotgun.SetActive(true);
            
            //shotgunanimator.Play("pick up shotgun");
        }
        else if(SaveVariables.GunAvailable && holdingweapon){
            holdingweapon = false;
            shotgun.SetActive(false);
            if(SaveVariables.InventoryOpen) InventoryMenu();
        }
    }
    IEnumerator ShowPrompt(string txt_message)
    {
        KeyPromptTxt.SetActive(true);
        KeyPromptTxt.GetComponent<TextMeshProUGUI>().text = txt_message;
        yield return new WaitForSeconds(2);
        KeyPromptTxt.SetActive(false);
    }
    public void PickUpObject(){
        if(PointingToObj != null){

            if(PointingToObj.name.Length >= 6 && PointingToObj.name.Substring(0, 6) == "Wrench")
            {
                Destroy(PointingToObj);
                SaveVariables.WrenchAvailable = true;
            }
            if(PointingToObj.name.Length >= 10 && PointingToObj.name.Substring(0, 10) == "Flashlight"){
                Destroy(PointingToObj);
                SaveVariables.FlashAvailable = true;
                
                SaveVariables.FlashAvailable = true;
                SaveVariables.flashtime = 100;
            }
            if(PointingToObj.name.Length >= 7 && PointingToObj.name.Substring(0,7) == "battery"){
                Destroy(PointingToObj);
                SaveVariables.NumBatteries++;
            }
            if (PointingToObj.name.Length >= 4 && PointingToObj.name.Substring(0,4) == "key1")
            {
                StartCoroutine(ShowPrompt("Door 1 unlocked"));
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<doorScript>().DoorStateLocked = false;
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<AudioSource>().clip = AudioManager.Door_Unlocked_Sound;
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<AudioSource>().Play();
                Destroy(PointingToObj);
               
                SaveVariables.Key1Available = true;
            }
            if (PointingToObj.name.Length >= 4 && PointingToObj.name.Substring(0, 4) == "key2")
            {
                StartCoroutine(ShowPrompt("Door 2 unlocked"));
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<doorScript>().DoorStateLocked = false;
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<AudioSource>().clip = AudioManager.Door_Unlocked_Sound;
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<AudioSource>().Play();
                Destroy(PointingToObj);
                
                SaveVariables.Key2Available = true;
            }
            if (PointingToObj.name.Length >= 4 && PointingToObj.name.Substring(0, 4) == "key3")
            {
                StartCoroutine(ShowPrompt("Door 3 unlocked"));
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<doorScript>().DoorStateLocked = false;
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<AudioSource>().clip = AudioManager.Door_Unlocked_Sound;
                PointingToObj.GetComponent<KeyScript>().door.GetComponent<AudioSource>().Play();
                Destroy(PointingToObj);
              
                SaveVariables.Key3Available = true;
            }
            if (PointingToObj.name.Length >= 7 && PointingToObj.name.Substring(0, 7) == "shotgun")
            {
                //PointingToObj.GetComponent<KeyScript>().door.GetComponent<doorScript>().DoorStateLocked = false;
                Destroy(PointingToObj);
                haveShotgun = true;
                
                SaveVariables.GunAvailable = true;
            }
            if (PointingToObj.name.Length >= 4 && PointingToObj.name.Substring(0, 4) == "Note")
            {
                
                //Destroy(PointingToObj);

                NoteUI.SetActive(true);
                NoteUI.transform.Find("NoteText").gameObject.GetComponent<TextMeshProUGUI>().text = PointingToObj.GetComponent<notescript>().txt;
                holdingnote = true;

               
            }
            if (PointingToObj.tag == "PieceOfImage") //WORK HERE TO MAKE THE PICTURE PUZZLE WORK
            {
                //PointingToObj.GetComponent<KeyScript>().door.GetComponent<doorScript>().DoorStateLocked = false;
                Destroy(PointingToObj);
                PicturePuzzleCanvas.GetComponent<PictureCanvas>().NewPieceAquired();

                //MainCanvas.transform.Find("Shotgun").gameObject.GetComponent<Image>().color = Color.black;
            }
        }
    }

    void increaseStamina()
    {
        stamina += incVal * Time.deltaTime;
    }

    void decreaseStamina()
    {
        if (stamina != 0)
        {
            stamina -= decVal * Time.deltaTime;
        }
    }

    public void Exit_Holdingnote(){
        holdingnote = false;
        NoteUI.SetActive(false);    
    }
    public void LShift_Enabled(){
        if(stamina > 0){
         
            madeSound = true;
            SoundDecibel = 10f;
            SoundAdd(10f);
            //Invoke("SoundReset", 2f);
            sprinting = true;
        }
    }
    public void LShift_Disabled(){
        sprinting = false;
        madeSound = false;
        //SoundDecibel = 0f;
        //SoundAdd(0f);
    }
    /*IEnumerator ShakeCamera(float duration, float magnitude)
        {
            
            Vector3 originalpos = cameraholder.transform.localPosition;
            float elapsed = 0.0f;
            
            while(elapsed < duration)
            {
                float x =  Random.Range(-0f, 0f) * magnitude;
                float y = Random.Range(-3f, 3f) * magnitude;

                cameraholder.transform.localPosition = new Vector3(x, y, originalpos.z);
                elapsed += Time.deltaTime;
                yield return null;
            }
            cameraholder.transform.localPosition = originalpos;

        }
    */

    
    public void MouseButtonZoom(){
        if (holdingweapon && totalbullets > 0)
        {
            if (!gun_zoomed_in)
            {
                gun_zoomed_in = true;
                Vector3 zoomedVector = new Vector3(-300f, 0f, 0f);
                shotgun.transform.position = zoomedVector;
                shotgunanimator.Play("Zoom shotgun");
                cam.fieldOfView = 25;
                ReadyZoomOut = true;
            }
            else
            {
                gun_zoomed_in = false;
                cam.fieldOfView = 60;
                if (ReadyZoomOut)
                    shotgunanimator.Play("zoom out");
            }
        }
        else if (gun_zoomed_in)
        {
            gun_zoomed_in = false;
            cam.fieldOfView = 60;
            if (ReadyZoomOut)
                shotgunanimator.Play("zoom out");
        }
    }
    void RemoveFLoor1()
    {
        RemovedLights.SetActive(false);
        ClockSound1.enabled = false;
    }
    public void ElectricityOpen()
    {
       
        SaveGame_event();
        StartCoroutine(CellNavmesh.Chase());
        Debug.Log("electricity opened");
        LightContainer.SetActive(true);
        ViewCollider.enabled = true;
        
        electricityopen = true;
        FloorDoor.GetComponent<Animator>().Play("Open");
        FloorDoor.GetComponent<AudioSource>().Play();

    }
    
    public void start_triggers()
    {
        if(SaveVariables.trigger_to_trigger == 1)
        {
            light0.enabled = true;
        }
        if (SaveVariables.trigger_to_trigger == 2)
        {
            light0.enabled = true;
            light1.enabled = true;
            trigger2.gameObject.SetActive(true);
        }
        if (SaveVariables.trigger_to_trigger == 3)
        {
            light0.enabled = true;
            light1.enabled = true;
            trigger2.gameObject.SetActive(true);
            SaveVariables.trigger_to_trigger = 3;
            lightevent.SetActive(true);
            trigger3.enabled = true;
            collider3.enabled = true;
        }
        if (SaveVariables.trigger_to_trigger == 4)
        {
            light0.enabled = true;
            light1.enabled = true;
            trigger2.gameObject.SetActive(true);
            SaveVariables.trigger_to_trigger = 3;
            lightevent.SetActive(true);
            trigger3.enabled = true;
            collider3.enabled = true;
            trigger3.triggerDone = true;
            trigger3_door.GetComponent<doorScript>().closeDoor();
            trigger3_door.GetComponent<doorScript>().DoorStateLocked = true;

            flicker1.enabled = true;
            flicker2.enabled = true;
            flicker3.enabled = true;
            flicker4.enabled = true;
            flicker5.enabled = true;
            flicker6.enabled = true;
        }
        if (SaveVariables.trigger_to_trigger == 5)
        {
            trigger4.triggerDone = true;
            removable.SetActive(false);
        }
    }

    void Update(){
        
        //Debug.Log(transform.position);
        
        if (transform.position.y > 22)
        {
            Debug.Log("Remmoved");
            playerfloor = 1;
            RemoveFLoor1();
            
        }
        else if (transform.position.y > 28) playerfloor = 2;

        if (!SaveVariables.PlayerHiding_Bed && !SaveVariables.PlayerHiding_Closet) lockLook = false;
        
        if (trigger1.triggered && !trigger1.triggerDone && SaveVariables.trigger_to_trigger == 1)
        {
            //light0.enabled = false;
            SaveVariables.trigger_to_trigger = 2;
            
            light1.enabled = true;
            trigger2.gameObject.SetActive(true);
            
            
        }
        if(trigger2.triggered && !trigger2.triggerDone && SaveVariables.trigger_to_trigger == 2)
        {
            //light0.enabled = false;
            //light1.enabled = false;
            SaveVariables.trigger_to_trigger = 3;
            
            //light2.enabled = true;  
            lightevent.SetActive(true);
            trigger3.enabled = true;
            collider3.enabled = true;
        }
        if (trigger3.triggered && !trigger3.triggerDone && SaveVariables.trigger_to_trigger == 3)
        {
            //lightevent.SetActive(false);
            //trigger3.enabled = false;
            //collider3.enabled = false;
            //lightevent.SetActive(false);
            light1.enabled = false;
            SaveVariables.trigger_to_trigger = 4;
            trigger3.triggerDone = true;
            trigger3_door.GetComponent<doorScript>().closeDoor();
            trigger3_door.GetComponent<doorScript>().DoorStateLocked = true;

            flicker1.enabled = true;
            flicker2.enabled = true;
            flicker3.enabled = true;
            flicker4.enabled = true;
            flicker5.enabled = true;
            flicker6.enabled = true;

            //close door
        }
        if (trigger4.triggered && !trigger4.triggerDone && SaveVariables.trigger_to_trigger == 4)
        {
            
            
            SaveVariables.trigger_to_trigger = 5;
            
            trigger4.triggerDone = true;
            removable.SetActive(false);
           
        }


        if (controllerScript.velocity == Vector3.zero)
        {
            CameraCarrier.GetComponent<headhbob>().enabled = false;
        }
        else if (!SaveVariables.PlayerHiding_Bed && !SaveVariables.PlayerHiding_Closet)
        {
            CameraCarrier.GetComponent<headhbob>().enabled = true;
        }
        if(sprinting && stamina <= 0)
        {
            sprinting = false;
        }
        //Debug.Log(eventSystem.currentSelectedGameObject);
        if(SaveVariables.CaffeineLevel > 0){
            SaveVariables.CaffeineLevel -= Time.deltaTime;
            CaffeineSliderObj.GetComponent<Slider>().value = SaveVariables.CaffeineLevel/100f;
        }
        else{
            //PLAYER DIES OR SMTH
        }

        PlayerPos = this.gameObject.transform.position;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            fpsanimator.Play("Hiding");
        }
        if(stamina > maxStamina) stamina = maxStamina;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = stamina;

        ////The input method has to change
        if(sprinting)
        {
            decreaseStamina();
        }
        else if(stamina != maxStamina)
        {
            increaseStamina();
        }

        if (sprinting)
        {
            staminaSlider.gameObject.SetActive(true);
            controllerScript.speed = Base_Mov_Speed*2;
        }
        else
        {
            staminaSlider.gameObject.SetActive(false);
            controllerScript.speed = Base_Mov_Speed;
        }

        batteryNumText.text = "Batteries: " + SaveVariables.NumBatteries.ToString();
        batteryChargeText.text = SaveVariables.flashtime.ToString("0") + "%";
        if (flashon) SaveVariables.flashtime -= 1 * Time.deltaTime;
        
        if (SaveVariables.flashtime <= 0)
        {
            if(SaveVariables.NumBatteries > 0){
                SaveVariables.NumBatteries -= 1;
                SaveVariables.flashtime += 50;
            }
            else{
                flashon = false;
                flashLight.SetActive(false);
            }
            
        }
        //incase if u reload and its over 100
        

        ////change this to the new input system
    

        //else holdingweapon = false;
        
        

        RaycastHit hit;
        
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth/2, cam.pixelHeight/2, 0));
        if (Physics.Raycast(ray, out hit))
        {
            
            if(PointingToObj != null)
            {
           
                if (hit.transform.gameObject.name != PointingToObj.name)
                {
                   
                    //Debug.Log("block outline");
                    PointingToObj = null;
                    PickableObjectFound = false;
                    InteractableObjectFound = false;
                    if (E_Prompt != null) E_Prompt.SetActive(false);
                    if (F_Prompt_Hide != null) F_Prompt_Hide.SetActive(false);
                    if (F_Prompt_fix != null) F_Prompt_fix.SetActive(false);
                    if (F_Prompt != null) F_Prompt.SetActive(false);
                 
                }
               
                
                

            }
          
            if(hit.transform.gameObject.name == "FirstFloorDoor")
            {
                No_Electricity_Prompt.SetActive(true);
            }
            else
            {
                No_Electricity_Prompt.SetActive(false);
            }
            //Debug.Log(hit.transform.gameObject.name);
            
            if (hit.transform.gameObject.tag == "PickableObject" && !PickableObjectFound && Vector2.Distance(hit.transform.gameObject.transform.position, transform.position) < PlayerAccessRange)
            {
                PickableObjectFound = true;
                if (E_Prompt != null) E_Prompt.SetActive(true);
                PointingToObj = hit.transform.gameObject;

            }
            else if ((hit.transform.gameObject.tag != "PickableObject" && PickableObjectFound) || Vector2.Distance(hit.transform.gameObject.transform.position, transform.position) >= PlayerAccessRange)
            {
                //Debug.Log("heheh");
                PickableObjectFound = false;
                if (E_Prompt != null) E_Prompt.SetActive(false);
                PointingToObj = null;
            }

            if (hit.transform.gameObject.tag == "InteractableObject" && !InteractableObjectFound && Vector2.Distance(hit.transform.gameObject.transform.position, transform.position) < PlayerAccessRange)
            {

                if (hit.transform.gameObject.name.Substring(0, 4) != "generator")
                {
                    InteractableObjectFound = true;
                    if (hit.transform.gameObject.name.Substring(0, 4) != "Hide")
                    {
                        
                        if (hit.transform.gameObject.name.Length >= 9 && hit.transform.gameObject.name.Substring(0,9) == "generator")
                        {
                            if (F_Prompt_fix != null) F_Prompt_fix.SetActive(true);
                        }
                        else
                        {
                            
                            if (F_Prompt != null) F_Prompt.SetActive(true);
                        }
                    }
                    else
                    {
                        if (F_Prompt_Hide != null) F_Prompt_Hide.SetActive(true);
                    }
                }
                else if(currentSelectedItem == "wrench")
                {
                    InteractableObjectFound = true;
                    if (F_Prompt_fix != null) F_Prompt_fix.SetActive(true);
                }
                PointingToObj = hit.transform.gameObject;
                //PointingToObj = hit.transform.gameObject;
                //// you can change the condition structure, & input.getkeydown has to change
            }
            else if ((hit.transform.gameObject.tag != "InteractableObject" && InteractableObjectFound) || Vector2.Distance(hit.transform.gameObject.transform.position, transform.position) >= PlayerAccessRange)
            {
                InteractableObjectFound = false;
                PointingToObj = null;
                if(F_Prompt != null) F_Prompt.SetActive(false);
                if (F_Prompt_Hide != null) F_Prompt_Hide.SetActive(false);
                if (F_Prompt_fix != null) F_Prompt_fix.SetActive(false);
                //PointingToObj = null;
            }

             
            /*if (hit.transform.gameObject.tag == "drawers")
            {
                
                InteractableObjectFound = true;
                F_Prompt.SetActive(true);
                PointingToObj = hit.transform.gameObject;
                Animator draweranim = PointingToObj.GetComponent<Animator>();
                
                if (Input.GetKeyDown(KeyCode.F) && draweranim.GetCurrentAnimatorStateInfo(0).IsName("closedrw"))
                {
                    draweranim.SetBool("opened", true);
                    
                    //dcounter++;
                    //draweranim.Play("drwanim");
                    
                }

                else if(Input.GetKeyDown(KeyCode.F) && draweranim.GetCurrentAnimatorStateInfo(0).IsName("drwanim"))
                {
                    draweranim.SetBool("opened", false);
                    //dcounter++;
                    //draweranim.Play("closedrw");
                }
            */




         
            /*else if(hit.transform.gameObject.tag == "closet")
            {
                InteractableObjectFound = true;
                F_Prompt.SetActive(true);
                PointingToObj = hit.transform.gameObject;
                Animator clsanim = PointingToObj.GetComponent<Animator>();
                //if (clsanim.GetCurrentAnimatorStateInfo(0).IsName("clsanim"))
                //{
                   // Debug.Log("closed");
                //}
                if (Input.GetKeyDown(KeyCode.F) && clsanim.GetCurrentAnimatorStateInfo(0).IsName("clsanim"))
                {
                    clsanim.SetBool("Open", true);
                    //Ccounter++;
                    //clsanim.Play("clsopenanim");

                }

                else if (Input.GetKeyDown(KeyCode.F) && clsanim.GetCurrentAnimatorStateInfo(0).IsName("clsopenanim"))
                {
                    clsanim.SetBool("Open", false);
                    //Ccounter++;
                    //clsanim.Play("clsanim");
                }

            }*/
        }

    }
    
}
