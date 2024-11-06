using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Rendering;

public class GameLoad : MonoBehaviour
{
    public GameObject player;
    public GameObject[] light_triggers;
    public GameObject[] notes;
    void Awake(){
        LoadGame();
        player.GetComponent<PlayerScript>().SetupInventory();
        DeleteIndexedObjects();
        if (!JsonUtility.FromJson<SaveClass>(File.ReadAllText(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() + ".txt")).NotFirstTime)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == i)
                    {
                        notes[i].GetComponent<notescript>().txt += SaveVariables.safe_code.Substring(i, 1);
                    }
                    else
                    {
                        notes[i].GetComponent<notescript>().txt += "_";
                    }

                }

            }
        }
    }
    public void DeleteIndexedObjects(){
        //poop
        //for(int i = 0, i < SaveVariables.ItemIndexRemoved.count())
    }
    public void LoadGame(){
        SaveVariables.safe_code = "";
        
        if (!JsonUtility.FromJson<SaveClass>(File.ReadAllText(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() +".txt")).NotFirstTime){
            SaveVariables.door_unlocked = new bool[6];
            SaveVariables.FlashAvailable = false;
            SaveVariables.WrenchAvailable = false;
            SaveVariables.NumBatteries = 0;
            SaveVariables.GunAvailable = false;
            SaveVariables.Key1Available = false;
            SaveVariables.Key2Available = false;
            SaveVariables.Key3Available = false;
            SaveVariables.CaffeineLevel = 100;
            SaveVariables.light_trigger_activated = 0;
            SaveVariables.safe_code = null;
            SaveVariables.trigger_to_trigger = 0;
            //player.transform.position = new Vector3(527.315f, 18.5f, 540f);

            SaveVariables.Player_Initial_Pos = new Vector3(527.315f, 18.5f, 540f);
            player.GetComponent<PlayerScript>().initialize_player_pos();
            for (int i = 0; i < 4; i++)
            {
                int num = Random.Range(1, 9);
                SaveVariables.safe_code += num.ToString();
            }
            SaveVariables.trigger_to_trigger = 1;
            Debug.Log("THE CAFFEINE AMOUNT IS " + SaveVariables.CaffeineLevel.ToString());
        }
        else{
            SaveClass save = new SaveClass();
            save = JsonUtility.FromJson<SaveClass>(File.ReadAllText(Application.dataPath + "/save" + PlayerPrefs.GetInt("SaveNum").ToString() +".txt"));
            SaveVariables.FlashAvailable = save.FlashAvailable;
            SaveVariables.WrenchAvailable = save.WrenchAvailable;
            SaveVariables.NumBatteries = save.FlashBatteries;
            SaveVariables.flashtime = save.flashtime;
            SaveVariables.GunAvailable = save.GunAvailable;
            SaveVariables.Key1Available = save.Key1Available;
            SaveVariables.Key2Available = save.Key2Available;
            SaveVariables.Key3Available = save.Key3Available;
            SaveVariables.Player_Initial_Pos = save.PlayerPosition;
            SaveVariables.CaffeineLevel = save.CaffeineLevel;
            SaveVariables.door_unlocked = save.door_unlocked;
            
            //player.transform.position = save.PlayerPosition;
            
          
            SaveVariables.light_trigger_activated = save.light_trigger_activated;
            SaveVariables.safe_code = save.safe_code;

            SaveVariables.trigger_to_trigger = save.trigger_to_trigger;
        }
    }
    public SaveClass CreateSave()
    {
        SaveClass save = new SaveClass();
        save.NotFirstTime = true;
        save.FlashAvailable = SaveVariables.FlashAvailable;
        save.WrenchAvailable = SaveVariables.WrenchAvailable;
        save.FlashBatteries = SaveVariables.NumBatteries;
        save.flashtime = SaveVariables.flashtime;
        save.GunAvailable = SaveVariables.GunAvailable;
        save.Key1Available = SaveVariables.Key1Available;
        save.Key2Available = SaveVariables.Key2Available;
        save.Key3Available = SaveVariables.Key3Available;
        save.PlayerPosition = player.transform.position;
        save.light_trigger_activated = SaveVariables.light_trigger_activated;
        save.safe_code = SaveVariables.safe_code;
        save.trigger_to_trigger = SaveVariables.trigger_to_trigger;
        save.CaffeineLevel = SaveVariables.CaffeineLevel;
        save.door_unlocked = SaveVariables.door_unlocked;


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
