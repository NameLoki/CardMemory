using System.Collections;
using UnityEngine;

namespace CardMemory.Card
{
    public class CardScript : MonoBehaviour
    {
        [SerializeField]
        private CardInfo cardInfo;
        private Renderer render;
        private bool isTouch = false;
        public bool IsTouch { get { return isTouch; } }

        public CardInfo CardInfo
        {
            get 
            {
                return cardInfo; 
            } 
            set
            {
                cardInfo = value;
                CardSetting();
            }
        }

        private void Awake()
        {
            render = gameObject.GetComponent<Renderer>();
        }

        private void CardSetting()
        {
            render.material = cardInfo.CardMaterial;
        }

        private IEnumerator CardRotation(int start, int end, bool touch)
        {
            int yRotation = 1;
            float time = 0f;

            while(start < end ? (yRotation < end) : (end < yRotation))
            {
                time += Time.deltaTime / 1f;
                yRotation = (int)Mathf.Lerp(start, end, time);
                gameObject.transform.rotation = Quaternion.Euler(90, yRotation, 0);
                yield return null;
            }

            isTouch = false;
            
        }

        public void ChangedRotation(short y = 0)
        {
            if(180 <= y)
            {
                isTouch = true;
                gameObject.transform.rotation = Quaternion.Euler(90, 180, 0);
            } 
            else
            {
                StartCoroutine(CardRotation(180, 0, false));
            } 
        }
    }
}
