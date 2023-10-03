using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip _sound;
    private AudioSource _audioSource;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_audioSource == null)
        _audioSource =  GameObject.FindWithTag("Sfx").GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_sound);
    }
}
