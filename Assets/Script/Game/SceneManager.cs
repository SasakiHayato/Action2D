using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public void OnLoadScene(string setName)
    {
        switch (setName)
        {
            case "Dungeon":
                GameManager.getInstance().SetDungeonBool(true);
                if (GameManager.getInstance().GetDungeonCount() == 3) UnityEngine.SceneManagement.SceneManager.LoadScene("BossRoom");
                else
                {
                    int a = GameManager.getInstance().SetDungeonCount(1);
                    Debug.Log(GameManager.getInstance().GetDungeonCount());
                    UnityEngine.SceneManagement.SceneManager.LoadScene(setName);
                }
                break;
            case "Midway":
                GameManager.getInstance().SetDungeonBool(false);
                UnityEngine.SceneManagement.SceneManager.LoadScene(setName);
                break;
            case "Start":
                GameManager.getInstance().SetDungeonBool(false);
                UnityEngine.SceneManagement.SceneManager.LoadScene(setName);
                break;
        }
    }
}
