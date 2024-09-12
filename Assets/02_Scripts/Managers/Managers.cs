using UnityEngine;

public class Managers : MonoBehaviour
{
    //������ �Ŵ����� ����´�. // ������Ƽ // �б� ���� // ���м� // �̱��� ������Ƽ
    //�Ŵ����� �����ϴ� �Ŵ���
    //�̱����� �����ϰ� ������Ƽ���Ͽ� ��� �ϱ� ���� private�� �������
    static Managers s_instance;//���ϼ��� ����ȴ�.
    public static Managers Instance { get { Init(); return s_instance; } }//�б� ���� ���� �ҷ���
    //���� �Ŵ������� �ڽ��� ������ �����ϱ� ���� �ٸ� �Ŵ������� ����(�̱����� ����ϰ�)���ִ� ġ���Ŵ������ �����ϸ� �ȴ�.
    //�׷��� ���� �ܺο��� ���� ���ʿ䰡 ��� private���� �������.
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    PoolManager _pool = new PoolManager();
    DataManager _data = new DataManager();
    //�÷��̾� ��ǲ �Ŵ���
    public static InputManager Input { get { return Instance._input; } }
    //���ҽ� �Ŵ���
    public static ResourceManager Resources { get { return Instance._resource; } }
    //������ �Ŵ���
    public static UIManager UI { get { return Instance._ui; } }
    //�� �Ŵ���
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    //���� �Ŵ���
    public static SoundManager Sound { get { return Instance._sound; } }
    //Ǯ �Ŵ���
    public static PoolManager Pool { get { return Instance._pool; } }
    //����Ÿ �Ŵ���
    public static DataManager Data { get { return Instance._data; } }
    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();//��ǲ�Ŵ����� OnUpdate()����
    }

    static void Init()
    {
        //���̶�� 
        if (s_instance == null)
        {
            //�ϴ� @Managers�� ã��
            GameObject go = GameObject.Find("@Managers");
            //�ٵ� �� @Managers�� ������
            if (go == null)//go �� ������
            {
                //@Managers�� �ڵ�������ؼ� ����� ������Ʈ�� ������ְ�
                go = new GameObject { name = "@Managers" };
                //@Managers���ٰ� �� ��ũ��Ʈ�� �߰��ؼ� ��������
                go.AddComponent<Managers>();
            }
            //������� �� Managers�� �ı��Ǹ� �ȵǱ⶧���� �ı������ʰ� ����
            DontDestroyOnLoad(go);
            //s_instance��� ������Ƽ�� ����ϱ� ���� ����ӿ�����Ʈ�� ������ְ� ��ũ��Ʈ�� �߰����� �� ������Ʈ��
            //Managers������Ʈ�� ��������
            s_instance = go.GetComponent<Managers>();
            //���������Ʈ�� ���������ִ°��� ���� ������
            s_instance._sound.Init();
            //������Ʈ Ǯ���ϴ� �Լ��� ����
            s_instance._pool.Init();
            //�뷮�� ��û ũ�ٰ� ���� ���� ���� �׻� �������� �״� ������ �Ƚ�����
            s_instance._data.Init();
        }
    }

    //���� �̵��� �� ���� Clear()�� ȣ���ϸ� ������ �ٸ��͵� Ŭ���� ���ٰ� ������ �Լ��� �������
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();

        //������Ŭ����
        Pool.Clear();
    }
}
