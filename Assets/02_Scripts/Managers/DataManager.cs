using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void Init()
    {
        //경로를 항상 생각해야함
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/StatData");
    }
}
