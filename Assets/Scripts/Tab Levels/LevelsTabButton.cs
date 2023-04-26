using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsTabButton : MonoBehaviour
{
    private bool isActive;
    [SerializeField] private LevelManager.GameModes listMode;

    private TabLevels tabLevels;
    private AnimationAlpha animationAlpha;
    private ButtonController buttonController;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        tabLevels = GetComponentInParent<TabLevels>();
        animationAlpha = GetComponent<AnimationAlpha>();
        buttonController = GetComponent<ButtonController>();

        buttonController.onClick.AddListener(Interact);
    }

    public void Interact()
    {
        tabLevels.SetListMode(listMode);
    }

    public void Activate(bool on)
    {
        isActive = on;

        //buttonController.SetInteractable(isActive);
        animationAlpha.StartAnimationAlpha(isActive ? 1 : 0.25f, 0.2f);
    }

    public LevelManager.GameModes GetListMode()
    {
        return listMode;
    }
}
