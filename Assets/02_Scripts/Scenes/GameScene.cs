using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //GameScene가 꺼져 있어도 작동 시키기 위해 Awake로 변경
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;

    }

    public override void Clear()
    {

    }
}
