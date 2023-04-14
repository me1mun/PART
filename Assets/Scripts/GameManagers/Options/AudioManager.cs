using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioMixer audioMixer;

    private int defaultVolume = 2;
    public enum AudioTypes { sound, music };
    private float[] volumeGrades = new float[4] { -80, -15, -5, 0 };
    public Dictionary<AudioTypes, AudioData> audios = new Dictionary<AudioTypes, AudioData>();

    private void Awake()
    {
        audios.Add(AudioTypes.sound, new AudioData() { volume = defaultVolume, saveKey = "option_sound", mixerKey = "volume_sound" });
        audios.Add(AudioTypes.music, new AudioData() { volume = defaultVolume, saveKey = "option_music", mixerKey = "volume_music" });
    }

    private void Start()
    {
        LoadVolumes();
    }

    private void LoadVolumes()
    {
        SetVolume(AudioTypes.sound, PlayerPrefs.GetInt(audios[AudioTypes.sound].saveKey, defaultVolume));
        SetVolume(AudioTypes.music, PlayerPrefs.GetInt(audios[AudioTypes.music].saveKey, defaultVolume));
    }



    public void SetVolume(AudioTypes audioType, int newVolume)
    {
        audios[audioType].volume = newVolume % volumeGrades.Length;

        audioMixer.SetFloat(audios[audioType].mixerKey, volumeGrades[audios[audioType].volume]);
        //Debug.Log(audioType.ToString() + ": " + audioMixer.("Volume_sound")) ;

        PlayerPrefs.SetInt(audios[audioType].saveKey, GetVolume(audioType));
    }

    public int GetVolume(AudioTypes audioType)
    {
        return audios[audioType].volume;
    }
}

public class AudioData
{
    public int volume = 2;
    public string saveKey;
    public string mixerKey;
}