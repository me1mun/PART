using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UI_sound : MonoBehaviour
{
    private enum Types { effects, music };
    [SerializeField] Types type;

    [SerializeField] private Options options;
    [SerializeField] private GradesController gradeController;

    private void OnEnable()
    {
        if(type == Types.effects)
        {
            gradeController.SetGrade(Options.volume_effects);
        }
        else if (type == Types.music)
        {
            gradeController.SetGrade(Options.volume_music);
        }
    }

    public void ChangeEffects()
    {
        options.ChangeVolumeEffects();
        SetGrade(Options.volume_effects);
    }

    public void ChangeMusic()
    {
        options.ChangeVolumeMusic();
        SetGrade(Options.volume_music);
    }

    private void SetGrade(int grade)
    {
        gradeController.SetGrade(grade);
    }
}
