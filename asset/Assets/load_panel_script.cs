using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_panel_script : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI load;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Ani_Ended()
    {
        load.color = Color.white;
        //SceneManager.LoadScene("Scene_A");
        StartCoroutine(LoadASync("Scene_A"));
    }
    IEnumerator LoadASync(string load_scene)
    {

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(load_scene);
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.RoundToInt(Mathf.Clamp01(loadOperation.progress / 0.9f) * 100);
            load.text = "Loading... " + progressValue.ToString() + "%";
            Debug.Log(progressValue.ToString());
            yield return null;

        }
    }
}
