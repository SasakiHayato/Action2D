using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneClass : MonoBehaviour
{
    public void OnLoadScene(string setName)
    {
        switch (setName)
        {
            case "Dungeon":
                if (GameManager.getInstance().GetDungeonCount() == 3) SceneManager.LoadScene("BossRoom");
                else
                {
                    GameManager.getInstance().SetDungeonCount();
                    SceneManager.LoadScene(setName);
                }
                break;
            case "Midway":
                SceneManager.LoadScene(setName);
                break;
            case "Start":
                SceneManager.LoadScene(setName);
                break;
        }
    }
}
