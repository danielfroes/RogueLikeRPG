using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTag : MonoBehaviour
{

    new public string tag;

    private void Awake() {
        if(tag != null)
        {   
            // if(tag[0] != '#')
            //     tag = '#' + tag;
            // print("tag: " + tag);
        }
        else{
            Debug.LogWarning("tag in" + gameObject.name + "attack not inserted");
        }
    }
}
