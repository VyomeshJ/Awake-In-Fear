using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaffeineHUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Find("Mask").GetComponent<RectTransform>().sizeDelta = new Vector2(365, (SaveVariables.CaffeineLevel / 100) * 654);
    }
}
