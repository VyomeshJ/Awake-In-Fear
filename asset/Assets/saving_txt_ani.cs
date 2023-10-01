using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class saving_txt_ani : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        StartCoroutine(save_ani());
    }
    IEnumerator save_ani()
    {
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j <= 3; j++)
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = "Saving...".Substring(0,6+j);
                yield return new WaitForSecondsRealtime(0.2f);
            }
        }
        gameObject.SetActive(false);
    }
}
