using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradesController : MonoBehaviour
{
    [SerializeField] private Image gradeImage;
    [SerializeField] private Sprite[] gradeIcons = new Sprite[4];
    private int grade = 0;

    private void Start()
    {
        //Init();
    }

    public void SetGrade(int newGrade)
    {
        grade = newGrade;
        SetGradeDisplay();
    }

    public void SetGradeDisplay()
    {
        int gradesCount = gradeIcons.Length;
        grade = Mathf.Clamp(grade, 0, gradesCount - 1);

        gradeImage.sprite = gradeIcons[grade];
    }
}
