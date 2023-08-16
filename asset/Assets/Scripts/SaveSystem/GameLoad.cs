using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameLoad : MonoBehaviour
{
    public GameObject player;
    public GameObject[] light_triggers;
    public GameObject[] notes;
    void Awake(){
        LoadGame();
        player.GetComponent<PlayerScript>().SetupInventory();
        DeleteIndexedObjects();
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                if(j == i)
                {
                    notes[i].GetComponent<notescript>().txt += SaveVariables.safe_code.Substring(i,1);
                }
                else
                {
                    notes[i].GetComponent<notescript>().txt += "_";
                }
                
            }
            
        }
    }
    public void DeleteIndexedObjects(){
        //poop
        //for(int i = 0, i < SaveVariables.ItemIndexRemoved.count())
    }
    public void LoadGame(){
        if(!JsonUtility.FromJson<SaveClass>(File.ReadAllText(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() +".txt")).NotFirstTime){
            player.transform.position = new Vector3(527.315f, 18.5f, 540f);

            for (int i = 0; i < 4; i++)
            {
                int num = Random.Range(0, 10);
                SaveVariables.safe_code += num.ToString();
            }
            Debug.Log(SaveVariables.safe_code);
        }
        else{
            SaveClass save = new SaveClass();
            save = JsonUtility.FromJson<SaveClass>(File.ReadAllText(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() +".txt"));
            SaveVariables.FlashAvailable = save.FlashAvailable;
            SaveVariables.NumBatteries = save.FlashBatteries;
            SaveVariables.flashtime = save.flashtime;
            SaveVariables.GunAvailable = save.GunAvailable;
            SaveVariables.Key1Available = save.Key1Available;
            SaveVariables.Key2Available = save.Key2Available;
            SaveVariables.Key3Available = save.Key3Available;
            player.transform.position = save.PlayerPosition;
            SaveVariables.light_trigger_activated = save.light_trigger_activated;
        }
    }
    public SaveClass CreateSave()
    {
        SaveClass save = new SaveClass();
        save.NotFirstTime = true;
        save.FlashAvailable = SaveVariables.FlashAvailable;
        save.FlashBatteries = SaveVariables.NumBatteries;
        save.flashtime = SaveVariables.flashtime;
        save.GunAvailable = SaveVariables.GunAvailable;
        save.Key1Available = SaveVariables.Key1Available;
        save.Key2Available = SaveVariables.Key2Available;
        save.Key3Available = SaveVariables.Key3Available;
        save.PlayerPosition = player.transform.position;
        save.light_trigger_activated = SaveVariables.light_trigger_activated;
        save.safe_code = SaveVariables.safe_code;
        Load_Light_Triggers();
        return save;
    }
    public void SaveGame(){
        SaveClass save = CreateSave();
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() + ".txt", json);
    }
    public void Load_Light_Triggers()
    {
        for(int i = 0; i < SaveVariables.light_trigger_activated; i++)
        {
            light_triggers[i].GetComponent<LightTrigger>().triggerDone = true;
            light_triggers[i].GetComponent<LightTrigger>().triggered = true;
        }
    }

}
