using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Localization;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject infiniteIcon, premiumIcon;
    [SerializeField] private TextTransition subtitle;
    [SerializeField] private LocalizedString subtitle_complete;

    public void SetupDisplay(int levelIndex, Level level)
    {
        subtitle.HideText();
        infiniteIcon.SetActive(level.isRandom);

        title.gameObject.SetActive(!level.isRandom);
        title.text = "#" + (levelIndex + 1);
    }

    public void SetSubtitleVictory()
    {
        subtitle.StartTextTransition(subtitle_complete, -1f);
    }
}
