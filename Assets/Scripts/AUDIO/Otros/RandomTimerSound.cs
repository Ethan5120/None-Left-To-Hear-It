using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimerSound : MonoBehaviour
{
    public AudioClip soundEffect; 
    private AudioSource audioSource;
    public float minInterval = 2f; 
    public float maxInterval = 30f;

    private float timer;
    private float nextSoundTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = soundEffect;
        nextSoundTime = Random.Range(minInterval, maxInterval);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= nextSoundTime)
        {
            PlayRandomSound();
            timer = 0f;
            nextSoundTime = Random.Range(minInterval, maxInterval);
        }
    }

    private void PlayRandomSound()
    {       
        audioSource.Play();
    }
}