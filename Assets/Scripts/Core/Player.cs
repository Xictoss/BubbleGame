using UnityEngine;

namespace BubbleGame.Core
{
    public class Player : MonoBehaviour
    {
        public int Soap = 0;
        public int Croquette = 0;

        private void OnEnable()
        {
            Soap = 0;
            Croquette = 0;
        }
        
    }
}
