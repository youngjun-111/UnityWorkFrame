using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
                            // T FindChild<T>
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform != null)
        {
            return transform.gameObject;
        }
        return null;
    }

    // �ֻ��� �θ�, �̸��� ������ �ʰ� �� Ÿ�Կ��� �ش��ϸ� ���� ( ���۳�Ʈ �̸� ),
    // ��������� ���, �ڽĸ� ã������ �ڽ��� �ڽĵ� ã�� ����
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false)
    where T : UnityEngine.Object
    {
        //�ֻ��� ��ü�� null�� ���
        if (go == null)
            return null;

        if (recursive == false) // ���� �ڽĸ�
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                //����ְų� �̸��� ������ ��, �̸��� Ʋ������ �ʴ´ٸ�
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    //�� ���� �ڽ���  ������Ʈ�� ������
                    T component = transform.GetComponent<T>();
                    //�� ������Ʈ���� ���� �ƴϸ�
                    if (component != null)
                        //�� ������Ʈ�� ��ȯ����
                        return component;
                }
            }
        }
        else// �ڽ��� �ڽı���
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {   // �̸��� ����ְų� ���� ã������ �̸��� ���ٸ�
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component; 
                }
            }
        }
        return null;
    }

    // ���ӿ�����Ʈ(go)�� �ش� ������Ʈ�� ������ T ������Ʈ �߰�
    //�������� ���� �ʿ䰡 �������� ������Ʈ�� ������ �ٿ��� �ʿ䰡 ��������.
    //��, Ÿ���� ������Ʈ���
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        //�� ��ü�� ������Ʈ�� ��������
        T component = go.GetComponent<T>();
        //�ٵ� �������� ������Ʈ�� ������
        if (component == null)
        {
            //�� ������Ʈ�� �ٿ���
            component = go.AddComponent<T>();
        }
        //�ƴϸ� �׳� ������Ʈ�� ��ȯ
        return component;
    }
}
