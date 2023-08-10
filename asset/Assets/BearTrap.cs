using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    bool playertagged;
    public int counter = 0;
    bool routineflag = false;
    //public P cylinder;
    bool damagetaken = false;
    // Start is called before the first frame update
    void Update()
    {
        if (routineflag == true)
            escapeTrap();

        if (playertagged == false)
        {
            //cylinder.enabled = true;
            damagetaken = false;
            // gameObject.GetComponent<PlayerMovement>.enabled = true;
        }

        if (damagetaken == true)
        {
            StartCoroutine(trapdamage());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            playertagged = true;
            if (playertagged == true)
            {
                //other.gameObject.GetComponent<PlayerMovement>().enabled = false;
                StartCoroutine(trapdamage());

                routineflag = true;

            }
        }
    }

    IEnumerator trapdamage()
    {
        damagetaken = false;
       // cylinder.transform.Find("Cylinder").gameObject.GetComponent<PlayerGeneralScript>().takeDamage(10);
        yield return new WaitForSeconds(0.7f);

        damagetaken = true;
    }
    void escapeTrap()
    {

        //if (GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMasterScript>().F_Pressed_Bool)
        //{
          //  counter += 1;
            //GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMasterScript>().F_Pressed_Bool = false;
        //}

        if (counter == 15)
        {
            playertagged = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playertagged = false;
        }
    }


}
