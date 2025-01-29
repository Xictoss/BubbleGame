using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleGame.Core
{
    public class SpawnSystem : MonoBehaviour
    {
        [Header("Assignations")]
        [SerializeField] private Player player;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button button;
        [SerializeField] private GameObject[] animals; //les trucs à spawn

        [Header("Spawn Settings")]
        public int index;
        public int croquetteCost = 10; 


        public void SpawnObject() //Button pour faire spawn manuellement
        {
            if (player.Croquette >= croquetteCost)
            {
                player.Croquette -= croquetteCost; //enlève les croquettes

                //TODO dynamic index
                SpawnAnimal(index);
            }

            else
            {
                button.transform.DOShakePosition(3, Vector3.right, 1000);
            }
        }

        public Animal SpawnAnimal(int i) => SpawnAnimal(i, spawnPoint.position, spawnPoint.rotation);
        
        public Animal SpawnAnimal(int i, Vector3 position, Quaternion rotation)
        {
            GameObject spawnedGameObject = Instantiate(animals[i], position, rotation);
                
            Animal animal = spawnedGameObject.GetComponent<Animal>();
            animal.index = i;
            return animal;
        }

        public void OnTryToSpawnWithButton()
        {
            SpawnObject();
        }


        public bool IsIndexValid(int index)
        {
            return index >= 0 && index < animals.Length;
        }
        
        public GameObject GetAnimalForIndex(int index)
        {
            return animals[index];
        }

    }

}
