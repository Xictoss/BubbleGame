using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleGame.Core.PlayerMecanics
{
    public class DragAndDrop : MonoBehaviour
    {
        private static Collider2D[] colliders = new Collider2D[8];
        private AnimalController animalController;

        [SerializeField] 
        private float maxDragDistance;

        [SerializeField] private LayerMask draggedMask, nonDraggedMask;
        
        private Vector3 MousePos;
        private Camera _cam;


        private Animal a;
        private Animal b;

        private Animal animal;
        private Rigidbody2D rb2d;
        private Collider2D col2d;
        private bool isDragged;
        
        private void Awake()
        {
            _cam = Camera.main;
            animalController = FindFirstObjectByType<AnimalController>();
            rb2d = GetComponent<Rigidbody2D>();
            col2d = GetComponent<Collider2D>();
            animal = GetComponent<Animal>();
        }

        private void Start()
        {
            animalController = FindFirstObjectByType<AnimalController>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D hit = Physics2D.OverlapPoint(GetMousePos());
                if (hit != null && hit == col2d)
                {
                    isDragged = true;
                    animal.enabled = false;   
                    col2d.excludeLayers = draggedMask;
                }
            }

            if (Input.GetMouseButtonUp(0) && isDragged)
            {
                ContactFilter2D contactFilter2D = new ContactFilter2D()
                {
                    layerMask = LayerMask.GetMask("Animal", "Sell"),
                };
                int count = Physics2D.OverlapPoint(GetMousePos(), contactFilter2D, colliders);
                for (int i = 0; i < count; i++)
                {
                    if(colliders[i] == col2d)
                        continue;
                    
                    if (colliders[i].TryGetComponent(out Animal b))
                    {
                        Animal a = GetComponent<Animal>();
                        animalController.Merge(a, b);
                        break;
                    }

                    if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Sell"))
                    {
                        Animal a = animal;
                        animalController.Sell(a);
                        break;
                    }
                }

                isDragged = false;
                animal.enabled = true;
                col2d.excludeLayers = nonDraggedMask;
            }
        }

        private void FixedUpdate()
        {
            if (isDragged)
            {
                Vector2 targetPos = GetMousePos();
                
                Vector2 diff = targetPos - rb2d.position;

                var size = Mathf.Min(diff.magnitude, maxDragDistance);
                Vector2 targetVelocity = diff.normalized * size;
                
                rb2d.linearVelocity = targetVelocity / Time.deltaTime;
            }
            
        }
        
        private Vector3 GetMousePos()
        {
            var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            return mousePos;
        }
    }
}