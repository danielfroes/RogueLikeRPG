using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class AudioManager
{
    private const int PoolSize = 32;
    private static readonly Queue<AudioSource> Pool;
    private static readonly Dictionary<int, AudioSource> BG;

    static AudioManager()
    {
        BG = new Dictionary<int, AudioSource>();
        Pool = new Queue<AudioSource>();
        
        Transform parent = new GameObject("AudioSources").transform;

        for (int i = 0; i < PoolSize; i++)
        {
            GameObject o = new GameObject($"AudioSource {i + 1}");
            Object.DontDestroyOnLoad(o);
            
            o.transform.parent = parent;
            o.SetActive(false);

            AudioSource src = o.AddComponent<AudioSource>();
            Pool.Enqueue(src);
        }
    }
    
    public static void Play(Sound sound)
    {
        AudioSource src = Pool.Dequeue();

        src.clip = sound.clip;
        src.outputAudioMixerGroup = sound.@group;
        src.loop = sound.loop;
        src.volume = sound.volume;

        src.gameObject.SetActive(true);
        src.Play();
        
        if (!sound.loop)
            Pool.Enqueue(src);
        else
            BG.Add(sound.GetInstanceID(), src);
    }

    public static void Stop(Sound sound)
    {
        if (BG.ContainsKey(sound.GetInstanceID()))
        {
            AudioSource src = BG[sound.GetInstanceID()];
            src.Stop();
            Pool.Enqueue(src);
            BG.Remove(sound.GetInstanceID());
        }
    }
    
}
