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
        {
            Managers.Resources.Destroy(child.gameObject);
        }
        //�����δ� ���� �κ��丮 ������ �����ؼ� ������ ������ ������ �׽�Ʈ�ϱ� �׳� ����� ������
        for (int i = 0; i < 12; i++)
        {
            GameObject item = Managers.Resources.Instantiate("UI/Scene/UI_Inven_Item");
            item.transform.SetParent(gridPanel.transform);

            //GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>()

            //Util.GetOrAddComponent<UI_Inven_Item>(item);
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo($"�׽�Ʈ{i}��");
        }
    }
}
