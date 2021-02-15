using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShooter : MonoBehaviour
{   
    [Header("Particle configuration")]
    public ParticleSystem particle;
    public bool isChild;

    [Header("Pooling")]
    [SerializeField]
    [Min(1)]
    int maxParticles = 1;
    Queue<ParticleSystem> pool;

    private Quaternion rotation = Quaternion.identity;
    private Vector3 position = Vector3.zero;

    void Awake()
    {
        pool = new Queue<ParticleSystem>();

        for (int i = 0; i < maxParticles; i++)
        {
            ParticleSystem tmp = Instantiate<ParticleSystem>(particle, isChild ? transform : null);
            tmp.gameObject.SetActive(false);
            pool.Enqueue(tmp);
        }
    }

    void Update()
    {
        position = transform.position;
    }

    public void LookAt(Vector2 direction)
    {
        this.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, direction));
    }

    public void Play()
    {
        ParticleSystem p = pool.Dequeue();
        p.gameObject.SetActive(true);

        if (isChild)
            p.transform.position = position;
        else
            p.transform.position = transform.position;

        p.transform.rotation = rotation;
        p.Play();

        StartCoroutine(Deactivate(p, particle.main.startLifetime.constant));
    }

    private IEnumerator Deactivate(ParticleSystem p, float time)
    {
        yield return new WaitForSeconds(time);
        p.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        p.gameObject.SetActive(false);
        pool.Enqueue(p);
    }
    
}
