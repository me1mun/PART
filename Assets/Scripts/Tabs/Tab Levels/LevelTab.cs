using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTab : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject casePrefab;
    [SerializeField] private GameObject container;

    //private List<GameObject> rows = new List<GameObject>();
    private Dictionary<GameObject, LevelCase> casesList = new Dictionary<GameObject, LevelCase>();
    private const int casesInRow = 5, casesGap = 170;
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
        foreach (KeyValuePair<GameObject, LevelCase> lcr in casesList)
        {
            Destroy(casesList[lcr.Key]);
        }
        casesList.Clear();
        //rowsClass.Clear();

        casesCount = GameManager.levelCount;
        casesClamp = Mathf.Clamp(casesCount, 1, 45);
        Debug.Log(casesCount / casesInRow);

        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, Mathf.Ceil((float)casesCount / casesInRow) * (casesGap));

        for(int i = 0; i < casesClamp; i++)
        {
            GameObject newCase = Instantiate(casePrefab, container.transform);

            RectTransform caseRect = newCase.GetComponent<RectTransform>();
            caseRect.localPosition = GetCasePosition(i);

            newCase.GetComponent<LevelCase>().Init(i);

            //rows.Add(newRow);
            casesList.Add(newCase, newCase.GetComponent<LevelCase>());
        }
    }

    private void OnEnable()
    {
        //Init();
    }

    private void ReplaceCase()
    {
        replaceCaseGap = 200 / canvasRect.sizeDelta.y * Camera.main.orthographicSize;
        //Debug.Log(rows[lastRow].GetComponent<LevelCaseRow>().GetStartLevel());
        Transform containerTransform = container.transform;

        GameObject firstCase = containerTransform.GetChild(0).gameObject;
        GameObject lastCase = containerTransform.GetChild(casesClamp - 1).gameObject;

        if (casesList[firstCase].GetLevelIndex() > 0)
        {
            if (firstCase.transform.position.y < borderUp.position.y + replaceCaseGap)
            {
                //Debug.Log("last to start");
                int newCaseIndex = casesList[firstCase].GetLevelIndex() - 1;
                lastCase.transform.localPosition = GetCasePosition(newCaseIndex);

                casesList[lastCase].Init(newCaseIndex);
                casesList[lastCase].transform.SetAsFirstSibling();

                firstCase = containerTransform.GetChild(0).gameObject;
                lastCase = containerTransform.GetChild(casesClamp - 1).gameObject;
            }
        }

        if (casesList[lastCase].GetLevelIndex() < casesCount - 1)
        {
            if (lastCase.transform.position.y > borderDown.position.y - replaceCaseGap)
            {
                //Debug.Log("firs to end");
                int newCaseIndex = casesList[lastCase].GetLevelIndex() + 1;
                firstCase.transform.localPosition = GetCasePosition(newCaseIndex);

                casesList[firstCase].Init(casesList[lastCase].GetLevelIndex() + 1);
                casesList[firstCase].transform.SetAsLastSibling();
            }
        }
    }

    private Vector3 GetCasePosition(int caseIndex)
    {
        int rowIndex = caseIndex / casesInRow;
        int indexInRow = caseIndex % casesInRow;

        return new Vector3(((casesInRow - 1) * -0.5f * casesGap) + indexInRow * casesGap, (rowIndex + 0.5f) * -casesGap, 0);
    }
}
