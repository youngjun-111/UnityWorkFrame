using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    //��� �޴¾ָ� �����ϱ� ���� protected�� �������
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    //������Ʈ�� �������� �Լ� ���¸� ������.
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects); // Dictionary �� �߰�

        // T �� ���ϴ� ������Ʈ���� Dictionary�� Value�� objects �迭�� ���ҵ鿡 �ϳ��ϳ� �߰�
        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }

            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;

        //���� ������ �׳� ����
        if (_objects.TryGetValue(typeof(T), out objects) == false)
        {
            return null;
        }

        //�������� �ε��� ��ȣ�� ������ ������ T�� ĳ���� ����
        return objects[idx] as T;
    }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }

    //�̷� �����̸� �ɵ� ������ OnDragHandler �̰������� �ٸ��͵� ����ϰ� ���״ϱ� �̳� Ÿ������ ����� ��
    //go : Action�� �� �ִ� ��ũ��Ʈ�� �ִ� ���� ������Ʈ...
    //action : ������ ��ų �׼�
    //mode(�ϰ����)
    public static void BindUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);
        evt.OnDragHandler += ((PointerEventData data) => { evt.transform.position = data.position; });

        switch (type)
        {
            //Ÿ���� Ŭ���̶�� �巡�״� ���� ���
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            //Ÿ���� �巡�׶�� Ŭ���� ���� ���
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}


