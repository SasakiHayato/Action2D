using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum MapStatus
{
    Wall,
    Load,

    Start,
    Goal,
}

public enum MapCell
{
    Open,
    Close,
}

public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject m_mapWall = null;

    [SerializeField] private GameObject[] m_horizontal = null;
    [SerializeField] private GameObject[] m_vertical = null;
    [SerializeField] private GameObject[] m_corner = null;
    [SerializeField] private GameObject[] m_threeDirections = null;
    [SerializeField] private GameObject m_all = null;

    [SerializeField] private Grid m_grid = null;

    private const int m_mapHeight = 15;
    private const int m_mapWide = 15;

    MapStatus[,] m_maps = new MapStatus[m_mapHeight, m_mapWide];
    MapCell[,] m_cells = new MapCell[m_mapHeight, m_mapWide];

    private int m_startX = 1;
    private int m_startY = 1;

    private List<int> m_xMapLoadList = new List<int>();
    private List<int> m_yMapLoadList = new List<int>();

    private List<int> m_xAddCellList = new List<int>();
    private List<int> m_yAddCellList = new List<int>();

    private List<int> m_xEndCellList = new List<int>();
    private List<int> m_yEndCellList = new List<int>();

    private int m_xCount = 0;
    private int m_yCount = 0;

    void Start()
    {
        MapReset();

        SetStartMap();
        BackLoad(m_xMapLoadList[m_xCount - 1], m_yMapLoadList[m_yCount - 1]);

        OpenLoadEnum();

        FindLoad(m_startX, m_startY);
        SetGoalMap();

        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapHeight; y++)
            {
                CreateMapCell(x, y);
            }
        }
    }

    private void MapReset()
    {
        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapWide; y++)
            {
                m_maps[x, y] = MapStatus.Wall;
                m_cells[x, y] = MapCell.Open;
            }
        }
    }

    private void SetStartMap()
    {
        m_maps[m_startX, m_startY] = MapStatus.Start;

        DirectionCheck(m_startX, m_startY);
    }

    private void DirectionCheck(int x, int y)
    {
        int count = 10;
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

    private void BackLoad(int mapX, int mapY)
    {
        m_xCount--;
        m_yCount--;

        if (m_xCount <= 0 || m_yCount <= 0)
        {
            return;
        }

        DirectionCheck(m_xMapLoadList[m_xCount - 1], m_yMapLoadList[m_yCount - 1]);
        BackLoad(m_xMapLoadList[m_xCount - 1], m_yMapLoadList[m_yCount - 1]);
    }

    private void OpenLoadEnum()
    {
        for (int x = 0; x < m_mapWide; x++)
        {
            for (int y = 0; y < m_mapWide; y++)
            {
                if (m_maps[x, y] != MapStatus.Wall)
                {
                    m_cells[x, y] = MapCell.Open;
                }
                else
                {
                    m_cells[x, y] = MapCell.Close;
                }
            }
        }
    }

    private void FindLoad(int x, int y)
    {
        if (m_cells[x, y] != MapCell.Open) return;
        m_cells[x, y] = MapCell.Close;
        int count = 0;

        bool right = false;
        bool left = false;
        bool up = false;
        bool down = false;

        if (m_cells[x + 1, y] == MapCell.Open)
        {
            count++;
            right = true;
        }
        if (m_cells[x - 1, y] == MapCell.Open)
        {
            count++;
            left = true;
        }
        if (m_cells[x, y + 1] == MapCell.Open)
        {
            count++;
            up = true;
        }
        if (m_cells[x, y - 1] == MapCell.Open)
        {
            count++;
            down = true;
        }

        if (count == 1)
        {
            if (right)
            {
                FindLoad(x + 1, y);
            }
            if (left)
            {
                FindLoad(x - 1, y);
            }
            if (up)
            {
                FindLoad(x, y + 1);
            }
            if (down)
            {
                FindLoad(x, y - 1);
            }
        }
        else if (count == 0)
        {
            m_xEndCellList.Add(x);
            m_yEndCellList.Add(y);

            Debug.Log("END is" + " X" + m_xEndCellList.Last() + " Y" + m_yEndCellList.Last());
        }
        else if (count > 1)
        {
            SelectLoad(x, y);
        }
    }

    private void SelectLoad(int x, int y)
    {
        if (m_cells[x + 1, y] == MapCell.Open)
        {
            AddCellList(x + 1, y);
        }
        if (m_cells[x - 1, y] == MapCell.Open)
        {
            AddCellList(x - 1, y);
        }
        if (m_cells[x, y + 1] == MapCell.Open)
        {
            AddCellList(x, y + 1);
        }
        if (m_cells[x, y - 1] == MapCell.Open)
        {
            AddCellList(x, y - 1);
        }

        while (m_yAddCellList.Count != 0 || m_xAddCellList.Count != 0)
        {
            FindLoad(m_xAddCellList.First(), m_yAddCellList.First());

            if (m_yAddCellList.Count == 0 || m_xAddCellList.Count == 0)
            {
                break;
            }
            m_yAddCellList.Remove(m_yAddCellList.First());
            m_xAddCellList.Remove(m_xAddCellList.First());
        }
    }

    private void AddCellList(int x, int y)
    {
        m_xAddCellList.Add(x);
        m_yAddCellList.Add(y);
    }

    private void SetGoalMap()
    {
        int random = Random.Range(0, m_xEndCellList.Count);
        Debug.Log(random);
        m_maps[m_xEndCellList[random], m_yEndCellList[random]] = MapStatus.Goal;
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

        m_xMapLoadList.Add(x);
        m_yMapLoadList.Add(y);

        m_xCount++;
        m_yCount++;
        DirectionCheck(x, y);
    }

    private GameObject SetMapTip(int mapX, int mapY)
    {
        GameObject set = default;
        bool up = false;
        bool down = false;

        bool right = false;
        bool left = false;
        //上
        if (m_maps[mapX, mapY + 1] != MapStatus.Wall)
        {
            up = true;
        }
        //下
        if (m_maps[mapX, mapY - 1] != MapStatus.Wall)
        {
            down = true;
        }
        //右
        if (m_maps[mapX + 1, mapY] != MapStatus.Wall)
        {
            right = true;
        }
        //左
        if (m_maps[mapX - 1, mapY] != MapStatus.Wall)
        {
            left = true;
        }
        // 左右判定
        if (right && left && !up && !down)
        {
            Debug.Log("左右");
            set = m_horizontal[0];
        }
        else if (right && !left && !up && !down)
        {
            Debug.Log("右");
            set = m_horizontal[2];
            
        }
        else if (left && !right && !up && !down)
        {
            Debug.Log("左");
            set = m_horizontal[1];
            
        }

        // 上下判定
        if (up && down && !right && !left)
        {
            Debug.Log("上下");
            set = m_vertical[0];
            
        }
        else if (up && !down && !right && !left)
        {
            Debug.Log("上");
            set = m_vertical[1];
        }
        else if (down && !up && !right && !left)
        {
            Debug.Log("下");
            set = m_vertical[2];
        }

        //角判定
        if (right && down && !left && !up)
        {
            Debug.Log("左上");
            set = m_corner[1];
        }
        else if (right && up && !left && !down)
        {
            Debug.Log("左下");
            set = m_corner[3];
        }
        else if (left && up && !right && !down)
        {
            Debug.Log("右下");
            set = m_corner[2];
        }
        else if (left && down && !right && !up)
        {
            Debug.Log("右上");
            set = m_corner[0];
        }

        // 三方向の判定
        if (right && left && up && !down)
        {
            Debug.Log("下には行けない");
            set = m_threeDirections[0];
        }
        if (right && left && !up && down)
        {
            Debug.Log("上には行けない");
            set = m_threeDirections[1];
        }
        if (right && !left && up && down)
        {
            Debug.Log("左には行けない");
            set = m_threeDirections[3];
        }
        if (!right && left && up && down)
        {
            Debug.Log("右には行けない");
            set = m_threeDirections[2];
        }

        // 全方向の判定
        if (left && right && up && down)
        {
            Debug.Log("全方向");
            set = m_all;
        }

        return set;
    }

    private void CreateMapCell(int x, int y)
    {
        GameObject set = default; // new GameObject();
        Vector2 vector = new Vector2(x * 8 - m_mapWide / 2, y * 8 - m_mapHeight / 2);

        Vector3 setVec = new Vector3();

        if (m_maps[x, y] == MapStatus.Wall)
        {
            set = m_mapWall;
            setVec = new Vector3(vector.x, vector.y, 0);
        }
        if (m_maps[x, y] == MapStatus.Load)
        {
            set = SetMapTip(x, y);
            setVec = new Vector3(vector.x, vector.y, 1);
        }
        if (m_maps[x, y] == MapStatus.Start)
        {
            set = SetMapTip(x, y);
            setVec = new Vector3(vector.x, vector.y, 0);
        }
        //if (m_maps[x, y] == MapStatus.Goal)
        //{
        //    m_cellRenderer.color = Color.blue;
        //}
        if (set == default)
        {
            set = new GameObject();
        }

        GameObject cell = Instantiate(set, setVec, Quaternion.identity);
        cell.transform.SetParent(m_grid.transform);
    }
}
