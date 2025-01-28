using System.Collections.Generic;
using UnityEngine;

namespace BubbleGame.Core
{
    public class SpawnSystem : MonoBehaviour
    {
        [Header("Spawn Settings")]
        public int croquettes = 50; //remove pour prendre la valeur présente dans le script du joueur 
        //système de croquettes prcq jsuis une fraude j'ai pas compris qui fait quoi et où
        public int index;
        public int coutEnCroquettes = 10; 
        [Header("Assignations")]
        [SerializeField]
        private GameObject[] animals; //les trucs à spawn
        
        
        [SerializeField] 
        private Transform spawnPoint;


        public void SpawnObject()
        {
            if (croquettes >= coutEnCroquettes)
            {
                croquettes -= coutEnCroquettes;//enlève les croquettes

                //TODO dynamic index
                SpawnAnimal(index);

                /* la poule c num 0 et ainsi de suite jusquà la chimere mais dcp jsp comment ça va s'organiser
 après quand yaura les bonus et tt bref */

            }

            else
            {
                Debug.Log("Pas assez de croquettes mgl");
                
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
