using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevelGeneration : MonoBehaviour
{
    private RandomPart[,] field;

    private Vector2Int levelSize;
    private int connectionChance = 72;

    [SerializeField] private Element elUniversal ,elEmpty, elSingle, elLine, elTurn, elTriple, elCross;

    public Level GenerateLevel(Level randomLevel)
    {
        levelSize.x = Random.Range(4, 6);
        levelSize.y = Random.Range(levelSize.x, 9);

        randomLevel.width = levelSize.x;
        randomLevel.height = levelSize.y;
        LevelDatabase.Colors[] colors = (LevelDatabase.Colors[])System.Enum.GetValues(typeof(LevelDatabase.Colors));
        randomLevel.colorName = System.Enum.GetName(typeof(LevelDatabase.Colors), colors[Random.Range(0, colors.Length)]);
        randomLevel.elements = new string[levelSize.x * levelSize.y];
        randomLevel.elementFlip = new int[levelSize.x * levelSize.y];

        field = new RandomPart[levelSize.x, levelSize.y];
        for (int y = 0; y < levelSize.y; y++)
        {
            for (int x = 0; x < levelSize.x; x++)
            {
                field[x, y] = new RandomPart();
            }
        }


        for (int y = 0; y < levelSize.y; y++)
        {
            for (int x = 0; x < levelSize.x; x++)
            {
                RandomPart part = field[x, y];
                int oneDemIndex = y * levelSize.x + x;

                if (x > 0 && field[x - 1, y].right)
                    part.left = true;

                if (y > 0 && field[x, y - 1].down)
                    part.up = true;

                if (x < levelSize.x - 1)
                    part.right = GetChance(connectionChance);

                if (y < levelSize.y - 1)
                    part.down = GetChance(connectionChance);

                part.element = SelectElement(part);

                //Debug.Log(oneDemIndex + " | " + randomLevel.elements[oneDemIndex] + " | " + part.element.name);
                randomLevel.elements[oneDemIndex] = part.element.name;
                randomLevel.elementFlip[oneDemIndex] = 0;
            }
        }

        return randomLevel;
    }

    private Element SelectElement(RandomPart part)
    {
        int connnectionsCount = 0;
        connnectionsCount += part.left ? 1 : 0;
        connnectionsCount += part.up ? 1 : 0;
        connnectionsCount += part.right ? 1 : 0;
        connnectionsCount += part.down ? 1 : 0;
        Debug.Log(connnectionsCount);

        switch (connnectionsCount)
        {
            case 0:
                return elEmpty;
            case 1:
                return elSingle;
            case 2:
                if (part.left && part.right || part.up && part.down)
                    return elLine;
                else
                    return elTurn;
            case 3:
                return elTriple;
            case 4:
                return elCross;
            default:
                return elUniversal;
        }
    }

    private bool GetChance(int probability)
    {
        return (Random.Range(0, 100) < probability);
    }


}

public class RandomPart
{
    public Element element;
    public bool left, up, right, down;
}