using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    // ���� ���� ã�� ��ȯ�ϴ� ������Ƽ
    //���� ���� �ִ� BaseScene ��ü�� ã��
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    // ���� �ε��ϴ� �޼���
    // Define.Scene Ÿ���� �޾� �׿� �ش��ϴ� ���� �ε��մϴ�.
    public void LoadScene(Define.Scene type)
    {
        // ���� ���� ������ �����ϴ� �޼��� ȣ��
        Managers.Clear();
        // ���� ���� �ε�
        SceneManager.LoadScene(GetSceneName(type));
    }

    // Define.Scene Ÿ���� �޾� �׿� �ش��ϴ� �� �̸��� ��ȯ�ϴ� �޼���
    // Enum.GetName�� ����Ͽ� Enum ���� ���ڿ��� ��ȯ
    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }

}
