using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorElementPool : MonoBehaviour
{
    [SerializeField] private LevelEditor editor;
    [SerializeField] private GameObject casePrefab;
    [SerializeField] private GameObject container;

    private Element activeElement = LevelDatabase.Instance.emptyElemet;

    private List<LevelEditorElementCase> elementCases = new List<LevelEditorElementCase>();
    //private Dictionary<Element, LevelEditorElementCase> elementPool = new Dictionary<Element, LevelEditorElementCase>();

    void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        int elementCount = LevelDatabase.Instance.elements.Length;

        int caseSize = 120 + 30;
        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(caseSize * elementCount + 60, containerRect.sizeDelta.y);

        elementCases.Add(casePrefab.GetComponent<LevelEditorElementCase>());

        for (int i = 0; i < elementCount; i++)
        {
            LevelEditorElementCase newCase = Instantiate(casePrefab, container.transform).GetComponent<LevelEditorElementCase>();
            newCase.Init(LevelDatabase.Instance.elements[i]);

            elementCases.Add(newCase);
        }


        SetupCasesDisplay();

        //Destroy(casePrefab);
    }

    public void SetActiveElement(Element el)
    {
        activeElement = el;

        SetupCasesDisplay();
    }

    public Element GetActiveElement()
    {
        return activeElement;
    }

    private void SetupCasesDisplay()
    {
        foreach(LevelEditorElementCase ce in elementCases)
        {
            ce.SetupDisplay(activeElement);
        }
    }
}
