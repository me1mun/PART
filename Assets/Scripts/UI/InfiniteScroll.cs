using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject casePrefab;
    [SerializeField] private GameObject container;

    private RectTransform canvasRect;
    [SerializeField] private Transform borderUp, borderDown;
    private Dictionary<GameObject, InfiniteScrollCase> casesList = new Dictionary<GameObject, InfiniteScrollCase>();

    [Space(200)]
    private int casesCount;
    [SerializeField] private int caseSize = 140;
    [SerializeField] private int casesInRow = 5;

    private const int caseGap = 15, containerGap = 30;
    private int casesMax = 45;
    private int casesClamp;
    private GameObject firstCase, lastCase;

    private void Awake()
    {
        canvasRect = GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>();
        casesMax = System.Convert.ToInt32((borderUp.localPosition.y - borderDown.localPosition.y) / (caseSize + caseGap) + 3) * casesInRow;
        //Debug.Log((borderUp.localPosition.y - borderDown.localPosition.y) + " | " + casesMax);
    }

    private void Start()
    {
        //CreateCases(LevelManager.Instance.GetLevelChallangeCount());
    }

    private void Update()
    {
        ReplaceCase();
    }

    void OnEnable()
    {
        //CreateCases(casesCount);
    }

    public void CreateCases(int casesCount)
    {
        DeleteCases();

        this.casesCount = casesCount;
        casesClamp = Mathf.Min(casesCount, casesMax);

        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, Mathf.Ceil((float)casesCount / casesInRow) * (caseSize + caseGap) + containerGap * 2);

        for (int i = 0; i < casesClamp; i++)
        {
            //Debug.Log(casesCount);
            GameObject newCaseObject = Instantiate(casePrefab, container.transform);

            RectTransform caseRect = newCaseObject.GetComponent<RectTransform>();
            caseRect.localPosition = GetCasePosition(i);

            InfiniteScrollCase newCase = newCaseObject.GetComponent<InfiniteScrollCase>();
            //newCase.SetTabLevels(tabLevels);
            newCase.Init(i);

            casesList.Add(newCaseObject, newCase);
        }
    }

    private void DeleteCases()
    {
        List<GameObject> keys = new List<GameObject>(casesList.Keys);

        foreach (GameObject key in keys)
        {
            key.transform.SetParent(null);
            Destroy(key);
        }

        casesList.Clear();
    }

    public void InitCases()
    {
        foreach (KeyValuePair<GameObject, InfiniteScrollCase> cs in casesList)
        {
            cs.Value.Init(cs.Value.GetIndex());
        }
    }

    private void ReplaceCase()
    {
        if(casesCount > 0)
        {
            float replaceCaseGap = 300 / canvasRect.sizeDelta.y * Camera.main.orthographicSize;
            Transform containerTransform = container.transform;

            firstCase = containerTransform.GetChild(0).gameObject;
            lastCase = containerTransform.GetChild(casesClamp - 1).gameObject;

            while (casesList[firstCase].GetIndex() > 0 && (firstCase.transform.position.y < borderUp.position.y + replaceCaseGap))
            {
                int newCaseIndex = casesList[firstCase].GetIndex() - 1;
                lastCase.transform.localPosition = GetCasePosition(newCaseIndex);

                casesList[lastCase].Init(newCaseIndex);
                casesList[lastCase].transform.SetAsFirstSibling();

                firstCase = containerTransform.GetChild(0).gameObject;
                lastCase = containerTransform.GetChild(casesClamp - 1).gameObject;
            }

            while (casesList[lastCase].GetIndex() < casesCount - 1 && (lastCase.transform.position.y > borderDown.position.y - replaceCaseGap))
            {
                int newCaseIndex = casesList[lastCase].GetIndex() + 1;
                firstCase.transform.localPosition = GetCasePosition(newCaseIndex);

                casesList[firstCase].Init(casesList[lastCase].GetIndex() + 1);
                casesList[firstCase].transform.SetAsLastSibling();

                firstCase = containerTransform.GetChild(0).gameObject;
                lastCase = containerTransform.GetChild(casesClamp - 1).gameObject;
            }
        }
    }

    private Vector3 GetCasePosition(int caseIndex)
    {
        int rowIndex = caseIndex / casesInRow;
        int indexInRow = caseIndex % casesInRow;
        int spacing = caseGap + caseSize;

        return new Vector3(((casesInRow - 1) * -0.5f * spacing) + indexInRow * spacing, (rowIndex + 0.5f) * -spacing - containerGap, 0);
    }
}
