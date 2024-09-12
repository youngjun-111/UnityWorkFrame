using System;
using System.Collections.Generic;

#region Stat
//�޸𸮿��� ��� �ִ� �Ÿ� ���Ϸ� ��ȯ �� �� �ִ� ����
[Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();
    //��ȯ �ϴ� �۾��� ���⼭ �ҷ���
    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
        {
            dict.Add(stat.level, stat);
        }

        return dict;
    }
}
#endregion
