using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{ 
   None,

   Wall,
   Load,
   Start,
}

public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject StartPrefab;
    [SerializeField] private GameObject LoadPrefab;
    [SerializeField] private GameObject NonePrefab;
    /// <summary>
    /// マップ全体の広さ
    /// </summary>
    const int mapHeight = 10;
    const int mapWidth = 10;

    private MapState[,] mapArray = new MapState[mapHeight, mapWidth];
    void Start()
    {
        // Map_Reset
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                mapArray[i, j] = MapState.None;
                Instantiate(NonePrefab, new Vector2(j - mapWidth / 2, i - mapHeight / 2), Quaternion.identity);
            }
        }
        // StartPosition
        int horizontal = Random.Range(1, mapHeight - 1);
        int vertical = Random.Range(1, mapWidth - 1);

        StartPositionSet(horizontal, vertical);
    }

    void StartPositionSet(int horizontal, int verticl)
    {
        mapArray[horizontal, verticl] = MapState.Start;

        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                if (mapArray[horizontal, verticl] == mapArray[i, j])
                {
                    Vector2 vector = new Vector2(j - mapWidth / 2, i - mapHeight / 2);
                    
                    CreateLoad(vector, horizontal, verticl);
                    SelectLoad(vector, horizontal, verticl);
                }
            }
        }
    }

    private void CreateLoad(Vector2 vector, int horizontal, int verticl)
    {
        if (mapArray[horizontal, verticl] == MapState.Load)
        {
            Instantiate(LoadPrefab, vector, Quaternion.identity);
        }
        if (mapArray[horizontal, verticl] == MapState.Start)
        {
            Instantiate(StartPrefab, vector, Quaternion.identity);
        }
    }

    private bool check = true;
    void SelectLoad(Vector2 vec, int horizontal, int verticl)
    {
        while (check)
        {
            int x = Random.Range(-1, 2);
            int y = Random.Range(-1, 2);

            if (x == 0 && y == 0 || x != 0 && y != 0)
            {
                Debug.Log("a");
                continue;
            }
            check = false;
            
            x *= 2;
            y *= 2;

            if (mapArray[x + horizontal, y + verticl] == MapState.None)
            {
                x /= 2;
                y /= 2;
                
                Vector2 vector = new Vector2(x, y);
                vec = vec - vector;
                
                CreateLoad(vec, horizontal - x, verticl - y);
            }
        }
    }
}
