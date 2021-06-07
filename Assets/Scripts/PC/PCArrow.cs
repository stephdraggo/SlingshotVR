using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bow;

using UnityEngine;

namespace PCBuild
{
    [RequireComponent(typeof(Rigidbody))]
    public class PCArrow : MonoBehaviour
    {
        [SerializeField] float shootForce = 10;

        new Rigidbody rigidbody;
    
        /// <summary>
        /// Call after spawning a bullet to initialise it
        /// </summary>
        /// <param name="_shootTransform">Transform to shoot from, uses position and rotation</param>
        /// <param name="_shootScript">Script that fired the bullet</param>
        /// <param name="_model">Model to display on this bullet</param>
        public void Setup(Transform _shootTransform)
        {
            transform.position = _shootTransform.position;
            transform.rotation = _shootTransform.rotation;
    
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * (shootForce * 100));
            
        }
        
    
        private void OnTriggerEnter(Collider _other)
        {
            if (_other.TryGetComponent(out HittableObject hittableObject))
            {
                if (OnHittableObjectCollision(hittableObject))
                {
                    rigidbody.isKinematic = true;
                    shootForce = 0;
                }
            }

        }

        private void OnCollisionEnter(Collision _other)
        {
            LayerMask layerCollided = _other.gameObject.layer;
            int layer = layerCollided.value;
            
            if (_other.gameObject.TryGetComponent(out HittableObject hittableObject))
            {
                if (OnHittableObjectCollision(hittableObject))
                {
                    rigidbody.isKinematic = true;
                    shootForce = 0;
                }
            }
            else if(layer == 0)
            {
                rigidbody.isKinematic = true;
                shootForce = 0;
            }
        }
        
        bool OnHittableObjectCollision(HittableObject _object)
        {
            bool stick = false;
            
            //destroy or stick the arrow
            switch (_object.ArrowEffect)
            {
                case ArrowEffect.StickArrow:
                    stick = true;
                    break;
                case ArrowEffect.DestroyArrow:
                    Destroy(gameObject);
                    break;
            }
            
            //call the hit function
            _object.OnArrowHit();

            return stick;
        }
    }
}
