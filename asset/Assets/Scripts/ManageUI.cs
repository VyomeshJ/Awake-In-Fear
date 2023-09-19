using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class ManageUI : MonoBehaviour
{
    public EventSystem eventSystem;
    public bool SaveScene;
    public GameObject[] SaveSlots;
    public Color RedSelectedColor;
    public GameObject transition_obj;

    void Awake(){
        if(SaveScene){
            if(File.Exists(Application.dataPath + "/save1.txt")){
                SaveSlots[0].GetComponent<RawImage>().color = RedSelectedColor;
                SaveSlots[0].transform.Find("EmptySlot").gameObject.SetActive(false);
                SaveSlots[0].transform.Find("Slot").gameObject.SetActive(true);
            }
            if(File.Exists(Application.dataPath + "/save2.txt")){
                SaveSlots[1].GetComponent<RawImage>().color = RedSelectedColor;
                SaveSlots[1].transform.Find("EmptySlot").gameObject.SetActive(false);
                SaveSlots[1].transform.Find("Slot").gameObject.SetActive(true);
            }
            if(File.Exists(Application.dataPath + "/save3.txt")){
                SaveSlots[2].GetComponent<RawImage>().color = RedSelectedColor;
                SaveSlots[2].transform.Find("EmptySlot").gameObject.SetActive(false);
                SaveSlots[2].transform.Find("Slot").gameObject.SetActive(true);
            }

        }
    }
    public void HoveringOverObject(GameObject obj){
        //eventSystem.SetSelectedGameObject(obj);
    }
    public void HoveringOverObjectExit(){
        //eventSystem.SetSelectedGameObject(null);
    }
    public void CreateSave(int saveNo){
        if(!File.Exists(Application.dataPath + "/save" + saveNo.ToString() + ".txt")){
            SaveClass save = new SaveClass();
            string json = JsonUtility.ToJson(save);
            File.WriteAllText(Application.dataPath + "/save" + saveNo.ToString() + ".txt", json);
        }
        PlayerPrefs.SetInt("SaveNum", saveNo);
        SceneChange("TutorialScene");
    }
    public void SceneChange(string scene_name){
        Debug.Log("cock");
        StartCoroutine(transition(scene_name));
    }
    IEnumerator transition(string scene_name)
    {
        transition_obj.SetActive(true);
        transition_obj.GetComponent<Animator>().Play("close");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene_name);
    }

}
