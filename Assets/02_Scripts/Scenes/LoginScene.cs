using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;
        //Temp_����Ƽ¯ 5�� ���� �ϰ� 5�� ����
        //List<GameObject> list = new List<GameObject>();
        //for (int i = 0; i < 5; i++)
        //{
        //    list.Add(Managers.Resources.Instantiate("unitychan"));
        //}

        //foreach(GameObject obj in list)
        //{
        //    Managers.Resources.Destroy(obj);
        //}
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
