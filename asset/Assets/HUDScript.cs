using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public GameObject player;
    public GameObject gunObj;
    public GameObject CaffeinePrompt;

    public Sprite[] SelectedInv;
    public Sprite[] UnselectedInv;
    public GameObject[] InvObjects;
    public string[] ItemDescriotions;

    public TextMeshProUGUI desc_txt;
    public bool flash_open;
    public bool wrench_open;
    public GameObject caffeine_prompt_prefab;
    public TextMeshProUGUI caffeine_pill_counter;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (SaveVariables.CaffeinePillsAvailable > 0)
        {
            caffeine_pill_counter.text = SaveVariables.CaffeinePillsAvailable.ToString();
            InvObjects[1].GetComponent<Image>().sprite = SelectedInv[1];
        }
        else
        {
            caffeine_pill_counter.text = "";
            InvObjects[1].GetComponent<Image>().sprite = UnselectedInv[1];
        }
        if (SaveVariables.FlashAvailable) InvObjects[0].GetComponent<Image>().sprite = SelectedInv[0];
        else InvObjects[6].GetComponent<Image>().sprite = UnselectedInv[0];
        if (SaveVariables.WrenchAvailable) InvObjects[6].GetComponent<Image>().sprite = SelectedInv[6];
        else InvObjects[6].GetComponent<Image>().sprite = UnselectedInv[6];

        if (SaveVariables.Key1Available) InvObjects[2].GetComponent<Image>().sprite = SelectedInv[2];
        else InvObjects[2].GetComponent<Image>().sprite = UnselectedInv[2];

        if (SaveVariables.Key2Available) InvObjects[3].GetComponent<Image>().sprite = SelectedInv[3];
        else InvObjects[2].GetComponent<Image>().sprite = UnselectedInv[3];

        if (SaveVariables.Key3Available) InvObjects[4].GetComponent<Image>().sprite = SelectedInv[4];
        else InvObjects[2].GetComponent<Image>().sprite = UnselectedInv[4];

    }
    public void test()
    {
        
    }
    public void change_item_description(int item_index)
    {
        desc_txt.text = ItemDescriotions[item_index];
        player.GetComponent<PlayerScript>().eventSystem.SetSelectedGameObject(InvObjects[item_index].transform.Find("Image").gameObject);
    }
    public void null_item_description()
    {
        //desc_txt.text = "";
    }
    public void FlashLightSelected(){
        if(SaveVariables.FlashAvailable){
                flash_open = true;
                InvObjects[0].GetComponent<Image>().sprite = SelectedInv[0];

                SaveVariables.InventoryOpen = false;
                if (player.GetComponent<PlayerScript>().currentSelectedItem != "flashlight")
                {
                    player.GetComponent<PlayerScript>().currentSelectedItem = "flashlight";
                    InvObjects[0].GetComponent<Image>().sprite = SelectedInv[0];
                    player.GetComponent<PlayerScript>().holdingweapon = false;
                    gunObj.SetActive(false);
                    player.GetComponent<PlayerScript>().OpenFlashlight();
                }
                else
                {
                    InvObjects[0].GetComponent<Image>().sprite = UnselectedInv[0];
                    player.GetComponent<PlayerScript>().flashLight.SetActive(false);
                    player.GetComponent<PlayerScript>().FlashlightHUD.SetActive(false);
                    player.GetComponent<PlayerScript>().currentSelectedItem = "null";
                    player.GetComponent<PlayerScript>().batteryChargeText.gameObject.SetActive(true);
                    player.GetComponent<PlayerScript>().batteryNumText.gameObject.SetActive(true);
            }
                gameObject.SetActive(false);
                Time.timeScale = 1;
            
            if(wrench_open)
            {
                wrench_open = false;
                InvObjects[6].GetComponent<Image>().sprite = UnselectedInv[6];
                player.GetComponent<PlayerScript>().wrenchObj.SetActive(false);
                player.GetComponent<PlayerScript>().currentSelectedItem = "null";
            }
            
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }
    public void WrenchSelected()
    {
        if (SaveVariables.WrenchAvailable)
        {
            wrench_open = true;
         
            //InvObjects[6].GetComponent<Image>().sprite = SelectedInv[6];

            SaveVariables.InventoryOpen = false;
            if (player.GetComponent<PlayerScript>().currentSelectedItem != "wrench")
            {
                player.GetComponent<PlayerScript>().currentSelectedItem = "wrench";
                //InvObjects[0].GetComponent<Image>().sprite = SelectedInv[0];
                player.GetComponent<PlayerScript>().holdingweapon = false;
                gunObj.SetActive(false);
                player.GetComponent<PlayerScript>().wrenchObj.SetActive(true);
            }
            else
            {
                InvObjects[6].GetComponent<Image>().sprite = UnselectedInv[6];
                player.GetComponent<PlayerScript>().wrenchObj.SetActive(false);
                player.GetComponent<PlayerScript>().currentSelectedItem = "null";
            }
            gameObject.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (flash_open)
            {
                flash_open = false;
                InvObjects[0].GetComponent<Image>().sprite = UnselectedInv[0];
                player.GetComponent<PlayerScript>().flashLight.SetActive(false);
                player.GetComponent<PlayerScript>().FlashlightHUD.SetActive(false);
                player.GetComponent<PlayerScript>().currentSelectedItem = "null";
            }
        }
    }
    public void CaffeineSelected(){
        if(SaveVariables.CaffeinePillsAvailable > 0)
        {
            if(SaveVariables.caffeine_lvl > 75)
            {
                GameObject prefab_inst = Instantiate(caffeine_prompt_prefab);
                prefab_inst.transform.parent = gameObject.transform;
                prefab_inst.transform.localPosition = new Vector3(0, -650f, 0);
                prefab_inst.transform.localScale = new Vector3(2, 2, 2);
            }
            else
            {
              
                SaveVariables.caffeine_lvl += 25;
                SaveVariables.CaffeinePillsAvailable -= 1;
            }
        }
    }
    public void GunSelected(){
       
        if(SaveVariables.GunAvailable){
            gameObject.SetActive(false);
            SaveVariables.InventoryOpen = false;
            if(player.GetComponent<PlayerScript>().currentSelectedItem != "gun"){
                player.GetComponent<PlayerScript>().flashLight.SetActive(false);
                player.GetComponent<PlayerScript>().FlashlightHUD.SetActive(false);
                player.GetComponent<PlayerScript>().currentSelectedItem = "gun";
                player.GetComponent<PlayerScript>().holdingweapon = true;
                gunObj.SetActive(true);
            }
            else{
                player.GetComponent<PlayerScript>().currentSelectedItem = "null";
                player.GetComponent<PlayerScript>().holdingweapon = false;
                player.GetComponent<PlayerScript>().gun_zoomed_in = false;
                player.GetComponent<PlayerScript>().cam.fieldOfView = 60;
                player.GetComponent<PlayerScript>().shotgunanimator.Play("zoom out");
                gunObj.GetComponent<Animator>().Play("leave shotgun");
                player.GetComponent<PlayerScript>()._disableShotgun();
            }
            Time.timeScale = 1;
        }
    }
}
