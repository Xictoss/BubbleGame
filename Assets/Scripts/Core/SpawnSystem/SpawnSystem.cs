using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BubbleGame.Core
{
    public class SpawnSystem : MonoBehaviour
    {

        [SerializeField] private Player player;
        [Header("Spawn Settings")]
       // public int croquettes = 50; //remove pour prendre la valeur présente dans le script du joueur 
        //système de croquettes prcq jsuis une fraude j'ai pas compris qui fait quoi et où
        public int index;
        public int coutEnCroquettes = 10; 
        [Header("Assignations")]
        [SerializeField]
        private GameObject[] animals; //les trucs à spawn
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Button button;


        public void SpawnObject()
        {
            if (player.Croquette >= coutEnCroquettes)
            {
                player.Croquette -= coutEnCroquettes;//enlève les croquettes

                //TODO dynamic index
                SpawnAnimal(index);

            }

            else
            {
                button.transform.DOShakePosition(3, Vector3.right, 1000);
            }
        }

        public void SpawnAnimal(int index)
        {
            GameObject spawnedGameObject = Instantiate(animals[index], spawnPoint.position, spawnPoint.rotation);
                
            Animal animal = spawnedGameObject.GetComponent<Animal>();
            animal.index = index;
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
