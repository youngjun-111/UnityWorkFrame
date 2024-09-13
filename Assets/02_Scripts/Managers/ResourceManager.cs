using UnityEngine;

public class ResourceManager
{
    //����� ������ ���ڸ�
    //1. ���������� ������ �ٷ� ����ϰ�
    //2. Ȥ�� Ǯ���� ������Ʈ�� ������ Managers.Pool.Pop(original, parent).gameObject;�� ��ȯ
    //3. ���࿡ Ǯ���� �ʿ��� ������Ʈ��� �ٷ� �����ϴ� ���� �ƴ϶� Ǯ�� �Ŵ������� ��Ź
    //����
    public T Load<T>(string path) where T : Object
    {
        //1. original ������ �ٷ� ��� GameObject original = Load<GameObject>($"Prefabs/{path}"); �̰� �ٷ� ���
        //���࿡ �������� ��쿡 ���������� Ǯ� �ѹ��� ã�Ƽ�
        //�װ��� �ٷ� ��ȯ
        //���� Ÿ���� ���ӿ��������
        if (typeof(T) == typeof(GameObject))
        {
            //���� �ε忡 Load<GameObject>($"Prefabs/{path}");�̷�������
            //��ü ��θ� �Ѱ��־��µ� Ǯ�� �׳� �������� �̸��� ����ϰ� ������
            //((name)���� �Ǿ� ������ (name)�� ����ؾ� �ϴϱ�
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            {
                name = name.Substring(index + 1);//�̷��� ('/')���� ���Ͱ� �ǰ���
            }
            GameObject go = Managers.Pool.GetOriginal(name);
            //�׷��� ������ ���� ó�� �׳� return Resources.Load<T>(path);�� �ǰ�
            //(name)�� ã�� �ôµ� ������ �̰��� ��ȯ�� �ָ� �Ǵµ�..
            if (go != null)
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
        //�ǹ̻� ȥ���� ���� �� �־ �̸��� ���� ������
        //GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        GameObject original = Load<GameObject>($"Prefabs/{path}");

        if (original == null)
        {
            //��θ� ã�� ���߰ų�, Prefab�� ��ã�Ƽ� ������Ų�� ������ ������ ��� ��
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        //2. Ȥ�� Ǯ���� ������Ʈ�� ������ Managers.Pool.Pop(original, parent).gameObject;�� ��ȯ
        if (original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);
        //"(Clone)"���ڿ��� ã�Ƽ� �ε����� ��ȯ
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
        {
            go.name = go.name.Substring(0, index);
        }
        return go;
    }

    //�����غ����� �� �����δ� �ʿ����.
    //3. ���࿡ Ǯ���� �ʿ��� ������Ʈ��� �ٷ� �����ϴ� ���� �ƴ϶� Ǯ�� �Ŵ������� ��Ź
    //������Ʈ Ǯ������ ���� ��Ű�°Ծƴ϶� ��Ȱ��ȭ �����ִ� ����ΰ� �̴�.
    //Ǯ���� �Ǿ��ְ� Poolable��ũ��Ʈ�� �پ��ִ� �ֵ��� ��Ȱ��ȭ �ٵ� ������ ������ ����
    //Ǯ�� �ִ� Pool�� �ٿ��ְ� ��� ���ҰŸ� �׳� Instantiate�� Destroy�� ���
    public void Destroy(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }
        Object.Destroy(go);
    }
}
