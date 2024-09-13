using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    [SerializeField]
    Text _text;

    int _score = 0;

    //����� UI�̸��� enum���� ����(�ʿ� �� �߰�)
    //��ư�� ��ưenum���� ���� �ؽ�Ʈ�� �ؽ�Ʈenum���� ����
    enum Buttons
    {
        PointButton,
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum Images
    {
        Image,
    }
    enum GameObjects
    {
        TestObject,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        //�Ȱ��� Bind�ؼ� Ÿ������ ������Ʈ�� ã����
        Bind<GameObject>(typeof(GameObjects));

        //�̺κ��� UI_Base���� �Լ����·� ����� �ش�.
        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        //evt.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });

        //�̰Ŵ� Ŭ�� �� ����� �Լ�
        //��, ��ư ���� ��
        GetButton((int)Buttons.PointButton).gameObject.BindUIEvent(OnButtonClicked);

        //�̰Ŵ� �巡�� �Ͽ� �̹����� �ű���� �ϴ� �Լ�
        //��, �̹��� Ŭ���Ͽ� �ű� ��
        GameObject go = GetImage((int)Images.Image).gameObject;
        BindUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"���� : {_score}";
    }

    //UI_Base�� �Ű����͵��� ���������
    //������Ʈ�� ������Ʈ�� �ƴϱ⿡ ���� ������� �Լ�
    //test�ϻ� ���� ������� �ʴ´� UI���� Ư���ϰ� GameObject�� ����ϴ� ���� ������� �����̴�.
    //public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    //{
    //    Transform transform = FindChild<Transform>(go, name, recursive);
    //    if (transform == null)
    //    {
    //        return null;
    //    }
    //    return transform.gameObject;
    //}

    ////������Ʈ�� �������� �Լ� ����(UI_Base�� �̵�)
    //Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();//Type�� using System�ʿ�
    //void Bind<T>(Type type) where T : UnityEngine.Object
    //{
    //    string[] names = Enum.GetNames(type);

    //    UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
    //    _objects.Add(typeof(T), objects);

    //    for (int i = 0; i < names.Length; i++)
    //    {
    //        //�Ʒ� Util���� ���� �Լ��� �̿��� ó��
    //        if (typeof(T) == typeof(GameObject))
    //        {
    //            objects[i] = Util.FindChild(gameObject, names[i], true);
    //        }
    //        else
    //        {
    //            objects[i] = Util.FindChild<T>(gameObject, names[i], true);
    //        }
    //        //��ã���ְ� �ִ��� üũ
    //        if (objects[i] == null)
    //        {
    //            Debug.Log($"Failed to bind({names[i]})");
    //        }
    //    }
    //}

    //T Get<T>(int idx) where T : UnityEngine.Object
    //{
    //    UnityEngine.Object[] objects = null;

    //    //���� ������ �׳� ����(Ű�� ���ԵǾ� �ִ��� Ȯ��)
    //    if (_objects.TryGetValue(typeof(T), out objects) == false)
    //        return null;

    //    //�������� �ε��� ��ȣ�� ������ ������ T�� ĳ���� ����
    //    return objects[idx] as T;
    //}

    ////�������� �Ѱ� �� �����ؼ� �߰��ܰ踦 ������ �뵵
    ////��, ���� ����ϴ� �͵��� Get<T>�� �̿����� �ʰ� �ٷ� ����� �� �ְ� ����� �� ��.
    //Text GetText(int idx) { return Get<Text>(idx); }
    //Button GetButton(int idx) { return Get<Button>(idx); }
    //Image GetImage(int idx) { return Get<Image>(idx); }
}
