using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundsComponent : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioData[] _sounds;
    private AudioSource _audioSource;

    public void Play(string id)
    {
        foreach (var audioData in _sounds)
        {
            if (audioData.Id != id) continue;
            {
                if (_audioSource == null)
                    _audioSource = GameObject.FindWithTag("SfxAudioSource").GetComponent<AudioSource>();
                _source.PlayOneShot(audioData.Clip);
                break;
            }
        }
    }
    [Serializable]
    public class AudioData
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private string _id;
        public string Id => _id;
        public AudioClip Clip => _clip;
    }
}
