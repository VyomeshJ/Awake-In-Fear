using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class flashlightUI : MonoBehaviour
{
    // Start is called before the first frame update
    public float[] widths;
    public int num = 0;
    public GameObject FillingMask;
    public bool state_ani;
    public GameObject batteryIcon;
    void Start()
    {
        widths[0] = 1200f;
        widths[1] = 972.11f;
        widths[2] = 878.38f;
        widths[3] = 769.03f;
        widths[4] = 683.11f;
        widths[5] = 582.18f;
        widths[6] = 484.17f;
        widths[7] = 380.22f;
        widths[8] = 282.21f;
        widths[9] = 178.25f;
        widths[10] = 0f;
        Debug.Log(widths[3]);
    }

    // Update is called once per frame
    void Update()
    {

        if (10 - ((int)SaveVariables.flashtime / 10) >= 0)
        {
            if(SaveVariables.flashtime > 1)
            {
                num = 9 - ((int)SaveVariables.flashtime / 10);
            }
            else
            {
                num = 10;
            }
            
        }
            if (num >= 0 && num < 11)
        {
            Debug.Log(widths[num]);
            FillingMask.GetComponent<RectTransform>().sizeDelta = new Vector2(widths[num], 600);
        }
        if(state_ani == false) StartCoroutine(Ani());
        gameObject.transform.Find("BatteryCount").gameObject.GetComponent<TextMeshProUGUI>().text = "x" + SaveVariables.NumBatteries.ToString();


    }
    IEnumerator Ani()
    {
        if(num < 6)
        {
            state_ani = true;
            gameObject.transform.Find("Outline").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 1, 1, 1f);
            gameObject.transform.Find("Mask").Find("Filling").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 1, 1, 1f);
            yield return new WaitForSeconds((float)(2f - (1.5f * (((float)num / 10f)))));
            gameObject.transform.Find("Outline").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 1, 1, 0.2f);
            gameObject.transform.Find("Mask").Find("Filling").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 1, 1, 0.2f);
            yield return new WaitForSeconds((float)(1f - (0.5f * (((float)num / 10f)))));
            state_ani = false;
        }
        else
        {
            state_ani = true;
            gameObject.transform.Find("Outline").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 0, 0, 1f);
            gameObject.transform.Find("Mask").Find("Filling").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 0, 0, 1f);
            yield return new WaitForSeconds((float)(2f - (1.5f * (((float)num / 10f)))));
            gameObject.transform.Find("Outline").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 0, 0, 0.2f);
            gameObject.transform.Find("Mask").Find("Filling").gameObject.GetComponent<Image>().color = new UnityEngine.Color(1, 0, 0, 0.2f);
            yield return new WaitForSeconds((float)(1f - (0.5f * (((float)num / 10f)))));
            state_ani = false;
        }
        
    }
    
}
