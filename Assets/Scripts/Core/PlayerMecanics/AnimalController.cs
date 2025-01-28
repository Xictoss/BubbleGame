using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;

namespace BubbleGame.Core
{
    public class AnimalController : MonoBehaviour
    {
        [SerializeField]
        private SpawnSystem spawnSystem;


        public void Merge(Animal a, Animal b)
        {
            if(a.index != b.index)
                return;
            
            int newIndex = a.index + 1;

            Destroy(a.gameObject);
            Destroy(b.gameObject);
            
            if (spawnSystem.IsIndexValid(newIndex))
            {
                spawnSystem.SpawnAnimal(newIndex);
            }
            else
            {
                //TODO game end
                Debug.Log("GG");
            }
        }
    }   
        
}    
