using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGame : MonoBehaviour
{
    public void SetReset() => GameManager.Instance.ReStart();
    public void SetSelectActive() => GameManager.Instance.SetSelectActive();
    public void SetScene(string set) => GameManager.Instance.SetScene(set);
    public void SetGameDiff(int set) => GameManager.Instance.SetGameDifficulty(set);
}
