using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRange : MonoBehaviour
{
    public bool InRange;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InRange = true;

        }
    }
    private void OnTriggerExit(Collider other)
    {
        InRange = false;
    }
    private void Update()
    {
        if(SaveVariables.PlayerHiding_Closet == true || SaveVariables.PlayerHiding_Bed == true)
        {
            InRange = false;
        }
    }
}
