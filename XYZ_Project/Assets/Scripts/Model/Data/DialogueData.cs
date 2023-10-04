using System;
using UnityEngine;

namespace Model.Data
{
    [Serializable]
    public class DialogueData
    {
        [SerializeField] private string[] _sentences;

        public string[] Sentences => _sentences;

    }
}