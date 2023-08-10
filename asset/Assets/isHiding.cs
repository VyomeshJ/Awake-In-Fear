using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isHiding : MonoBehaviour
{
    public navmeshAI navmeshscript;
    public GameObject enemy;
    public bool ishiding;
    // Start is called before the first frame update
    void Start()
    {
        ishiding = false;
    }
    public void OnTriggerEnter(Collider other)
    {
        ishiding = true;
    }
    public void OnTriggerExit(Collider other)
    {
        ishiding = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(navmeshscript.enabled == false)
        {
           // enemy.SetActive(false);
        
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log(ishiding);
        }
    }
}
