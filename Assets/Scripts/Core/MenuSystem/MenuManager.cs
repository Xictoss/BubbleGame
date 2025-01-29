using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BubbleGame.Core.MenuSystem
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _image;
        
        public void Play()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void HowToPlay()
        {
            _image.SetActive(true);
        }

        public void Cross()
        {
            _image.SetActive(false);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}