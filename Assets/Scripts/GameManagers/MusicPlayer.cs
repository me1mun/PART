using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> tracks;
    private AudioSource audioSource;
    private int currentIndex = -1;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        if (tracks.Count == 0)
        {
            Debug.LogError("MusicPlayer: No tracks found in inspector.");
            return;
        }

        currentIndex = Random.Range(0, tracks.Count);

        audioSource.clip = tracks[currentIndex];
        audioSource.Play();
    }
}