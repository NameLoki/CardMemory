using CardMemory.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardMemory.Manager
{
    public class SceneChangedManager : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private AudioClip effect;

        public void LoadScene(string sceneName)
        {
            SoundManager.Instance.PlayEffectSound(effect);
            SceneManager.LoadScene(sceneName);
        }
    }
}
