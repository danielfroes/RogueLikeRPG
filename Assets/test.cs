using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Sound music;
    
    void Start()
    {
        AudioManager.Play(music);
    }
}
