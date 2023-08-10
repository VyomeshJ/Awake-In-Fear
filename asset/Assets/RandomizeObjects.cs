using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomizeObjects : MonoBehaviour
{
    public Transform Obj1;
    public Transform Obj2;
    public Transform Obj3;
    public Transform Obj4;
    public Transform[] spawnpoints;

    int indexNumber;
    int indexNumber2;
    bool inloop = true;

    // Start is called before the first frame update
    void Start()
    {
        
            indexNumber = Random.Range(0, spawnpoints.Length);
            indexNumber2 = Random.Range(0, spawnpoints.Length);

            while (indexNumber == indexNumber2)
            {
                indexNumber2 = Random.Range(0, spawnpoints.Length);
            }
            
                
                Obj1.position = spawnpoints[indexNumber].position;
                Obj2.position = spawnpoints[indexNumber2].position;
                
            

        
        //Obj3.position = spawnpoints[indexNumber].position;
        //Obj4.position = spawnpoints[indexNumber].position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
