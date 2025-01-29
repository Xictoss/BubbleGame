using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleGame.Core
{
    public class DragAndDrop : MonoBehaviour
    {
        private AnimalController animalController;

        [SerializeField] 
        private float maxDragDistance;
        
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

        private void OnMouseUp()
        {
            
            isDragged = false;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Animal"))
            {
                Animal a = GetComponent<Animal>();
                Animal b = hit.collider.GetComponent<Animal>();
                animalController.Merge(a, b);
            }
            if(animalController != null)
                animalController.Merge(a, b);
            else
            {
                Debug.LogError("No animal controller was assigned", gameObject);
            }
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
                }
                Debug.Log(hit);
            }

            if (Input.GetMouseButtonUp(0) && isDragged)
            {
                isDragged = false;
                animal.enabled = true;
            }
        }

        private void FixedUpdate()
        {
            if (isDragged)
            {
                Debug.Log("Doing drag", gameObject);
                animal.enabled = false;
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