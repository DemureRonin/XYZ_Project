using System;
using Model.Data;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.UI;


    public class DialogueBoxController : MonoBehaviour
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _container;
        [SerializeField] private Animator _animator;
        
        [Space] [SerializeField] private float _textSpeed = 0.09f;

        [Header("Sounds")] [SerializeField] private AudioClip _typingSound;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioClip _close;
        private DialogueData _data;
        private int _currentSentence;

        private AudioSource _sfxSource;

        private void Start()
        {
            _sfxSource = AudioUtils.FindSfxSource();
        }

        public void ShowDialogue(DialogueData dialogueData)
        {
            _data = dialogueData;
            _currentSentence = 0;
            _text.text = string.Empty;
            
            _container.SetActive(true);
            _sfxSource.PlayOneShot(_open);
        }

    }
