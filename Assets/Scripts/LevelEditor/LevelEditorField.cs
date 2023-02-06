using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorField : MonoBehaviour
{
    [SerializeField] private Transform cellsParent;
    [SerializeField] private GameObject cellPrefab;
    public LevelEditorCell[,] cellsList;

    private Vector2Int fieldSize = new Vector2Int(8, 10);

    void Start()
    {
        CellsGenerate();
    }

    private void CellsGenerate()
    {
        //GameObject cellPrefab = cellsParent.transform.GetChild(0).gameObject;
        cellsList = new LevelEditorCell[fieldSize.x, fieldSize.y];

        for (int y = 0; y < fieldSize.y; y++)
        {
            for (int x = 0; x < fieldSize.x; x++)
            {
                cellsList[x, y] = Instantiate(cellPrefab, cellsParent).GetComponent<LevelEditorCell>();
            }
        }

        Destroy(cellPrefab);
    }
    
    public void PaintField(LevelDatabase.Colors colorName)
    {
        Color32 newColor = LevelDatabase.Instance.GetColor(colorName);

        foreach (LevelEditorCell ec in cellsList)
        {
            ec.PaintSelf(newColor);
        }
    }
}
