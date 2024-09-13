using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    //���� �� ���´� Unknown���� ����
    //Define.Scene _sceneType = Define.Scene.Unknown;
    //�ٸ������� ����ϱ� ���� ������Ƽ�� ����
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

    protected virtual void Init()
    {
        //Ÿ������ ������Ʈ�� ã�ƺ���
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        //ã�� ������Ʈ�� ������ 
        //��� Resoureces -> Prefabs -> UI -> EventSystem �� ������Ű�� �̸��� @EventSystem���� ����
        if (obj == null)
        {
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    }

    //���⼭ ���� ������ ���� ���� �Ŷ� abstract��
    public abstract void Clear();
}
