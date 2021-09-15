using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMapClass : MonoBehaviour
{
    const int m_wide = 16;
    const int m_height = 16;

    int m_startPosX;
    int m_startPosY;

    [SerializeField] GameObject m_wall;
    [SerializeField] GameObject m_load;

    private enum Type
    {
        Wall,
        Load,
    }
    Type[,] m_mapType = new Type[m_wide, m_height];

    void Start()
    {
        MapTypeReset();

        int startPosX = 0;
        int startPosY = 0;
        StartPosSet(ref startPosX,ref startPosY);
        Debug.Log($"StartX {startPosX} StartY {startPosY}");
        SetDir(startPosX, startPosY);

        for (int x = 0; x < m_wide; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                SetType(x, y);
            }
        }
    }

    void MapTypeReset()
    {
        for (int x = 0; x < m_wide; x++)
        {
            for (int y = 0; y < m_height; y++)
            {
                m_mapType[x, y] = Type.Wall;
            }
        }
    }
    void StartPosSet(ref int x,ref int y)
    {
        bool xBool = false;
        bool yBool = false;
        
        while (!xBool)
        {
            int posX = Random.Range(m_wide - 3, m_wide - 2);
            if (posX % 2 == 1)
            {
                xBool = true;
                x = posX;
            }
        }
        while(!yBool)
        {
            int posY = Random.Range(m_height - 3, m_height - 2);
            if (posY % 2 == 1)
            {
                yBool = true;
                y = posY;
            }
        }
        m_mapType[x, y] = Type.Load;
    }
    void SetDir(int x, int y)
    {
        int count = 10;
        while (count > 0)
        {
            count--;
            int dirX = Random.Range(-1, 2);
            if (dirX == 0)
            {
                int dirY = Random.Range(-1, 2);
                if (dirY == 0) count--;
                else
                {
                    int setY = dirY * 2;
                    if (setY + y < 0 || setY + y >= m_height) continue;

                    if (m_mapType[x, setY + y] == Type.Wall)
                    {
                        Digging(x, setY + y);
                        break;
                    }
                }

            }
            else
            {
                int setX = dirX * 2;
                if (setX + x < 0 || setX + x >= m_wide) continue;

                if (m_mapType[setX + x, y] == Type.Wall)
                {
                    Digging(setX + x, y);
                    break;
                }
            }
        }
    }
    void Digging(int x, int y)
    {
        Debug.Log($"X :{x} Y :{y}");
        m_mapType[x, y] = Type.Load;
    }
    void SetType(int x, int y)
    {
        GameObject setObject = default;
        Vector2 setVec = new Vector2(x * 8 - m_wide / 2, y * 8 - m_height / 2);
        if (m_mapType[x, y] == Type.Wall)
        {
            setObject = Instantiate(m_wall);
        }
        else if (m_mapType[x, y] == Type.Load)
        {
            setObject = Instantiate(m_load);
        }

        setObject.transform.position = setVec;
        setObject.name = $"X :{x} Y :{y}";
    }
}
