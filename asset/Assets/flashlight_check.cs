using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlight_check : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SaveVariables.FlashAvailable)
        {
            gameObject.SetActive(false);
        }
    }

}
