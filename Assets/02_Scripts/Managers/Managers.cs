using System.Collections;
using System.Collections.Generic;
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
    //�÷��̾� ��ǲ �Ŵ��� �Ҵ�
    public static InputManager Input { get { return Instance._input; } }
    //���ҽ� �Ŵ��� �Ҵ�
    public static ResourceManager Resources { get { return Instance._resource; } }
    //������ �Ŵ��� �Ҵ�
    public static UIManager UI { get { return Instance._ui; } }
    //�� �Ŵ��� �Ҵ�
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    //���� �Ŵ��� �Ҵ�
    public static SoundManager Sound { get { return Instance._sound; } }
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
        }
    }

    //���� �̵��� �� ���� Clear()�� ȣ���ϸ� ������ �ٸ��͵� Ŭ���� ���ٰ� ������ �Լ��� �������
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();
    }
}
