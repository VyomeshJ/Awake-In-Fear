using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ManageUI : MonoBehaviour
{
    public EventSystem eventSystem;
    public bool SaveScene;
    public GameObject[] SaveSlots;
    public Color RedSelectedColor;
    public GameObject transition_obj;
    public Sprite SelectedSlot;

    void Awake(){
        if(SaveScene){
            if(File.Exists(Application.dataPath + "/save1.txt")){
                SaveSlots[0].GetComponent<Image>().sprite = SelectedSlot;
                SaveSlots[0].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                SaveSlots[0].transform.Find("EmptySlot").gameObject.SetActive(false);
                SaveSlots[0].transform.Find("Slot").gameObject.SetActive(true);
                SaveSlots[0].transform.Find("DeleteButton").gameObject.SetActive(true);
            }
            if(File.Exists(Application.dataPath + "/save2.txt")){
                SaveSlots[1].GetComponent<Image>().sprite = SelectedSlot;
                SaveSlots[1].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                SaveSlots[1].transform.Find("EmptySlot").gameObject.SetActive(false);
                SaveSlots[1].transform.Find("Slot").gameObject.SetActive(true);
                SaveSlots[1].transform.Find("DeleteButton").gameObject.SetActive(true);
            }
            if(File.Exists(Application.dataPath + "/save3.txt")){
                SaveSlots[2].GetComponent<Image>().sprite = SelectedSlot;
                SaveSlots[2].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
                SaveSlots[2].transform.Find("EmptySlot").gameObject.SetActive(false);
                SaveSlots[2].transform.Find("Slot").gameObject.SetActive(true);
                SaveSlots[2].transform.Find("DeleteButton").gameObject.SetActive(true);
            }

        }
    }
    public void HoveringOverObject(GameObject obj){

        if(obj.transform.Find("EmptySlot").gameObject.activeInHierarchy)
        {
            obj.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            obj.transform.Find("EmptySlot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            obj.transform.Find("EmptySlot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
        }
        else
        {
            //obj.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            obj.transform.Find("Slot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
            obj.transform.Find("Slot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1f);
        }
            

        //eventSystem.SetSelectedGameObject(obj);
    }
    public void HoveringOverObjectExit(){
        if (SaveSlots[0].transform.Find("EmptySlot").gameObject.activeInHierarchy)
        {
            SaveSlots[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            //eventSystem.SetSelectedGameObject(null);
            SaveSlots[0].transform.Find("EmptySlot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[0].transform.Find("EmptySlot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            //SaveSlots[0].GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
            SaveSlots[0].transform.Find("Slot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[0].transform.Find("Slot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
        if (SaveSlots[1].transform.Find("EmptySlot").gameObject.activeInHierarchy)
        {
            SaveSlots[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[1].transform.Find("EmptySlot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[1].transform.Find("EmptySlot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            //SaveSlots[1].GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
            SaveSlots[1].transform.Find("Slot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[1].transform.Find("Slot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
        if (SaveSlots[2].transform.Find("EmptySlot").gameObject.activeInHierarchy)
        {
            SaveSlots[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[2].transform.Find("EmptySlot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[2].transform.Find("EmptySlot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
           // SaveSlots[2].GetComponent<Image>().color = new Color(1, 1, 1, 0.8f);
            SaveSlots[2].transform.Find("Slot").Find("EmptySlotText").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
            SaveSlots[2].transform.Find("Slot").Find("EmptySlotPrompt").gameObject.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.2f);
        }
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

    public void DeleteSave(int num)
    {
        File.Delete(Application.dataPath + "/save" + num.ToString() + ".txt");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PlayerPrefs.SetInt(num.ToString() + "_ElectricityDoor", 0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
