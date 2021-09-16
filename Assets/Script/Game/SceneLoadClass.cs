using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadClass : MonoBehaviour
{
    public void OnLoadScene(string setName) => Load(setName);

    void Load(string name)
    {
        switch (name)
        {
            case "Dungeon":
                GameManager.Instance.SetDungeonBool(true);
                if (GameManager.Instance.GetDungeonCount() == 3) SceneManager.LoadScene("BossRoom");
                else
                {
                    int a = GameManager.Instance.SetDungeonCount(1);
                    Debug.Log(GameManager.Instance.GetDungeonCount());
                    SceneManager.LoadScene(name);
                }
                break;
            case "Midway":
                GameManager.Instance.SetDungeonBool(false);
                SceneManager.LoadScene(name);
                break;
            case "Start":
                GameManager.Instance.SetDungeonBool(false);
                SceneManager.LoadScene(name);
                break;
        }
    }
}
