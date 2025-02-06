using UnityEngine;

namespace BubbleGame.Core.SFXManager
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;
     
        [SerializeField]
        private SoundLibrary sfxLibrary;
        [SerializeField]
        private AudioSource sfx2DSource;
     
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        
        public void PlaySound2D(string soundName)
        {
            sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
        }
    }
}
