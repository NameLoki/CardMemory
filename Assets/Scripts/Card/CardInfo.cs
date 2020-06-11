using UnityEngine;

namespace CardMemory.Card
{
    [CreateAssetMenu(fileName = "Card Info", menuName = "Scriptable Object/CardInfo", order = int.MaxValue)]
    public class CardInfo : ScriptableObject
    {
#pragma warning disable 649
        [SerializeField]
        private ECardType cardType;
        [SerializeField]
        private ESuit suit;
        [SerializeField]
        private Material cardMaterial;

        public ECardType CardType { get { return cardType; } }
        public ESuit Suit { get { return suit; } }
        public Material CardMaterial { get { return cardMaterial; } }
    }
}
