using CardMemory.Card;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardMemory.Manager
{
    public class ResourceManager
    {
        private static List<CardInfo> cardInfos;
        public static List<CardInfo> LoadResources()
        {
            if(cardInfos != null)
            {
                return cardInfos;
            }

            cardInfos = new List<CardInfo>();

            var types = Enum.GetValues(ECardType.Club.GetType());
            var suits = Enum.GetValues(ESuit.Ace.GetType());

            foreach(var t in types)
            {
                foreach(var s in suits)
                {
                    cardInfos.Add(Resources.Load<CardInfo>("Card/" + t + "/" + t + "_" + s));
                }
            }

            return cardInfos;
        }
    }
}
