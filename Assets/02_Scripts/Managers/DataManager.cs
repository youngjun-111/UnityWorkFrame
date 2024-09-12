using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//�޸𸮿��� ��� �ִ� �Ÿ� ���Ϸ� ��ȯ �� �� �ִ� ����
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
        //��θ� �׻� �����ؾ���
        TextAsset textAsset = Managers.Resources.Load<TextAsset>($"Data/StatData");
        StatData data = JsonUtility.FromJson<StatData>(textAsset.text);
    }
}
