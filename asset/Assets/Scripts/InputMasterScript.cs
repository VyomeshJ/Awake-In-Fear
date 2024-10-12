using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMasterScript : MonoBehaviour
{
    public enum InputMethod{
        Controller,
        Keyboard
    }
    public InputMethod CurrentInputMethod;
    public InputMaster inputMaster;
    public GameObject Player;
    public GameObject Camera;

    private void Awake() {
        inputMaster = new InputMaster();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnEnable() {
        inputMaster.Player.Movement.performed += Movement;
        inputMaster.Player.Movement.canceled += MovementCanceled;
        //inputMaster.Player.Look.performed += Look;
        //inputMaster.Player.Look.canceled += LookCanceled;
        inputMaster.Player.SpaceSouth.performed += Jump;
        inputMaster.Player.EWest.performed += E_Pressed;
        inputMaster.Player.EWest.canceled += E_Cancelled;
        inputMaster.Player.GNorth.performed += G_Pressed;
        inputMaster.Player.GNorth.canceled += G_Cancelled;
        inputMaster.Player.LClick.performed += LClick_Pressed;
        inputMaster.Player.RClick.performed += RClick_Pressed;

        inputMaster.Player.FLT.performed += F_Pressed;
        //inputMaster.Player.FLT.canceled += F_Cancelled;
        inputMaster.Player.Escape.performed += Esc_Pressed;
        //inputMaster.Player.Num1.performed += Num1_Pressed;
        inputMaster.Player.LeftShift.performed += LShift_Pressed;
        inputMaster.Player.LeftShift.canceled += LShift_Cancelled;
        inputMaster.Player.Inventory.performed += Inventory_Pressed;
        inputMaster.Player.P.performed += P_Pressed;
        inputMaster.Player.P.canceled += P_Canceled;
        inputMaster.Player.Enable();
    }
    private void OnDisable() {
        inputMaster.Player.Disable();
    }

    public void P_Pressed(InputAction.CallbackContext ctx)
    {
        Camera.GetComponent<objectPick>().p_pressed = true;
    }
    public void P_Canceled(InputAction.CallbackContext ctx)
    {
        Camera.GetComponent<objectPick>().p_pressed = false;
    }
    public void LClick_Pressed(InputAction.CallbackContext ctx)
    {
        Player.GetComponent<PlayerScript>().LClick();
    }
    public void RClick_Pressed(InputAction.CallbackContext ctx)
    {
        Player.GetComponent<PlayerScript>().MouseButtonZoom();
    }

    public void Movement(InputAction.CallbackContext ctx){
        //if(!SaveVariables.InventoryOpen){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;

        Player.GetComponent<CharController_Motor>().moveFB = ctx.ReadValue<Vector2>().x;
        Player.GetComponent<CharController_Motor>().moveLR = ctx.ReadValue<Vector2>().y;
        

        //}
    }
    public void MovementCanceled(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        Player.GetComponent<CharController_Motor>().moveFB = 0;
        Player.GetComponent<CharController_Motor>().moveLR = 0;
    }
    public void Look(InputAction.CallbackContext ctx){
        //if(!SaveVariables.InventoryOpen){
            if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse"){
                
                CurrentInputMethod = InputMethod.Keyboard;
                Player.GetComponent<CharController_Motor>().rotX = ctx.ReadValue<Vector2>().x * Player.GetComponent<CharController_Motor>().MouseSensitivity;
            
                if (!SaveVariables.PlayerHiding_Bed)
                {
                    Player.GetComponent<CharController_Motor>().rotY = ctx.ReadValue<Vector2>().y * Player.GetComponent<CharController_Motor>().MouseSensitivity;
                }
                else
                {
                    Player.GetComponent<CharController_Motor>().rotY = 0;
                }

            }
            if(ctx.action.activeControl.device.name == "XInputControllerWindows"){
                CurrentInputMethod = InputMethod.Controller;
                Player.GetComponent<CharController_Motor>().rotX = ctx.ReadValue<Vector2>().x * Player.GetComponent<CharController_Motor>().ControllerSensitivity;
            //Player.GetComponent<CharController_Motor>().rotY = ctx.ReadValue<Vector2>().y * Player.GetComponent<CharController_Motor>().ControllerSensitivity;
                if (!SaveVariables.PlayerHiding_Bed)
                {
                    Player.GetComponent<CharController_Motor>().rotY = ctx.ReadValue<Vector2>().y * Player.GetComponent<CharController_Motor>().ControllerSensitivity;
                }
                else
                {
                    Player.GetComponent<CharController_Motor>().rotY = 0;
                }
            }
        //}
        
    }
    public void LookCanceled(InputAction.CallbackContext ctx){
        CurrentInputMethod = InputMethod.Controller;
        Player.GetComponent<CharController_Motor>().rotX = 0;
        Player.GetComponent<CharController_Motor>().rotY = 0;
    }
    public void Jump(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        //if(!SaveVariables.InventoryOpen){
            Player.GetComponent<CharController_Motor>().Jump();
        //}
    }
    public void E_Pressed(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        if(!SaveVariables.InventoryOpen){
            Player.GetComponent<PlayerScript>().PickUpObject();
        }
    }
    public void E_Cancelled(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        //Player.GetComponent<PlayerScript>().E_Pressed = false;
    }
    private void G_Pressed(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        if(!SaveVariables.InventoryOpen){
            Player.GetComponent<PlayerScript>().InteractWithObject();
        }
        //Player.GetComponent<PlayerScript>().G_Pressed = true;
    }
    private void G_Cancelled(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        //Player.GetComponent<PlayerScript>().G_Pressed = false;
    }
    private void F_Pressed(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        
        Player.GetComponent<PlayerScript>().Flashlight();
    }
    private void F_Cancelled(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        //Player.GetComponent<PlayerScript>().F_Pressed = false;
    }
    
    private void Esc_Pressed(InputAction.CallbackContext ctx){
        if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
        if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
        if (SaveVariables.InventoryOpen) Player.GetComponent<PlayerScript>().InventoryMenu();
        else if (SaveVariables.PlayerHiding_Bed || SaveVariables.PlayerHiding_Closet) Player.GetComponent<PlayerScript>().StopHiding();
  
        else if (GameObject.FindGameObjectWithTag("PauseMenuObj").GetComponent<PauseMenu>().PromptOpen){
            GameObject.FindGameObjectWithTag("PauseMenuObj").GetComponent<PauseMenu>().AskPrompt.SetActive(false);
            GameObject.FindGameObjectWithTag("PauseMenuObj").transform.Find("PauseUI").gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("PauseMenuObj").GetComponent<PauseMenu>().PromptOpen = false;
        }
        else if (GameObject.FindGameObjectWithTag("PauseMenuObj").transform.Find("PauseUI").gameObject.activeSelf) GameObject.FindGameObjectWithTag("PauseMenuObj").GetComponent<PauseMenu>().resumeGame();
        else if (!Player.GetComponent<PlayerScript>().holdingnote) GameObject.FindGameObjectWithTag("PauseMenuObj").GetComponent<PauseMenu>().pauseGame();
        
        else if (Player.GetComponent<PlayerScript>().holdingnote) Player.GetComponent<PlayerScript>().Exit_Holdingnote();
    }
    /*
    private void Num1_Pressed(InputAction.CallbackContext ctx){
        
            if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
            if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
            Player.GetComponent<PlayerScript>().Num1();
    }
    */
    private void LShift_Pressed(InputAction.CallbackContext ctx){
        if(!SaveVariables.InventoryOpen){
            if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
            if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
            Player.GetComponent<PlayerScript>().LShift_Enabled();
        }
    }
    private void LShift_Cancelled(InputAction.CallbackContext ctx){
        if(!SaveVariables.InventoryOpen){
            if(ctx.action.activeControl.device.name == "Keyboard" || ctx.action.activeControl.device.name == "Mouse") CurrentInputMethod = InputMethod.Keyboard;
            if(ctx.action.activeControl.device.name == "XInputControllerWindows") CurrentInputMethod = InputMethod.Controller;
            Player.GetComponent<PlayerScript>().LShift_Disabled();
        }
    }
    private void Inventory_Pressed(InputAction.CallbackContext ctx){
        Player.GetComponent<PlayerScript>().InventoryMenu();
    }
    
}
