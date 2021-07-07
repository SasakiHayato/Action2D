using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Wall,
    Load,

    StartPos,
    EndPos,

    Shop,
}

public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject cell;

    const int height = 20;
    const int width = 20;

    State[,] state = new State[width, height];

    private int m_x, m_y;

    // x が横軸, y が縦軸
    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                state[x, y] = State.Wall;
            }
        }

        StartPos();
        Select(m_x, m_y);

        SelectShop();
        EndPos();

        //MapCreate
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Create(x, y);
            }
        }
    }

    private void StartPos()
    {
        int x = Random.Range(3, height - 3);
        int y = Random.Range(3, width - 3);

        m_x = x;
        m_y = y;
        state[x, y] = State.StartPos;
    }

    private void Select(int i, int j)
    {
        bool check = true;
        while (check)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);

            if (x == 0 && y == 0 || x != 0 && y != 0)
            {
                continue;
            }
            check = false;

            // 2つ先を調べる。
            if (i + x * 2 < 0 || i + x * 2 >= width)
            {
                continue;
            }
            else if (j + y * 2 < 0 || j + y * 2 >= height)
            {
                continue;
            }

            if (state[i + x * 2, j + y * 2] == State.Wall)
            {
                if (state[i + x, j + y] != State.StartPos)
                {
                    state[i + x, j + y] = State.Load;
                }
                
                Select(i + x, j + y);
            }
            else if (state[i + x * 2, j + y * 2] != State.Wall)
            {
                Select(i + x, j + y);
            }
        }
    }

    private void SelectShop()
    {
        int x = Random.Range(1, height - 1);
        int y = Random.Range(1, width - 1);

        if (state[x, y] == State.Load)
        {
            state[x, y] = State.Shop;
            return;
        }

        SelectShop();
    }

    private void EndPos()
    {
        int x = Random.Range(1, height - 1);
        int y = Random.Range(1, width - 1);

        if (state[x, y] == State.Load)
        {
            state[x, y] = State.EndPos;
            return;
        }
        EndPos();
    }

    // Wall = 黒, Load = 白, Start = 青, Goal = 緑, Shop = 赤,
    private void Create(int i, int j)
    {
        if (state[i, j] == State.Wall)
        {
            cell.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else if (state[i, j] == State.Load)
        {
            cell.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (state[i, j] == State.Shop)
        {
            cell.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (state[i, j] == State.StartPos)
        {
            cell.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (state[i, j] == State.EndPos)
        {
            cell.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }


        Instantiate(cell, new Vector2(i - height / 2, j - width / 2), Quaternion.identity);
    }
}
