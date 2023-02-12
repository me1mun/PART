using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTab : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;

    [SerializeField] private GameObject rowPrefab;
    [SerializeField] private GameObject container;

    //private List<GameObject> rows = new List<GameObject>();
    private Dictionary<GameObject, LevelCaseRow> rows = new Dictionary<GameObject, LevelCaseRow>();
    private int casesInRow = 5;
    private int rowsCount, rowsClamp;
    private float replaceRowGap;
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
            ReplaceRow();
        }

        ReplaceRow();
    }

    public void Init()
    {
        foreach (KeyValuePair<GameObject, LevelCaseRow> lcr in rows)
        {
            Destroy(rows[lcr.Key]);
        }
        rows.Clear();
        //rowsClass.Clear();

        rowsCount = (int)Mathf.Ceil((float)GameManager.levelCount / casesInRow);
        rowsClamp = Mathf.Clamp(rowsCount, 1, 10);

        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(containerRect.sizeDelta.x, rowsCount * (140 + 30));

        for(int i = 0; i < rowsClamp; i++)
        {
            GameObject newRow = Instantiate(rowPrefab, container.transform);

            RectTransform rowRect = newRow.GetComponent<RectTransform>();
            rowRect.localPosition = new Vector3(rowRect.localPosition.x, (i + 0.5f) * -170, rowRect.localPosition.z);

            newRow.GetComponent<LevelCaseRow>().Init(i);

            //rows.Add(newRow);
            rows.Add(newRow, newRow.GetComponent<LevelCaseRow>());
        }
    }

    private void OnEnable()
    {
        //Init();
    }

    private void ReplaceRow()
    {
        replaceRowGap = 200 / canvasRect.sizeDelta.y * Camera.main.orthographicSize;
        //Debug.Log(rows[lastRow].GetComponent<LevelCaseRow>().GetStartLevel());
        Transform containerTransform = container.transform;

        GameObject firstRow = containerTransform.GetChild(0).gameObject;
        GameObject lastRow = containerTransform.GetChild(rowsClamp - 1).gameObject;

        if (rows[firstRow].GetRowNum() > 0)
        {
            if (firstRow.transform.position.y < borderUp.position.y + replaceRowGap)
            {
                lastRow.transform.localPosition = firstRow.transform.localPosition + new Vector3(0, 170, 0);

                rows[lastRow].Init(rows[firstRow].GetRowNum() - 1);
                rows[lastRow].transform.SetAsFirstSibling();

                firstRow = containerTransform.GetChild(0).gameObject;
                lastRow = containerTransform.GetChild(rowsClamp - 1).gameObject;
            }
        }

        if (rows[lastRow].GetRowNum() < rowsCount - 1)
        {
            if (lastRow.transform.position.y > borderDown.position.y - replaceRowGap)
            {
                firstRow.transform.localPosition = lastRow.transform.localPosition - new Vector3(0, 170, 0);

                rows[firstRow].Init(rows[lastRow].GetRowNum() + 1);
                rows[firstRow].transform.SetAsLastSibling();
            }
        }
    }
}
