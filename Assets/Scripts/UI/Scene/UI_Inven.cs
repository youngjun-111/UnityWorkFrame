using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        //���� ������Ʈ Ÿ���� ���ε�
        Bind<GameObject>(typeof(GameObjects));

        //������ �׽�Ʈ�� ���� �־�ξ��� ������ �����Ѵ�.
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        //�����δ� ���� �κ��丮 ������ �����ؼ� ������ ������ ������ �׽�Ʈ�ϱ� �׳� ����� ������
        for (int i = 0; i < 8; i++)
        {

        }
    }
}
