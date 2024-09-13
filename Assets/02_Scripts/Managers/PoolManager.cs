using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public Transform Root { get; set; }
        Stack<Poolable> _poolStack = new Stack<Poolable>();

        //���� ������
        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name} _Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        //Ǯ�� �����ϰ� ������Ʈ�� �ٿ��ִ� �Լ�(Clone)�̶�� �̸��� �����ϱ� ���� name�� ResourceManager����
        //Clone�� ���������� �������η� ��������
        Poolable Create()
        {
            GameObject go = Object.Instantiate<GameObject>(Original);
            //�̸���Clone�� �Ⱥٰ�
            go.name = Original.name;
            //������Ʈ�� ���̰� ��ȯ
            return go.GetOrAddComponent<Poolable>();
        }

        //Ǯ�� �־��ִ� �Լ�
        public void Push(Poolable poolable)
        {
            if (poolable == null)
            {
                return;
            }
            //�θ� �Ʊ� �������� @Pool_Root�� �ڽ����� ����
            poolable.transform.parent = Root;
            //StackPush�� 1���� ����
            poolable.gameObject.SetActive(false);
            //�������� Ǯ���� �ƴ��� �ȵƴ��� Ȯ��
            poolable.isUsing = false;

            _poolStack.Push(poolable);

        }

        //Ǯ�� ������ �̾ƿ��� �Լ�
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            //Stack�� ����� ������Ʈ�� 0���� Ŭ ��쿡 ������ �����̰�
            if (_poolStack.Count > 0)
            {
                poolable = _poolStack.Pop();
            }
            //�ٵ� ������ 1�� ���� ������
            else
            {
                poolable = Create();
            }
            //Ȱ��ȭ �����ְ�
            poolable.gameObject.SetActive(true);
            //����Ʈ���̿·ε忡�� ���ְ� ���� ������ ���� �����ִ� ���
            if (parent == null)
            {
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;
            }
            //�θ� �Ʊ� �������� @Pool_Root�� �ڽ����� ����
            poolable.transform.parent = parent;
            //�������� Ǯ���� �ƴ��� �ȵƴ��� Ȯ��
            poolable.isUsing = true;
            return poolable;
        }
    }
    #endregion

    #region Pool�� Dictionary�� ���
    //���� Ǯ���� ��Ʈ���̶�� Ű�� �̿��� �̸�(string)�� �̿��ؼ� ����
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    Transform _root;
    public void Init()
    {
        if (_root == null)
        {
            //Ǯ���� �� ������Ʈ�� �ִٸ� @Pool_Root ���Ͽ� ��� �ְ� �� �� ����
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }
    //���� ��� �ϵ���
    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
        {
            CreatePool(original);
        }
        return _pool[original.name].Pop(parent);
    }

    //���� ����� �ϵ��� class���� �����ڸ� ���� ��ųʸ��� ����������
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root;

        _pool.Add(original.name, pool);
    }

    public GameObject GetOriginal(string name)
    {
        //��� 1���� ���� �ϰ�, �����ߴ� �� 1���� �����ؼ� ���� ����ȭ ��Ű��?
        //�ٵ� �ܺ������� ���̴°� ���� ���̰� ����
        if (_pool.ContainsKey(name) == false)
        {
            return null;
        }

        return _pool[name].Original;
    }

    //���� Pop()�� �ѹ��� ���ϰ� Ǯ�� ���� ���¿��� Push�� �ϴ� ����
    //�����Ϳ��� �巡�׿� ������� ���� ���� ���� ��츦 ����ؼ�
    //���������� ó���� ����
    //�������� ��Ʈ�� ������ �ִ� ��Ȱ��ȭ ��Ű�µ�. ������������ ������ �ְ� �����ǰ� poolable�� �ֱ⵵�ϰ�
    //�׷��� ������ƮǮ������ ������ ���� ������ �ְ� �ƴϸ� �׾ִ� poolable�� �־ ���� ���ѹ�����
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;

        if (_pool.ContainsKey(name) == false)
        {
            //Ǯ�� ���� �ʰ� ����...
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }

    //Clear ���� ����� �� �� �Ἥ �־���� ���� ������� ���� �ƴϸ� �״�� �����Ǿ�� ���� ���Ӹ��� �ٸ�����
    //(������ ����ϴ� ������Ʈ�� �ʹ��� �ٸ��� ������ �����ִ� ����� �־�� �ϴ�)
    public void Clear()
    {
        foreach (Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }
        _pool.Clear();
    }
    #endregion
}
