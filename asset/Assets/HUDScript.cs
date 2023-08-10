using System.Collections;
using System.Collections.Generic;
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

    private void Update()
    {
        if (SaveVariables.Key1Available) InvObjects[2].GetComponent<Image>().sprite = SelectedInv[2];
        else InvObjects[2].GetComponent<Image>().sprite = UnselectedInv[2];

        if (SaveVariables.Key1Available) InvObjects[3].GetComponent<Image>().sprite = SelectedInv[3];
        else InvObjects[2].GetComponent<Image>().sprite = UnselectedInv[3];

        if (SaveVariables.Key1Available) InvObjects[4].GetComponent<Image>().sprite = SelectedInv[4];
        else InvObjects[2].GetComponent<Image>().sprite = UnselectedInv[4];

    }
    public void FlashLightSelected(){
        if(SaveVariables.FlashAvailable){

            InvObjects[0].GetComponent<Image>().sprite = SelectedInv[0];
            
            SaveVariables.InventoryOpen = false;
            if(player.GetComponent<PlayerScript>().currentSelectedItem != "flashlight"){
                player.GetComponent<PlayerScript>().currentSelectedItem = "flashlight";
                InvObjects[0].GetComponent<Image>().sprite = SelectedInv[0];
                player.GetComponent<PlayerScript>().holdingweapon = false;
                gunObj.SetActive(false);
                player.GetComponent<PlayerScript>().OpenFlashlight();
            }
            else{
                InvObjects[0].GetComponent<Image>().sprite = UnselectedInv[0];
                player.GetComponent<PlayerScript>().flashLight.SetActive(false);
                player.GetComponent<PlayerScript>().FlashlightHUD.SetActive(false);
                player.GetComponent<PlayerScript>().currentSelectedItem = "null";
            }
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
       
    }
    public void CaffeineSelected(){
        
    }
    public void GunSelected(){
        Debug.Log("gun shit");
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
