using CardMemory.Manager;
using UnityEngine;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField]
    private Text PointText;
    [SerializeField]
    private Text MessageText;
    [SerializeField]
    private AudioClip Effect;

    private const string NEW_SCORE_MESSAGE = "새로운 기록 갱신!";
    private const string RESULT_MESSAGE = "결과";

    public void OpenResult(int point)
    {
        SoundManager.Instance.StopBgmSound();
        PointText.text = point.ToString();
        SoundManager.Instance.PlayEffectSound(Effect);
        PointChecked(point);
    }

    private void PointChecked(int point)
    {
        string message;

        if(PlayerPrefs.GetInt("HightScore", 0) < point)
        {
            SaveHightScore(point);
            message = NEW_SCORE_MESSAGE;
        }
        else
        {
            message = RESULT_MESSAGE;
        }

        MessageText.text = message;
    }

    private void SaveHightScore(int point)
    {
        PlayerPrefs.SetInt("HightScore", point);
    }
}
