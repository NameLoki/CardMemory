using CardMemory.Manager;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private Text HightScoreText;
    [SerializeField]
    private AudioClip Bgm;

    private const string SCORE_MESSAGE = "최고점수 : ";

    private void Awake()
    {
        ScorePrint();
    }

    private void Start()
    {
        StartBgm();
    }

    private void ScorePrint()
    {
        HightScoreText.text = SCORE_MESSAGE + PlayerPrefs.GetInt("HightScore", 0);
    }

    private void StartBgm()
    {
        SoundManager.Instance.PlayBgmSound(Bgm);
    }
}
