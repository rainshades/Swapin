using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] Clips;

    public AudioSource player;

    public void SetSong(int index)
    {
        player.clip = Clips[index];
        player.Play(); 
    }

    public void PlayClip(AudioSource source, int index)
    {
        source.clip = Clips[index];
        source.Play(); 
    }
}
