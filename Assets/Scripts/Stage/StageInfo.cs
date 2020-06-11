using UnityEngine;

namespace CardMemory.Stage
{
    [CreateAssetMenu(fileName = "Stage Info", menuName = "Scriptable Object/StageInfo", order = int.MaxValue)]
    public class StageInfo : ScriptableObject
    {
#pragma warning disable 649
        [SerializeField]
        private string stageName;
        [SerializeField]
        private float time = 30f;
        [SerializeField]
        private byte point = 5;

        public string StageName { get { return stageName; } }
        public float Time { get { return time; } }
        public byte Point { get { return point; } }
    }
}
