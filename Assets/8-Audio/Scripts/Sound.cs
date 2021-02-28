using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Sound")]
public class Sound : ScriptableObject
{
    public AudioClip clip;
    public AudioMixerGroup group;
    public bool loop;
    [Range(0f, 1f)] public float volume = 1f;
}
