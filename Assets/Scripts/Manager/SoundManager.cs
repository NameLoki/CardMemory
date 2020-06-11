using UnityEngine;

namespace CardMemory.Manager
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance;
        public static SoundManager Instance { get { return instance;  } }

#pragma warning disable 649
        [SerializeField]
        private AudioSource effectSource;
        [SerializeField]
        private AudioSource bgmSource;

        private float bgmVolume;
        private float effectVolume;

        private void Awake()
        {
            if(instance == null)
            {
                SetBgmVolume(0.1f);
                SetEffectVolume(0.1f);
                instance = this;
            }
        }

        public void SetBgmVolume(float volume)
        {
            bgmVolume = volume;
            bgmSource.volume = volume;
        }

        public void SetEffectVolume(float volume)
        {
            effectVolume = volume;
            effectSource.volume = volume;

        }

        public void PlayEffectSound(AudioClip clip)
        {
            effectSource.clip = clip;
            effectSource.Play();
        }

        public void PlayBgmSound(AudioClip clip)
        {
            bgmSource.clip = clip;
            bgmSource.Play();
        }

        public void StopBgmSound()
        {
            bgmSource.Stop();
        }

        public void StopEffectSound()
        {
            effectSource.Stop();
        }
    }
}
