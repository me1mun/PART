using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class FieldController : MonoBehaviour
{
    public UnityEvent OnLevelComplete;

    private Level currentLevel;
    [SerializeField] Transform fieldContainer;
    [SerializeField] GameObject casePrefab;

    private bool isInteractable = true;
    public PartController[,] field = new PartController[0, 0];
    private Vector2Int fieldMaxSize = new Vector2Int(8, 10);
    private Vector2Int fieldSize;
    private LevelDatabase.Colors levelColor;
    public bool isLooped = false;

    void Start()
    {
        //currentLevel = JsonUtility.FromJson<Level>(LevelList.Instance.levelJson[GameManager.level].text);

        //FieldCreate(currentLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            CheckLoopComplete();
    }

    public void CreateField(Level level)
    {
        ClearField();

        currentLevel = level;
        levelColor = LevelDatabase.Instance.GetColorEnum(level.colorName);

        fieldSize.x = level.width;
        fieldSize.y = level.height;

        Color32 levelColor32 = LevelDatabase.Instance.GetColor(levelColor);

        GetComponent<GridLayoutGroup>().constraintCount = fieldSize.x;
        field = new PartController[fieldSize.x, fieldSize.y];

        for (int y = 0; y < fieldSize.y; y++)
        {
            for (int x = 0; x < fieldSize.x; x++)
            {
                int elementIndex = y * fieldSize.x + x;
                //Debug.Log("Index: " + elementIndex);
                field[x, y] = Instantiate(casePrefab, fieldContainer).GetComponent<PartController>();

                Element newPart = LevelDatabase.Instance.emptyElemet;
                int newPartFlip = 0;
                //levelColor = LevelDatabase.Instance.defaultColor;

                if (level.elements != null)
                {
                    newPart = LevelDatabase.Instance.GetElement(level.elements[elementIndex]);

                    if (newPart.isFixed)
                        newPartFlip = level.elementFlip[elementIndex];
                    else
                        newPartFlip = UnityEngine.Random.Range(0, 4);
                }
                
                field[x, y].Init(newPart, newPartFlip, new Vector2Int(x, y));
                field[x, y].PaintSelf(levelColor32);
            }
        }

        //Destroy(casePrefab);
    }

    public bool CheckLoopComplete()
    {
        foreach(PartController pc in field)
        {
            if (!CheckElementLoop(pc))
            {
                isLooped = false;
                return isLooped;
            }
        }

        isLooped = true;
        return isLooped;
    }

    public void CheckLevelComplete()
    {
        if (CheckLoopComplete())
            OnLevelComplete.Invoke();
    }

    private bool CheckElementLoop(PartController part)
    {
        Vector2Int partPos = part.GetPosition();

        bool conLeft = true, conUp = true, conRight = true, conDown = true;

        if (part.connectionLeft == Element.ConnectionTypes.regular)
            if (partPos.x == 0 || field[partPos.x - 1, partPos.y].connectionRight == Element.ConnectionTypes.none)
                conLeft = false;

        if (part.connectionUp == Element.ConnectionTypes.regular)
            if (partPos.y == 0 || field[partPos.x, partPos.y - 1].connectionDown == Element.ConnectionTypes.none)
                conUp = false;

        if (part.connectionRight == Element.ConnectionTypes.regular)
            if (partPos.x == fieldSize.x - 1 || field[partPos.x + 1, partPos.y].connectionLeft == Element.ConnectionTypes.none)
                conRight = false;

        if (part.connectionDown == Element.ConnectionTypes.regular)
            if (partPos.y == fieldSize.y - 1 || field[partPos.x, partPos.y + 1].connectionUp == Element.ConnectionTypes.none)
                conDown = false;

        return conLeft && conUp && conRight && conDown;
    }

    private void ClearField()
    {
        foreach (PartController el in field)
        {
            Destroy(el.gameObject);
        }
    }

    public void Shufflefield(bool flashShuffle)
    {
        foreach (PartController el in field)
        {
            if(!el.isFixed)
            {
                el.FlipElement(UnityEngine.Random.Range(0, 4), flashShuffle);
            }
        }
    }

    public void PaintField(LevelDatabase.Colors colorName)
    {
        Color32 newColor = LevelDatabase.Instance.GetColor(colorName);

        foreach (PartController ec in field)
        {
            ec.PaintSelf(newColor);
        }
    }

    public void SetInteractable(bool interactable)
    {
        isInteractable = interactable;

        foreach (PartController p in field)
            p.SetInteractable(interactable);

        float newScale = interactable ? 1f : 0.75f;
        GetComponent<AnimationScale>().StartAnimationResize(newScale, 0.2f);
    }
}
