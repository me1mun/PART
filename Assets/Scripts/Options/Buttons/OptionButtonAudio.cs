using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionButtonAudio : MonoBehaviour
{

    [SerializeField] AudioManager.AudioTypes audioType;

    //[SerializeField] private Options options;
    [SerializeField] private UIGrades gradeController;
    [SerializeField] private AudioSource buttonSound;

    private void OnEnable()
    {
        SetupDisplay();
    }

    public void ChangeVolume()
    {
        GameManager.Instance.audioManager.SetVolume(audioType, GameManager.Instance.audioManager.GetVolume(audioType) + 1);
        buttonSound.Play();
        SetupDisplay();
    }

    private void SetupDisplay()
    {
        gradeController.SetGrade(GameManager.Instance.audioManager.GetVolume(audioType));
    }
}
