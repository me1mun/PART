using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionButtonOrientation : MonoBehaviour
{
    [SerializeField] private Image orientationImage;

    private void OnEnable()
    {
        SetupDisplay();
    }

    public void ChangeOrientation()
    {
        GameManager.Instance.orientation.SetOrientation(GameManager.Instance.orientation.GetCurrentOrientation() + 1);

        SetupDisplay();
    }

    public void SetupDisplay()
    {
        orientationImage.sprite = GameManager.Instance.orientation.GetCurrentOrientationIcon();
    }
}
