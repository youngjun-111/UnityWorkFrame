using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //����
    public T Load<T>(string path) where T : Object
    {
        //���࿡ �������� ��쿡 ���������� Ǯ� �ѹ��� ã�Ƽ�
        //�װ��� �ٷ� ��ȯ
        //���� Ÿ���� ���ӿ��������
        if(typeof(T) == typeof(GameObject))
        {
            //���� �ε忡 Load<GameObject>($"Prefabs/{path}");�̷�������
            //��ü ��θ� �Ѱ��־��µ� Ǯ�� �׳� �������� �̸��� ����ϰ� ������
            //((name)���� �Ǿ� ������ (name)�� ����ؾ� �ϴϱ�
            string name = path;
            int index = name.LastIndexOf('/');
            if(index >= 0)
            {
                name = name.Substring(index + 1);//�̷��� ('/')���� ���Ͱ� �ǰ���
            }

            //(name)�� ã�� �ôµ� ������ �̰��� ��ȯ�� �ָ� �Ǵµ�..
            GameObject go = Managers.Pool.GetOriginal(name);
            //�׷��� ������ ���� ó�� �׳� return Resources.Load<T>(path);�� �ǰ�
            if(go != null)
            {
                return go as T;
            }
        }
        return Resources.Load<T>(path);
    }


    public GameObject Instantiate(string path, Transform parent = null)
    {
        //��θ� �������ָ� ���������� ��밡����
        //Resources.Load<T>(path);�� ����Ұ�� �ֻ��� ���� ���� ������ Resources������
        //��, ��ΰ� Resources ���� -> Prefabs ���� �ȿ� �ִ� ObjectPrefab�̶� ��.
        //����ȭ �ҽ� Prefabs�� ���� ������. �̹� ResourceManager���� ������ �༭
        //1. ���������� ������ �ٷ� ��� ������ �Ʒ�ó�� �����
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");

        if (prefab == null)
        {
            //��θ� ã�� ���߰ų�, Prefab�� ��ã�Ƽ� ������Ų�� ������ ������ ��� ��
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. Ȥ�� Ǯ���� ������Ʈ�� ������ �װ��� ��ȯ
        GameObject go = Object.Instantiate(prefab, parent);
        //"(Clone)"���ڿ��� ã�Ƽ� �ε����� ��ȯ
        int index = go.name.IndexOf("(Clone)");
        if(index > 0)
        {
            go.name = go.name.Substring(0, index);
        }
        return go;
    }

    //�����غ����� �� �����δ� �ʿ����.
    //3. ���࿡ Ǯ���� �ʿ��� ������Ʈ��� �ٷ� �����ϴ� ���� �ƴ϶� Ǯ�� �Ŵ������� ��Ź
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go);
    }
}
