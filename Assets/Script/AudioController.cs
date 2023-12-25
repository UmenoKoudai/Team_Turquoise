using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _seAudio;
    [SerializeField]
    private AudioSource _bgmAudio;

    [SerializeField]
    SeClass[] _seClass;

    [SerializeField]
    BgmClass[] _bgmClass;

    static AudioController _instance;
    public static AudioController Instance
    {
        get
        {
            if(!_instance)
            {
                _instance = FindObjectOfType<AudioController>();
                if(!_instance)
                {
                    Debug.LogError("AudioController���A�^�b�`�����I�u�W�F�N�g�����݂��܂���B");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else if(_instance == this)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }


    public void SePlay(SeClass.SE se)
    {
        SeClass data = null;
        foreach(var playSe in _seClass)
        {
            if (playSe.SeState != se) continue;
            data = playSe;
            break;
        }
        _seAudio?.PlayOneShot(data?.SeClip);
    }

    public void BgmPlay(BgmClass.BGM bgm)
    {
        BgmClass data = null;
        foreach (var playSe in _bgmClass)
        {
            if (playSe.BgmState != bgm) continue;
            data = playSe;
            break;
        }
        _bgmAudio.clip = data?.BgmClip;
        _bgmAudio.Play();
    }

    [Serializable]
    public class BgmClass
    {
        [SerializeField]
        AudioClip _bgmClip;
        public AudioClip BgmClip => _bgmClip;

        [SerializeField]
        BGM _bgmState;
        public BGM BgmState => _bgmState;

        public enum BGM
        {
            Title,
            Basic,
            B1F,
            Ending,
            GameOver
        }
    }


    [Serializable]
    public class SeClass
    {
        [SerializeField]
        AudioClip _seClip;
        public AudioClip SeClip => _seClip;

        [SerializeField]
        SE _seState;
        public SE SeState => _seState;

        public enum SE
        {
            Click,
            DoorOpen,
            EnemyWalk1,
            EnemyWalk2,
            EnemyDiscover,
            AstralChange,
            Walk,
            EnemyDiscovery,
            Breaker,
            Switch,
        }
    }
}




