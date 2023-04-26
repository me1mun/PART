using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private GameObject infiniteIcon, premiumIcon;
    [SerializeField] private TextTransition subtitle;
    [SerializeField] private LocalizedString subtitle_complete;

    public void SetupDisplay(GameModeInfo gmInfo, int levelIndex, Level level)
    {
        subtitle.HideText();
        title.gameObject.SetActive(gmInfo.displayTitle);
        infiniteIcon.SetActive(gmInfo.isInfinite);
        premiumIcon.SetActive(gmInfo.isPremium);

        if (gmInfo.displayLevelName)
            title.text = level.levelName;
        else
            title.text = "#" + (levelIndex + 1);
    }

    public void SetSubtitleVictory()
    {
        subtitle.StartTextTransition(subtitle_complete, -1f);
    }
}
