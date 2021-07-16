using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapStatus
{
    Wall,
    Load,
}

public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject m_wallCell = null;
    [SerializeField] private GameObject m_grid = null;

    [SerializeField] private GameObject[] m_tipsVertical = null;
    [SerializeField] private GameObject[] m_tipsHorizontal = null;
    [SerializeField] private GameObject[] m_tipsCorner = null;

    private const int m_mapHeight = 7;
    private const int m_mapWide = 7;

    MapStatus[,] m_maps = new MapStatus[m_mapHeight, m_mapWide];

    private int m_startX = 1;
    private int m_startY = 1;

    private int m_checkCount = 10;

    void Start()
    {
        CellReset();

        SetStartCell();

        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapHeight; y++)
            {
                CreateMapCell(x, y);
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
        m_maps[m_startX, m_startY] = MapStatus.Load;

        DirectionCheck(m_startX, m_startY);
    }

    private void DirectionCheck(int x, int y)
    {
        bool check = true;
        while (check)
        {
            m_checkCount--;
            if (m_checkCount < 0)
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
                    m_checkCount = 10;
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
                    m_checkCount = 10;
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

    private void CreateMapCell(int x, int y)
    {
        GameObject setCell = new GameObject();
        Vector3 setVec = new Vector3();
        Vector2 vector = new Vector2(x * 8 - m_mapWide / 2, y * 8 - m_mapHeight / 2);

        if (m_maps[x, y] == MapStatus.Wall)
        {
            setVec = new Vector3(vector.x, vector.y, 0);
            setCell = m_wallCell;
        }
        if (m_maps[x, y] == MapStatus.Load)
        {
            setVec = new Vector3(vector.x, vector.y, 1);
            setCell = SetMapTip(x, y);
        }
        
        GameObject cell = Instantiate(setCell, setVec, Quaternion.identity);
        cell.transform.SetParent(m_grid.transform);
    }

    private GameObject SetMapTip(int mapX, int mapY)
    {
        bool right = false;
        bool left = false;

        bool up = false;
        bool down = false;

        //上
        if (m_maps[mapX, mapY + 1] == MapStatus.Load)
        {
            up = true;
        }
        //下
        if (m_maps[mapX, mapY - 1] == MapStatus.Load)
        {
            down = true;
        }
        //右
        if (m_maps[mapX + 1, mapY] == MapStatus.Load)
        {
            right = true;
        }
        //左
        if (m_maps[mapX - 1, mapY] == MapStatus.Load)
        {
            left = true;
        }

        GameObject set = new GameObject();

        // 左右判定
        if (right && left && !up && !down)
        {
            set = m_tipsHorizontal[0];
            return set;
        }
        else if (right && !left && !up && !down)
        {
            set = m_tipsHorizontal[1];
            return set;
        }
        else if (left && !right && !up && !down)
        {
            set = m_tipsHorizontal[2];
            return set;
        }

        // 上下判定
        if (up && down && !right && !left)
        {
            set = m_tipsVertical[0];
            return set;
        }
        else if (up && !down && !right && !left)
        {
            set = m_tipsVertical[1];
            return set;
        }
        else if (down && !up && !right && !left)
        {
            set = m_tipsVertical[2];
            return set;
        }

        //角判定
        if (right && down && !left && !up)
        {
            set = m_tipsCorner[1];
            return set;
        }
        else if (right && up && !left && !down)
        {
            set = m_tipsCorner[3];
            return set;
        }
        else if (left && up && !right && !down)
        {
            set = m_tipsCorner[2];
            return set;
        }
        else if (left && down && !right && !up)
        {
            set = m_tipsCorner[0];
            return set;
        }
        else
        {
            return set;
        }
    }
}
