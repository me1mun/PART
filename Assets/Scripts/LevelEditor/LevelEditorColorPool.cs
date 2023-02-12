using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorColorPool : MonoBehaviour
{
    [SerializeField] private GameObject casePrefab;
    [SerializeField] private GameObject container;

    [SerializeField] private LevelDatabase.Colors activeColor = LevelDatabase.Colors.white;

    private List<LevelEditorColorCase> colorCases = new List<LevelEditorColorCase>();
    //private Dictionary<Element, LevelEditorElementCase> elementPool = new Dictionary<Element, LevelEditorElementCase>();

    void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        int colorsCount = LevelDatabase.Instance.colorsList.Count;
        //caseElements = new LevelEditorElementCase[elementCount];

        RectTransform containerRect = container.GetComponent<RectTransform>();
        float caseSize = containerRect.sizeDelta.x + container.GetComponent<HorizontalLayoutGroup>().spacing;
        containerRect.sizeDelta = new Vector2(caseSize * colorsCount + 60, containerRect.sizeDelta.y);

        foreach(KeyValuePair<LevelDatabase.Colors, Color32> col in LevelDatabase.Instance.colorsList)
        {
            LevelEditorColorCase newCase = Instantiate(casePrefab, container.transform).GetComponent<LevelEditorColorCase>();

            newCase.Init(col.Key, col.Value);

            colorCases.Add(newCase);
        }

        SetupCasesDisplay();

        //Destroy(casePrefab);
    }

    public void SetActiveColor(LevelDatabase.Colors colorName)
    {
        activeColor = colorName;

        SetupCasesDisplay();
        LevelEditor.Instance.field.PaintField(activeColor);
    }

    private void SetupCasesDisplay()
    {
        foreach (LevelEditorColorCase cc in colorCases)
        {
            cc.SetupDisplay(activeColor);
        }
    }

    public LevelDatabase.Colors GetActiveColor()
    {
        return activeColor;
    }
}
