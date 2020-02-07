using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRandomizer : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] soundBank;
    int lastClipIndex;

    public void PlayRandomSound()
    {
        int nextClipIndex = Random.Range(0, soundBank.Length);
        if (nextClipIndex == lastClipIndex)
        {
            nextClipIndex++;
            if (nextClipIndex == soundBank.Length)
                nextClipIndex = 0;
        }
        source.PlayOneShot(soundBank[nextClipIndex]);
        lastClipIndex = nextClipIndex;
    }
}
