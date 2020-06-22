using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSystem : MonoBehaviour
{
    [SerializeField] private GameObject actionMenu;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {

            ToggleActionMenu();
            
        }
    }


    private void ToggleActionMenu()
    {

        actionMenu.SetActive(!actionMenu.activeInHierarchy);
        
        if(actionMenu.activeInHierarchy)
            Time.timeScale = 0f;
        if(!actionMenu.activeInHierarchy)
            Time.timeScale = 1f;
    }
}
