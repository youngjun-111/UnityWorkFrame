using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //������ �Ŵ����� ����´�. // ������Ƽ // �б� ���� // ���м� // �̱��� ������Ƽ
    //�Ŵ����� �����ϴ� �Ŵ���
    //�̱����� �����ϰ� ������Ƽ���Ͽ� ��� �ϱ� ���� private�� �������
    static Managers s_instance;//���ϼ��� ����ȴ�.
    public static Managers instance { get { Init(); return s_instance; } }//�б� ���� ���� �ҷ���
    //���� �Ŵ������� �ڽ��� ������ �����ϱ� ���� �ٸ� �Ŵ������� ����(�̱����� ����ϰ�)���ִ� ġ���Ŵ������ �����ϸ� �ȴ�.
    //�׷��� ���� �ܺο��� ���� ���ʿ䰡 ��� private���� �������.
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return instance._input; } }
    public static ResourceManager Resource { get { return instance._resource; } }
    public static UIManager UI { get { return instance._ui; } }
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
        }
    }
}
