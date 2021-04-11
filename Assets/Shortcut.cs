using System;
using System.Collections;
using System.Collections.Generic;
using Squeak;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shortcut : MonoBehaviour
{
    public Action shortcut1;
    public Action shortcut2;

    private Input input;

    private void Awake()
    {
        input = new Input();

        input.Player.Shortcut1.performed += PerformS1;
        input.Player.Shortcut2.performed += PerformS2;
    }

    private void PerformS1(InputAction.CallbackContext ctx)
    {
        if (ActionBar.instance.numActions < shortcut1.actionBarsNeeded)
            return;

        ActionBar.instance.SpendAction(shortcut1.actionBarsNeeded);
        ActionCaster.instance.CastAction(shortcut1);
        FindObjectOfType<PlayerController>().Cast(shortcut1);
    }
    
    private void PerformS2(InputAction.CallbackContext ctx)
    {
        if (ActionBar.instance.numActions < shortcut2.actionBarsNeeded)
            return;

        ActionBar.instance.SpendAction(shortcut2.actionBarsNeeded);
        ActionCaster.instance.CastAction(shortcut2);
        FindObjectOfType<PlayerController>().Cast(shortcut2);
    }
}