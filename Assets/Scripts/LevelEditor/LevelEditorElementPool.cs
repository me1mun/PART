using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorElementPool : MonoBehaviour
{
    [SerializeField] LevelEditorField field;

    [SerializeField] private GameObject casePrefab;
    [SerializeField] private GameObject container;

    private GameObject[] caseElements;
    private Dictionary<Element, GameObject> elementPool = new Dictionary<Element, GameObject>();

    void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        int elementCount = ElementData.Instance.elements.Length;
        caseElements = new GameObject[elementCount];

        int caseSize = 120 + 30;
        RectTransform containerRect = container.GetComponent<RectTransform>();
        containerRect.sizeDelta = new Vector2(caseSize * elementCount + 60, containerRect.sizeDelta.y);

        for (int i = 0; i < elementCount; i++)
        {
            caseElements[i] = Instantiate(casePrefab, container.transform);
            elementPool.Add(ElementData.Instance.elements[i], caseElements[i]);

            caseElements[i].GetComponent<LevelEditorElementCase>().Init(ElementData.Instance.elements[i]);
        }

        Destroy(casePrefab);
    }

    public void DisplayActiveElement(Element el)
    {

    }
}
