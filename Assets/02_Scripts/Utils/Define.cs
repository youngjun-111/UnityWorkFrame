using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum CameraMode
    {
        //ī�޶� ��尡 �������� ��� ���⼭ �߰������� �߰����ָ� �ȴ�.
        QuarterView, testView,
    }

    public enum MouseEvent
    {
        Press, Click,
    }

    public enum UIEvent
    {
        //���� ����
        Click, Drag,
    }

    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,//���� ������ ������ �������ؼ� �߰�
    }
}
