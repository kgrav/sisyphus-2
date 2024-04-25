using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    static InputManager _inputManager;
    public static InputManager inputManager { get { if (!_inputManager) _inputManager = FindObjectOfType<InputManager>(); return _inputManager; } }


    public bool uiMode;
    //true for menu, false for oracle
    public InputActionReference RB, DPR;
    public bool LBHold = false;
    public void AButton(InputAction.CallbackContext ctx)
    {
        
        if (ctx.performed)
        {
            if (uiMode && FocusableUIComponent.focus)
            {
                FocusableUIComponent.focus.AButton();
            }
            else if(!uiMode){
                SisyphusController.sisy.AButton();
            }
        }
    }
    public void BButton(InputAction.CallbackContext ctx)
    {
        
        if (ctx.performed)
        {
            if (uiMode && FocusableUIComponent.focus)
            {
                FocusableUIComponent.focus.BButton();
            }
        }
    }

    public void LBButtonPress(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            LBHold = true;
        }
    }
    public void LBButtonRelease(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            LBHold = false;
        }
    }
    public void RBButton(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!uiMode)
            {
                SisyphusController.sisy.RBButton();
            }
        }
    }
    public void DPadR(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!uiMode)
            {
                SisyphusController.sisy.DPadR();
            }
            else if (FocusableUIComponent.focus)
            {
                FocusableUIComponent.focus.DPR();
            }
        }
    }
    public void DPadD(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (uiMode && FocusableUIComponent.focus)
            {
                FocusableUIComponent.focus.DPD();
            }
        }
    }
    public void DPadL(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (uiMode && FocusableUIComponent.focus)
            {
                FocusableUIComponent.focus.DPL();
            }
        }
    }
    public void DPadU(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (uiMode && FocusableUIComponent.focus)
            {
                FocusableUIComponent.focus.DPU();
            }
        }
    }


}