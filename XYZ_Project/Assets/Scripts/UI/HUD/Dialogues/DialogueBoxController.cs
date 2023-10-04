using System;
using System.Collections;
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
        
        private const string IsOpen = "IsOpen";

        private AudioSource _sfxSource;

        private Coroutine _typingCoroutine;

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
            _animator.SetBool(IsOpen, true);
        }

        private void OnStartDialogueAnimation()
        {
            _typingCoroutine = StartCoroutine(TypeDialogueText());
        }

        private IEnumerator TypeDialogueText()
        {
            _text.text = string.Empty;
            var sentence = _data.Sentences[_currentSentence];
            foreach (var letter in sentence)
            {
                _text.text += letter;
                _sfxSource.PlayOneShot(_typingSound);
                yield return new WaitForSeconds(_textSpeed);
            }

            _typingCoroutine = null;
        }

        private void OnCloseAniamtionComplete()
        {
            
        }

        public void OnSkip()
        {
            if (_typingCoroutine == null) return;
            StopTypeAnimation();
            _text.text = _data.Sentences[_currentSentence];
        }

        private void StopTypeAnimation()
        {
            if(_typingCoroutine != null)
                StopCoroutine(_typingCoroutine);
            _typingCoroutine = null;
        }

        public void OnContinue()
        {
           StopTypeAnimation();
           _currentSentence++;
           var isDialogueComplete = _currentSentence >= _data.Sentences.Length;
           if (isDialogueComplete)
           {
               HideDialogueBox();
           }
           else
           {
               OnStartDialogueAnimation();
           }
        }

        private void HideDialogueBox()
        {
            _animator.SetBool(IsOpen, false);
            _sfxSource.PlayOneShot(_close);
        }



    }
