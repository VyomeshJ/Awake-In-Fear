using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caffeine_prompt : MonoBehaviour
{
    // Start is called before the first frame update
    float timer = 0;
    void FixedUpdate()
    {
        while(timer <= 5f)
        {
            timer += Time.fixedUnscaledTime;
        }
        if(timer >= 5f)
        {
            Destroy(gameObject);
        }
    }
}
