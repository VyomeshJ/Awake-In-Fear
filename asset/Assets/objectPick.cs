using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPick : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;



    public float throwForce = 500f;
    public float pickUpRange = 15f;
    private float rotationSensitivity = 1f;
    public GameObject heldObj;
    public Rigidbody heldObjRb;
    public bool canDrop = true;
    public int LayerNumber;
    public bool p_pressed;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");


    }

    void FixedUpdate()
    {
        //flashlightpos.transform.rotation = Quaternion.Euler(0, 180, 0);


        if (p_pressed)
        {
            p_pressed = false;
            Debug.Log("die mf 0");
            if (heldObj == null)
            {
                Debug.Log("die mf 1");
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                {
                    if (hit.transform.gameObject.tag == "dragable")

                    {
                        Debug.Log("die mf 2");
                        PickUpObject(hit.transform.gameObject);
                        if (hit.transform.gameObject.name == "s")
                        {

                            Debug.Log(hit.transform.position);

                            hit.transform.eulerAngles = new Vector3(0, 180, 0);

                        }
                    }
                }
            }
            else
            {
                if (canDrop == true)
                {
                    //StopClipping();
                    DropObject();
                }
            }
        }
        if (heldObj != null)
        {
            MoveObject();
            /*if (Input.GetKeyDown(KeyCode.Mouse1) && canDrop == true)
            {
                StopClipping();
                ThrowObject();
            }*/
        }
    }
    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            heldObjRb.isKinematic = true;
            heldObjRb.transform.parent = holdPos.transform;
            heldObj.layer = LayerNumber;
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        }
    }
    void DropObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObj = null;
    }
    void MoveObject()
    {
        heldObj.transform.position = holdPos.transform.position;
    }
    void ThrowObject()
    {
        Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        heldObj.layer = 0;
        heldObjRb.isKinematic = false;
        heldObj.transform.parent = null;
        heldObjRb.AddForce(transform.forward * throwForce);
        heldObj = null;
    }
    /*void StopClipping()
    {
        var clipRange = Vector3.Distance(heldObj.transform.position, transform.position);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        if (hits.Length > 1)
        {
            heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f);
        }
    }*/
}
