using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [Header ("Sound")]
    [SerializeField] private AudioMixer audioMixer;

    private float[] volumeGrades = new float[4] { -80, -15, -5, 0 };
    public static int volume_effects = 2;
    public static int volume_music = 3;

    private void Start()
    {
        VolumeInit();
    }

    public void ChangeVolumeEffects()
    {
        volume_effects = ClampVolume(volume_effects + 1, volumeGrades.Length);

        VolumeInit();
    }

    public void ChangeVolumeMusic()
    {
        volume_music = ClampVolume(volume_music + 1, volumeGrades.Length);

        VolumeInit();
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

    public void VolumeInit()
    {
        audioMixer.SetFloat("Volume_music", volumeGrades[volume_music]);
        audioMixer.SetFloat("Volume_effects", volumeGrades[volume_effects]);
    }
}
