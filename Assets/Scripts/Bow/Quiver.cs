using System;
using UnityEngine;
using Wakaba.VR.Interaction;

namespace Bow
{
    public class Quiver : InteractableObject
    {
        [Space]
        
        [Header("Quiver Settings")]
        [SerializeField] private bool limitedArrows = false;
        [Tooltip("If limitedArrows is false, this will not have any effect")]
        [SerializeField] private int maxArrows = 20;
        private int arrowCount;

        private void Start()
        {
            arrowCount = maxArrows;
        }

        protected override bool TryTakeSpawnedObject()
        {
            //If arrows are limited and empty, return false
            return !(limitedArrows && arrowCount <= 0);
        }
    
        protected override void OnTakeSpawnedObject(InteractableObject _object)
        {
            if (limitedArrows) arrowCount--;
        }
    }

}
