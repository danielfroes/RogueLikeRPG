using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ActionMenu : MonoBehaviour
{
    [SerializeField] private GameObject actionMenu;
    private EventSystem es;
    private void Awake() {
       es = GameObject.FindObjectOfType<EventSystem>();
    }
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
        {
            Time.timeScale = 0f;
            es.SetSelectedGameObject(null);
            es.SetSelectedGameObject(es.firstSelectedGameObject);
        }
        if(!actionMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
    }
}
