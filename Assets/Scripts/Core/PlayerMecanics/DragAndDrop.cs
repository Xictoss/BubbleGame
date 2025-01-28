using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BubbleGame.Core
{
    public class Dragger : MonoBehaviour
    {
        
        [SerializeField] private AnimalController animalController;
        private Vector3 MousePos;
        private Camera _cam;

        private Animal currentDraggedAnimal;

        private Animal a;
        private Animal b;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void OnMouseUp()
        {
            animalController.Merge(a, b);
        }

        public bool isDragged;
        
        private void OnMouseDrag()
        {
            if (CompareTag("Animals"))
            {
                transform.position = GetMousePos();
                
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
        