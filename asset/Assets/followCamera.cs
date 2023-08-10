using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    //public Transform camerapos;
    private Vector3 vectOffset;
    private GameObject goFollow;
    public float speed = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
        goFollow = Camera.main.gameObject;
        vectOffset = transform.position - goFollow.transform.position;

        //transform.position = new Vector3(camerapos.position.x, camerapos.position.y, camerapos.position.z );
        //transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, Camera.main.transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = goFollow.transform.position + vectOffset;
        transform.rotation = Quaternion.Slerp(transform.rotation, goFollow.transform.rotation, speed * Time.deltaTime);
    }

}
