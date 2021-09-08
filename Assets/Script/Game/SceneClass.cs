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
                GameManager.getInstance().SetDungeonBool(true);
                if (GameManager.getInstance().GetDungeonCount() == 3) SceneManager.LoadScene("BossRoom");
                else
                {
                    int a = GameManager.getInstance().SetDungeonCount(1);
                    Debug.Log(GameManager.getInstance().GetDungeonCount());
                    SceneManager.LoadScene(setName);
                }
                break;
            case "Midway":
                GameManager.getInstance().SetDungeonBool(false);
                SceneManager.LoadScene(setName);
                break;
            case "Start":
                GameManager.getInstance().SetDungeonBool(false);
                SceneManager.LoadScene(setName);
                break;
        }
    }
}
