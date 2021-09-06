using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusItemDataBase : ScriptableObject
{
    [SerializeField] List<StatusItem> m_items = new List<StatusItem>(); 
}

[System.Serializable]
public class StatusItem
{

}
