using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleGame.Core
{
    public class AnimalController : MonoBehaviour
    {
        private SpawnSystem spawnSystem;

        private float currentTime;
        [SerializeField]
        public float timeBeforeAutoMerge = 10;
        private void Awake()
        {
            spawnSystem = GetComponent<SpawnSystem>();
        }

        private void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timeBeforeAutoMerge)
            {
                currentTime = 0;
                var groups = Animal.Animals
                    .GroupBy(animal => animal.index);

                IGrouping<int, Animal> highest = null;
                foreach (var group in groups)
                {
                    if (group.Count() >= 2)
                    {
                        if(highest == null || highest.Key < group.Key)
                            highest = group;
                    }
                }

                if (highest != null)
                {
                    var animalsMerge = highest.ToList();
                    int first = Random.Range(0, animalsMerge.Count);
                    Animal a = animalsMerge[first];
                    animalsMerge.RemoveAt(first);
                    
                    int second = Random.Range(0, animalsMerge.Count);
                    Animal b = animalsMerge[second];
                    animalsMerge.RemoveAt(second);

                    Merge(a, b);
                }
            }
        }

        public void Merge(Animal a, Animal b)
        {
            if(a.index != b.index && a != b)
                return;
            
            int newIndex = a.index + 1;

            Vector3 aPosition = a.transform.position;
            Vector3 bPosition = b.transform.position;
            
            Transform aTransform = a.transform;
            Transform bTransform = b.transform;
            
            Vector3 center = (aPosition + bPosition) * 0.5f;
            aTransform.DOMove(center, 1);
            bTransform.DOMove(center, 1).OnComplete(() =>
            {
                Destroy(a.gameObject);
                Destroy(b.gameObject);
            
                if (spawnSystem.IsIndexValid(newIndex))
                { 
                    spawnSystem.SpawnAnimal(newIndex, a.transform.position, a.transform.rotation);
                }
                else
                {
                    //TODO game end
                    Debug.Log("GG");
                    
                }
            });
        }
    }   
        
}    
