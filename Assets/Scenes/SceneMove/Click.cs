using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Scene2", LoadSceneMode.Single);
    }
}
