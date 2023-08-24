using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController_Motor : MonoBehaviour
{
    public float moveFB;
    public float moveLR;
    public float speed;
    public float MouseSensitivity;
    public float ControllerSensitivity;

    public float WaterHeight = 15.5f;
    public CharacterController character;
    public GameObject cam;

    public bool moving;
    public float rotX, rotY;
    public bool webGLRightClickRotation = true;
    public float gravity;
    bool flashon;
    public GameObject flashlight;
    public Vector2 Movement;
    public bool isGrounded;
    public float jumpHeight;

    public LayerMask groundMask;
    public Transform groundcheck;
    public float groundDistance = 0.5f;
    public Vector3 velocity;
    public float gravityValue;
    public AudioSource walkingSource;
    public bool playing;



    void Start()
    {
        //LockCursor ();
        flashon = false;
        character = GetComponent<CharacterController>();
        if (Application.isEditor)
        {
            webGLRightClickRotation = false;
        }
    }


    void CheckForWaterHeight()
    {
        if (transform.position.y < WaterHeight)
        {
            gravity = 0f;
        }
        else
        {
            gravity = gravityValue;
        }
    }



    void Update()
    {
        if (moveFB > 0 || moveFB < 0 || moveLR > 0 || moveLR < 0)
        {
            moving = true;
            if (!playing)
            {
                playing = true;
                walkingSource.Play();
            }
            

        }
        else
        {
            moving = false;
            if (playing)
            {
                playing = false;
                walkingSource.Stop();
            }
        }

        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);


        //moveFB = Input.GetAxis ("Horizontal") * speed;
        //moveLR = Input.GetAxis ("Vertical") * speed;

        /*if(GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMasterScript>().CurrentInputMethod == InputMasterScript.InputMethod.Keyboard){
			rotX = Input.GetAxis ("Mouse X");
			rotY = Input.GetAxis ("Mouse Y");
		}
		*/

        //rotX = Input.GetKey (KeyCode.Joystick1Button4);
        //rotY = Input.GetKey (KeyCode.Joystick1Button5);

        CheckForWaterHeight();






        if (webGLRightClickRotation)
        {
            //if (Input.GetKey(KeyCode.Mouse0))
            //{
                CameraRotation(cam, rotX, rotY);
            //}
        }
        else if (!webGLRightClickRotation)
        {
            CameraRotation(cam, rotX, rotY);
        }
        Vector3 movement = transform.right * moveFB * speed + transform.forward * moveLR * speed;
        //movement = transform.rotation * movement;
        character.Move(movement * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }


    }
    public void Jump()
    {
        if (isGrounded)
        {
           
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }


    void CameraRotation(GameObject cam, float rotX, float rotY)
    {
        ///if((cam.transform.rotation.x < 80 && cam.transform.rotation.x > 260) || (cam.transform.rotation.x >= 90 && rotY > 0) || (cam.transform.rotation.x <= 260 && rotY < 0)){
        //Debug.Log(cam.transform.localEulerAngles.x);
        //rotX = Mathf.Clamp(rotX, -90f, 90f);
        //transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        transform.Rotate(0, rotX * Time.deltaTime, 0);
        cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);


    }




}
