using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;

    private float[] volumeGrades = new float[4] { -80, -15, -5, 0 };
    public int volume_sounds = 2;
    public int volume_music = 3;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void ChangeVolumeSounds()
    {
        volume_sounds = ClampVolume(volume_sounds + 1, volumeGrades.Length);

        ApplyVolume();
        //Debug.Log("Effects: " + volume_sounds);
    }

    public void ChangeVolumeMusic()
    {
        volume_music = ClampVolume(volume_music + 1, volumeGrades.Length);

        ApplyVolume();
        //Debug.Log("Music: " + volume_music);
    }

    private int ClampVolume(int volumeIndex, int volumeCount)
    {
        if (volumeIndex < 0)
        {
            return (volumeCount - 1);
        }
        else if (volumeIndex >= volumeCount)
        {
            return(0);
        }

        return (volumeIndex);
    }

    private void ApplyVolume()
    {
        audioMixer.SetFloat("Volume_music", volumeGrades[volume_music]);
        audioMixer.SetFloat("Volume_sounds", volumeGrades[volume_sounds]);
    }
}
