using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_script : MonoBehaviour
{
    
    private void Start()
    {
        Destroy(gameObject, 5);
       
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Substring(0,10) != "Projectile")
        {
            Destroy(gameObject);
        }
        
    }
}
