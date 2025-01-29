using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BubbleGame.Core
{
    public class Animal : MonoBehaviour
    {
        public static IEnumerable<Animal> Animals => animals;
        
        private static List<Animal> animals = new List<Animal>();

        [SerializeField] 
        public int soapValue;
        [SerializeField] 
        public int croquetteValue;
        [SerializeField] 
        float moveSpeed = 2.5f;
        
        [HideInInspector]
        public int index;
        
        private Rigidbody2D rb2d;
        
        private Vector2 targetDirection;
        private Vector2 lastDirection;
        private float currentWaitTime;
        private float waitTime;


        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            transform.DOPunchScale(Vector3.one * 0.25f, 0.2f);
        }

        private void OnEnable()
        {
            animals.Add(this);
        }

        private void OnDisable()
        {
            animals.Remove(this);
        }

        private void Start()
        {
            targetDirection = Random.insideUnitCircle.normalized;
            SetNewRandomDirection();
        }

        private void Update()
        {
            currentWaitTime += Time.deltaTime;
            if (currentWaitTime >= waitTime)
            {
                SetNewRandomDirection();
            }
        }

        private void SetNewRandomDirection()
        {
            waitTime = Random.Range(2f, 4f);
            currentWaitTime = 0;
                
            lastDirection = targetDirection;
                
            Vector2 direction = Random.insideUnitCircle;
            targetDirection = direction.normalized;
        }

        private void FixedUpdate()
        {
            float interpolation = Mathf.InverseLerp(0, waitTime, currentWaitTime);
            
            Vector2 currentDirection = Vector2.Lerp(lastDirection, targetDirection, interpolation).normalized;
            rb2d.linearVelocity = currentDirection * moveSpeed;
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            targetDirection = Vector2.Reflect(targetDirection, other.contacts[0].normal).normalized;
        }
    }
}
