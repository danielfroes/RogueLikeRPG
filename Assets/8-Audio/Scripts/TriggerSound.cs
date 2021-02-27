using System;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public Sound sound;

    private void OnEnable()
    {
        AudioManager.Play(sound);
    }
}
