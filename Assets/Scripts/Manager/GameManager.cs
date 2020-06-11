using CardMemory.Card;
using CardMemory.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardMemory.Manager
{
    public class GameManager : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private GameObject[] stages;
        [SerializeField]
        private Text StageText;
        [SerializeField]
        private Text TimerText;
        [SerializeField]
        private Text PointText;
        [SerializeField]
        private GameObject GameUI;
        [SerializeField]
        private GameResult ClearPanel;

        [SerializeField]
        private AudioClip Bgm;

        [SerializeField]
        private AudioClip GoodClip;
        [SerializeField]
        private AudioClip BadClip;

        private Touch touch;
        private int score = 0;

        private const string TIME_MESSAGE = "남은 시간: ";

        private byte cardCount = 0; // 스테이지 내부 카드 다 눌렀는지 체크
        private byte stageCount = 0; // 현재 스테이지 확인용

        private CardScript orderCard;
        private CardScript nowCard;

        private IEnumerator timerCoroutine;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            SoundManager.Instance.PlayBgmSound(Bgm);

            CardSetting();

            for (int i = 0; i < stages.Length; ++i)
            {
                stages[i].SetActive(false);
            }

            StageChange();
        }

        private void Update()
        {
            CardTouch();
        }

        private void CardTouch()
        {
            GameObject target;

            if (0 < Input.touchCount && Input.touchCount < 2)
            {
                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    target = GetClickedObject();
                    if (target != null)
                    {
                        target.GetComponent<CardScript>().ChangedRotation(y: 180);
                        CardChecked(target.GetComponent<CardScript>());
                    }
                }
            }
        }

        private void CardSetting()
        {
            List<CardInfo> cardInfos = ResourceManager.LoadResources(); // 모든 카드 리소스
            List<CardScript> stageCards; // 해당 스테이지 카드를 담을 리스트

            List<byte> randInfoIndex = new List<byte>(); // 카드 인덱스 담을 리스트

            for(int i = 0; i < stages.Length; ++i)
            {
                stageCards = stages[i].GetComponent<StageScript>().Cards; // 해당 스테이지 카드목록 불러옴

                byte max = (byte)(stageCards.Count * 0.5);

                byte indexCount = 0;
                byte temp;

                while(indexCount < max)
                {
                    temp = (byte)Random.Range(0, cardInfos.Count);
                    if (!(randInfoIndex.Contains(temp)))
                    {
                        randInfoIndex.Add(temp);
                        ++indexCount;
                    }
                }

                byte[] check = new byte[max];
                byte count = 0;

                while(count < stageCards.Count)
                {
                    byte t = (byte)Random.Range(0, max);

                    if(!(check[t] == 2))
                    {
                        stageCards[count].CardInfo = cardInfos[randInfoIndex[t]];
                        ++check[t];
                        ++count;
                    }
                }

                randInfoIndex.Clear();
            }
        }

        private void CardChecked(CardScript card)
        {
            if(orderCard == null)
            {
                orderCard = card;
                return;
            }

            nowCard = card;

            if(orderCard.CardInfo.CardType == nowCard.CardInfo.CardType)
            {
                if(orderCard.CardInfo.Suit == nowCard.CardInfo.Suit)
                {
                    UpdatePoint();
                    UpdateCardCount();
                    SoundManager.Instance.PlayEffectSound(GoodClip);
                    return;
                }
            }

            SoundManager.Instance.PlayEffectSound(BadClip);

            orderCard.ChangedRotation();
            nowCard.ChangedRotation();

            ResetCard();
        }

        private void UpdatePoint()
        {
            score += stages[stageCount].GetComponent<StageScript>().StageInfo.Point;
            PointText.text = score.ToString();
        }

        private void UpdateCardCount()
        {
            ++cardCount;
            ResetCard();

            if ((byte)(stages[stageCount].GetComponent<StageScript>().Cards.Count * 0.5) <= cardCount)
            {
                NextStage();
                cardCount = 0;
            }
        }

        private void ResetCard()
        {
            orderCard = null;
            nowCard = null;
        }

        private GameObject GetClickedObject()
        {
            RaycastHit hit;
            GameObject target = null;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(true == (Physics.Raycast(ray.origin, ray.direction, out hit)))
            {
                if(hit.collider.gameObject.tag.Equals("Card"))
                {
                    target = hit.collider.gameObject;
                    if(target.GetComponent<CardScript>().IsTouch)
                    {
                        return null;
                    }
                }
            }

            return target;
        }

        private IEnumerator TimeUpdate(float time)
        {
            while(0 < time)
            {
                TimerText.text = TIME_MESSAGE + time;
                time--;
                yield return Yield.WaitForSecond(1.0f); 
            }
            NextStage();
        }

        private void StageChange()
        {
            StageInfo stageInfo = stages[stageCount].GetComponent<StageScript>().StageInfo;

            StageText.text = stageInfo.StageName;

            timerCoroutine = TimeUpdate(stageInfo.Time);

            stages[stageCount].SetActive(true);
            StartCoroutine(timerCoroutine);
        }

        private void NextStage()
        {
            stages[stageCount].SetActive(false);
            StopCoroutine(timerCoroutine);
            ++stageCount;

            if (stages.Length <= stageCount)
            {
                GameClose();
                return;
            }

            StageChange();
        }

        private void GameClose()
        {
            ClearPanel.gameObject.SetActive(true);
            ClearPanel.OpenResult(score);
            CanvasActive(false);
        }

        private void CanvasActive(bool value)
        {
            GameUI.SetActive(value);
        }
    }
}
