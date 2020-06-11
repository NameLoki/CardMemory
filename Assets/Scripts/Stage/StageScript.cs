using System.Collections.Generic;
using UnityEngine;
using CardMemory.Card;

namespace CardMemory.Stage
{
    public class StageScript : MonoBehaviour
    {
        public List<CardScript> Cards = new List<CardScript>();
#pragma warning disable 649
        [SerializeField]
        private StageInfo stageInfo;

        public StageInfo StageInfo { get { return stageInfo; } }

        private void Awake()
        {
            for(int i = 0; i < transform.childCount; ++i)
            {
                Cards.Add(transform.GetChild(i).gameObject.GetComponent<CardScript>());
            }
        }
    }
}
