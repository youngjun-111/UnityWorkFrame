using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    //����� �ҽ��� BGM��, ����Ʈ�뵵�� �ϳ��� �ΰ��� ����
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClip = new Dictionary<string, AudioClip>();

    //���带 ��������� ������Ʈ�� ����������
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");

        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }

        _audioClip.Clear();
    }

    //Ÿ��, ���, ����ӵ��� �ϴ� ��������
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path);
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing{path}");
            return;
        }
    }

    //public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    //{
    //    //�Լ��� �������� �������� �и� �� ���� �ݺ��� �ڵ�� �ϳ��� �������� �ٸ� ������ ȣ�� �ϵ���
    //    if (path.Contains("Sounds/") == false)
    //    {
    //        path = $"Sounds/{path}";
    //    }
    //    if (type == Define.Sound.Bgm)
    //    {
    //        AudioClip audioClip = Managers.Resources.Load<AudioClip>(path);
    //        if (audioClip == null)
    //        {
    //            Debug.Log($"AudioClip Missing{path}");
    //            return;
    //        }

    //        //AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
    //        ////�̹� �������� Bgm�� �ִٸ� ����
    //        //if (audioSource.isPlaying) { audioSource.Stop(); }

    //        //audioSource.pitch = pitch;
    //        //audioSource.clip = audioClip;
    //        //audioSource.Play();
    //    }
    //    else
    //    {
    //        AudioClip audioClip = GetOrAddAudioClip(path);
    //        if (audioClip == null)
    //        {
    //            Debug.Log($"AudioClip Missing{path}");
    //            return;
    //        }
    //        //AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
    //        //audioSource.pitch = pitch;
    //        //audioSource.PlayOneShot(audioClip);
    //    }
    //}

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
        {
            return;
        }
        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying) { audioSource.Stop(); }
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }


    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        //�Լ��� �������� �������� �и� �� ���� �ݺ��� �ڵ�� �ϳ��� �������� �ٸ� ������ ȣ�� �ϵ���
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }
        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClip.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Managers.Resources.Load<AudioClip>(path);
                _audioClip.Add(path, audioClip);
            }
        }
        if (audioClip == null)
        {
            Debug.Log($"AudioClip Missing {path}");
        }
        //�������� �����ͼ� �ִ� ���� ���� �־����� �ִ����� ����
        return audioClip;
    }
}
