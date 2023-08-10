using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlick : MonoBehaviour
{

    public bool flickering = false;
    public float timedelay;

    void Update()
    {
        if (flickering == false)
        {
            StartCoroutine(startflickering());

        }
        IEnumerator startflickering()
        {
            flickering = true;
            this.gameObject.GetComponent<Light>().enabled = false;
            timedelay = Random.Range(0.01f, 2f);
            yield return new WaitForSeconds(timedelay);
            this.gameObject.GetComponent<Light>().enabled = true;
            yield return new WaitForSeconds(0.1f);

            flickering = false;


        }
    }
}
