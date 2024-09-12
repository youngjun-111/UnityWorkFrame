using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //GameScene�� ���� �־ �۵� ��Ű�� ���� Awake�� ����
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Stat> Dict = Managers.Data.StatDict;
        //Temp_����Ƽ» 2�� ����
        //for (int i = 0; i < 2; i++)
        //{
        //    Managers.Resources.Instantiate("unitychan");
        //}
    }

    public override void Clear()
    {

    }
}
