using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public void Init()
    {
        //��θ� �׻� �����ؾ���
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/StatData");
    }
}
