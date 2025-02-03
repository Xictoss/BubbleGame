using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BubbleGame.Core.MenuSystem
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject _creditImage;
        
        public void Play()
        {
            SceneManager.LoadScene("TutoScene");
        }

        public void Credits()
        {
            _creditImage.SetActive(true);
        }

        public void Cross()
        {
            _creditImage.SetActive(false);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}