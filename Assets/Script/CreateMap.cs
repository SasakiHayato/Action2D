using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapStatus
{
    Wall,
    Load,

    Start,
}

public class CreateMap : MonoBehaviour
{
    //[SerializeField] private GameObject m_cell = null;
    [SerializeField] private GameObject m_wallCell = null;
    [SerializeField] private GameObject m_player = null;

    [SerializeField] private GameObject m_grid = null;
    [SerializeField] private GameObject[] m_mapTips = null;

    private const int m_mapHeight = 7;
    private const int m_mapWide = 7;

    MapStatus[,] m_maps = new MapStatus[m_mapHeight, m_mapWide];

    SpriteRenderer m_cellRenderer = null;

    private int m_startX = 1;
    private int m_startY = 1;

    private int count = 10;

    void Start()
    {
        //m_cellRenderer = m_cell.GetComponent<SpriteRenderer>();

        CellReset();

        SetStartCell();

        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapHeight; y++)
            {
                CreateMapCell(x, y);
            }
        }

        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapHeight; y++)
            {
                if (m_maps[x, y] == MapStatus.Load)
                {
                    SetMapTip(x, y);
                }
            }
        }
    }

    private void CellReset()
    {
        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapWide; y++)
            {
                m_maps[x, y] = MapStatus.Wall;
            }
        }
    }

    private void SetStartCell()
    {
        m_maps[m_startX, m_startY] = MapStatus.Start;

        DirectionCheck(m_startX, m_startY);
    }

    private void DirectionCheck(int x, int y)
    {
        bool check = true;
        while (check)
        {
            count--;
            if (count < 0)
            {
                break;
            }

            int[] directionX = { 0, 2, -2 };
            int randomX = Random.Range(0, 3);

            if (directionX[randomX] == 0)
            {
                int[] directionY = { 2, -2 };
                int randomY = Random.Range(0, 2);

                if (y + directionY[randomY] < 0 || y + directionY[randomY] >= m_mapHeight) continue;

                if (m_maps[x, y + directionY[randomY]] == MapStatus.Wall)
                {
                    count = 10;
                    check = false;
                    SetStatus(x, y + directionY[randomY], 0, directionY[randomY]);

                }
                else
                {
                    continue;
                }
            }
            else
            {
                if (x + directionX[randomX] < 0 || x + directionX[randomX] >= m_mapWide) continue;

                if (m_maps[x + directionX[randomX], y] == MapStatus.Wall)
                {
                    count = 10;
                    check = false;
                    SetStatus(x + directionX[randomX], y, directionX[randomX], 0);

                }
                else
                {
                    continue;
                }
            }
        }
    }

    private void SetStatus(int x, int y, int directionX, int directionY)
    {
        m_maps[x, y] = MapStatus.Load;

        if (directionX < 0)
        {
            m_maps[x + 1, y] = MapStatus.Load;
        }
        else if (directionX > 0)
        {
            m_maps[x - 1, y] = MapStatus.Load;
        }

        if (directionY < 0)
        {
            m_maps[x, y + 1] = MapStatus.Load;
        }
        else if (directionY > 0)
        {
            m_maps[x, y - 1] = MapStatus.Load;
        }

        DirectionCheck(x, y);
    }

    private void SetMapTip(int mapX, int mapY)
    {
        int count = 0;
        MapStatus targetMap = m_maps[mapX, mapY];
        for (int x = mapX - 1; x <= mapX + 1; x++)
        {
            for (int y = mapY - 1; y <= mapY + 1; y++)
            {
                if (x < 0 || x >= m_mapWide)
                {
                    continue;
                }
                else if (y < 0 || y >= m_mapHeight)
                {
                    continue;
                }
                if (targetMap == m_maps[x, y])
                {
                    count++;
                }
            }
        }

        Debug.Log(count);
    }

    private void CreateMapCell(int x, int y)
    {
        GameObject setCell = new GameObject();
        Vector3 vector = new Vector3();
        Vector2 vec = new Vector2(x * 8 - m_mapWide / 2, y * 8 - m_mapHeight / 2);

        if (m_maps[x, y] == MapStatus.Wall)
        {
            vector = new Vector3(vec.x, vec.y, 0);
            setCell = m_wallCell;
        }
        if (m_maps[x, y] == MapStatus.Load)
        {
            //m_cellRenderer.color = Color.white;
            vector = new Vector3(vec.x, vec.y, 1);
            setCell = m_mapTips[0];
        }
        else if (m_maps[x, y] == MapStatus.Start)
        {
            vector = new Vector3(vec.x, vec.y, 0);
            //setCell = m_player;
            setCell = m_mapTips[1];
        }

        GameObject cell = Instantiate(setCell, vector, Quaternion.identity);
        cell.transform.SetParent(m_grid.transform);
    }
}
