﻿using System;
using Model.Data;
using Scripts.Model.Definitions;
using UnityEditor;
using UnityEngine;

namespace Scripts.Dialogues
{
    public class ShowDialogueComponent : MonoBehaviour
    {
        [SerializeField] private Mode _mode;
        [SerializeField] private DialogueData _bound;
        [SerializeField] private DialogueDef _external;

        private DialogueBoxController _dialogueBox;
        public void Show()
        {
            if (_dialogueBox == null)
                _dialogueBox = FindObjectOfType<DialogueBoxController>();
            _dialogueBox.ShowDialogue(Data);
        }

        public void Show(DialogueDef def)
        {
            _external = def;
            Show();
        }

        public DialogueData Data
        {
            get
            {
                switch (_mode)       
                {
                    case Mode.Bound: return _bound;
                    case Mode.External: return _external.Data;
                    default: throw new ArgumentException();
                }
            }
        }
        public enum Mode
        {
            Bound, 
            External
        }
    }

   
}