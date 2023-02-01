using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorField : MonoBehaviour
{
    [SerializeField] private GameObject cellsParent;
    [SerializeField] private GameObject cellPrefab;
    public LevelEditorCell[,] cellsList;
    public Element activeElement;

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
                cellsList[x, y] = Instantiate(cellPrefab, cellsParent.transform).GetComponent<LevelEditorCell>();
                cellsList[x, y].position = new Vector2Int(x, y);
                //cellsList[x, y].field = this;
            }
        }

        Destroy(cellPrefab);
    }
}
