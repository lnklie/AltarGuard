using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonManager<SoundManager>
{
    AudioSource[] audioSources = new AudioSource[4];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        GenerateSound();
        SoundPlay();
    }
    public void GenerateSound()
    {
        // ���� ����
        GameObject root = GameObject.Find("Sound");
        if (root == null)
        {
            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(ESound));

            for (int i = 0; i < soundNames.Length; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            audioSources[0].clip = Resources.Load<AudioClip>("Sounds\\BackGround\\WorldMap");
            audioSources[(int)ESound.BackGround].loop = true;
        }
    }

    public void Clear()
    {
        // ���� �ʱ�ȭ
        for (int i = 1; i < audioSources.Length; i++)
        {
            audioSources[i].Stop();
            audioSources[i].clip = null;
        }

        audioClips.Clear();
    }

    public void SoundStop(ESound _soundType)
    {
        // ���� ����
        audioSources[(int)_soundType].Stop();
    }

    public void SetSoundOption(ESound _soundType, float _value)
    {
        if(audioSources[(int)_soundType])
        {
            // ���� �ɼ� ����
            audioSources[(int)_soundType].volume = _value;
            Debug.Log("���� ������ " + audioSources[(int)_soundType].volume);
        }
        else
            Debug.Log("����� ����");
    }

    public void SoundPlay(AudioClip _audioClip, ESound _soundType = ESound.Effect, float _pitch = 1.0f)
    {
        // Ŭ������ �ҷ��� ���� ����
        if (_audioClip == null)
            return;
        if (_soundType == ESound.BackGround)
        {
            AudioSource audioSource = audioSources[(int)ESound.BackGround];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = _pitch;
            audioSource.clip = _audioClip;
            audioSource.Play();
        }
        //else if (_soundType == Sound.EFFECTGROUND)
        //{
        //    AudioSource audioSource = audioSources[(int)Sound.EFFECTGROUND];
        //    if (audioSource.isPlaying)
        //        audioSource.Stop();

        //    audioSource.pitch = _pitch;
        //    audioSource.clip = _audioClip;
        //    audioSource.Play();
        //}
        else
        {
            AudioSource audioSource = audioSources[(int)ESound.Effect];
            audioSource.pitch = _pitch;
            audioSource.PlayOneShot(_audioClip);
        }
    }
    public void SoundPlay(string _path, ESound _soundType = ESound.Effect, float _pitch = 1.0f)
    {
        // ��η� ���� ����
        AudioClip audioClip = GetOrAddAudioClip(_path, _soundType);
        SoundPlay(audioClip, _soundType, _pitch);
    }

    public void SoundPlay(ESound _soundType = ESound.BackGround, float _pitch = 1.0f)
    {
        audioSources[0].Play();
    }

    private AudioClip GetOrAddAudioClip(string _path, ESound _soundType = ESound.Effect)
    {
        // ���� �߰� �Ǵ� ��������
        if (_path.Contains($"Sounds/ + {_soundType}") == false)
        {
            _path = $"Sounds/{_soundType}/{_path}";
        }

        AudioClip audioClip = null;

        if (_soundType == ESound.BackGround)
        {
            audioClip = Resources.Load<AudioClip>(_path);
        }
        //else if (_soundType == Sound.EFFECTGROUND)
        //{
        //    audioClip = Resources.Load<AudioClip>(_path);
        //}
        else
        {
            if (audioClips.TryGetValue(_path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(_path);
                audioClips.Add(_path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {_path}");

        return audioClip;
    }
}
