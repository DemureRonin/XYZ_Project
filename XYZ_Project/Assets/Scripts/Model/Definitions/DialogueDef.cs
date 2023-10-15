using Model.Data;
using UnityEngine;

namespace Scripts.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/Dialogue", fileName = "Dialogue")]
    public class DialogueDef : ScriptableObject
    {
        [SerializeField] private DialogueData _data;
        public DialogueData Data => _data;
    }
}