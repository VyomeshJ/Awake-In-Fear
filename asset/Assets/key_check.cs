using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_check : MonoBehaviour
{
    // Start is called before the first frame update
    public int key_num;
    void Start()
    {
        if(key_num == 1 && SaveVariables.Key1Available)
        {
           gameObject.SetActive(false);
        }
        else if(key_num == 2 && SaveVariables.Key2Available)
        {
            gameObject.SetActive(false);
        }
        else if(key_num == 3 && SaveVariables.Key3Available)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
