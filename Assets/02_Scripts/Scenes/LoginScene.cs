using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //�׳� ���� �ε��ϴ���
            //SceneManager.LoadScene("Game");
            //�Ŵ����� �������༭ �ε����ֱ� ����
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
    public override void Clear()
    {
        Debug.Log("�α��� ����");
    }
}
