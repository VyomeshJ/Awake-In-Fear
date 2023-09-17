using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeObjects : MonoBehaviour
{

    //public Transform Obj1;
    public Transform Obj2;
    public Transform Obj3;
    public Transform Obj4;
    public Transform[] spawnpoints;

    public GameObject notedup1;
    public GameObject notedup2;

    int indexNumber;
    int indexNumber2;
    int indexNumber3;
    int indexNumber4;
    bool inloop = true;

    // Start is called before the first frame update
    void Start()
    {
        
            indexNumber = Random.Range(0, 1);
            indexNumber2 = Random.Range(2, 6); 
            indexNumber3 = Random.Range(7, 11);
            indexNumber4 = Random.Range(12, 16);

            if(indexNumber == 0)
        {
            notedup1.SetActive(true);
        }
        else if (indexNumber == 1)
        {
            notedup2.SetActive(true);
        }



        Obj2.position = spawnpoints[indexNumber2].position;
                Obj3.position = spawnpoints[indexNumber3].position;
                Obj4.position = spawnpoints[indexNumber4].position;




        //Obj3.position = spawnpoints[indexNumber].position;
        //Obj4.position = spawnpoints[indexNumber].position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
