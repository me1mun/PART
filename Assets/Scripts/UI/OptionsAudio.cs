using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsAudio : MonoBehaviour
{

    private enum Types { sounds, music };
    [SerializeField] Types type;

    //[SerializeField] private Options options;
    [SerializeField] private GradesController gradeController;

    private void OnEnable()
    {
        if(type == Types.sounds)
        {
            gradeController.SetGrade(AudioManager.Instance.volume_sounds);
        }
        else if (type == Types.music)
        {
            gradeController.SetGrade(AudioManager.Instance.volume_music);
        }
    }

    public void ChangeSounds()
    {
        AudioManager.Instance.ChangeVolumeSounds();
        SetGrade(AudioManager.Instance.volume_sounds);
    }

    public void ChangeMusic()
    {
        AudioManager.Instance.ChangeVolumeMusic();
        SetGrade(AudioManager.Instance.volume_music);
    }

    private void SetGrade(int grade)
    {
        gradeController.SetGrade(grade);
    }
}
