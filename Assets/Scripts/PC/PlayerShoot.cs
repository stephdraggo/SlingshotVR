using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PCBuild
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Arrow prefab")]
        [SerializeField] PCArrow arrowPrefab;
        [SerializeField] float reload = 0.1f;

        [SerializeField] Transform shootLocation;

        float reloadTimer;


        private void Update()
        {
            MouseClickShoot();
        }

        void MouseClickShoot()
        {
            if (reloadTimer <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ShootArrow();
                    reloadTimer = reload;
                }
            }
            else
            {
                reloadTimer -= Time.deltaTime;
            }
        }

        void ShootArrow()
        {
            PCArrow arrow = Instantiate(arrowPrefab);
            arrow.Setup(shootLocation);
        }

   
    }

}
