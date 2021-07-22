using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void LoadD()
    {
        SceneManager.LoadScene("Dungeon");
    }
    public void LoadG()
    {
        SceneManager.LoadScene("Midway");
    }
}
