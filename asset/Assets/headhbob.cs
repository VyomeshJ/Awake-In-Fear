using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headhbob : MonoBehaviour
{
    public float walkingBobbingSpeed = 0.5f;
    public float bobbingAmount = 0.001f;
    public CharController_Motor controller;

    float defaultPosY = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.moving && controller.GetComponentInParent<PlayerScript>().Move_Null == false)
        {
            
            //Player is moving
            timer += Time.deltaTime * walkingBobbingSpeed;
            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        }
        else
        {
            
            //Idle
            timer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
        }
    }
}
