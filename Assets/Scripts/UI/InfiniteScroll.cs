using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject casePrefab;
    [SerializeField] private GameObject container;

    //private List<GameObject> rows = new List<GameObject>();
    private Dictionary<GameObject, InfiniteScrollCase> casesList = new Dictionary<GameObject, InfiniteScrollCase>();
    private const int caseSize = 160, caseGap = 20;
    private const int casesInRow = 5;
    private int casesCount, casesClamp;
    private float replaceCaseGap;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private Transform borderUp, borderDown;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ReplaceCase();
        }

        ReplaceCase();
    }

    public void Init()
    {
        foreach (KeyValuePair<GameObject, InfiniteScrollCase> cs in casesList)
        {
            Destroy(casesList[cs.Key]);
        }
        casesList.Clear();

        casesCount = GameManager.levelCount;
        casesClamp = Mathf.Clamp(casesCount, 0, 45);

        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, Mathf.Ceil((float)casesCount / casesInRow) * (caseSize + caseGap));

        for (int i = 0; i < casesClamp; i++)
        {
            //Debug.Log(casesCount);
            GameObject newCase = Instantiate(casePrefab, container.transform);

            RectTransform caseRect = newCase.GetComponent<RectTransform>();
            caseRect.localPosition = GetCasePosition(i);

            newCase.GetComponent<InfiniteScrollCase>().Init(i);

            casesList.Add(newCase, newCase.GetComponent<InfiniteScrollCase>());
        }
    }

    private void OnEnable()
    {
        //Init();
    }

    private void ReplaceCase()
    {
        replaceCaseGap = 300 / canvasRect.sizeDelta.y * Camera.main.orthographicSize;
        Transform containerTransform = container.transform;

        GameObject firstCase = containerTransform.GetChild(0).gameObject;
        GameObject lastCase = containerTransform.GetChild(casesClamp - 1).gameObject;

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

    private Vector3 GetCasePosition(int caseIndex)
    {
        int rowIndex = caseIndex / casesInRow;
        int indexInRow = caseIndex % casesInRow;
        int spacing = caseGap + caseSize;

        return new Vector3(((casesInRow - 1) * -0.5f * spacing) + indexInRow * spacing, (rowIndex + 0.5f) * -spacing, 0);
    }
}
