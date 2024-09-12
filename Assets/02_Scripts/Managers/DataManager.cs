using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//메모리에서 들고 있는 거를 파일로 변환 할 수 있는 공식
[Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public class StatData
{
    public List<Stat> stats = new List<Stat>();
}

public class DataManager
{
    public void Init()
    {
        //경로를 항상 생각해야함
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/StatData");
        StatData data = JsonUtility.FromJson<StatData>(textAsset.text);
    }
}
