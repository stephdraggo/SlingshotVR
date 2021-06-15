using System;
using System.Collections;
using System.Collections.Generic;
using Bow;
using UnityEngine;

namespace PCBuild
{
    public class PlayerShoot : MonoBehaviour
    {
        [Header("Arrow prefab")]
        [SerializeField] PCArrow arrowPrefab;
        [SerializeField] float reload = 0.1f;
        [SerializeField] private int maxArrows = 20;
        [SerializeField] private int arrows;
        [SerializeField] private bool limitedArrows;

        [SerializeField] Transform shootLocation;

        float reloadTimer;


        private void Start()
        {
            if (GameControlPC.gameMode == GameMode.Strategic)
            {
                limitedArrows = true;
                arrows = maxArrows;
                GameControlPC.instance.SetArrowText("Arrows: " + arrows);
            }
        }

        private void Update()
        {
            MouseClickShoot();
        }

        void MouseClickShoot()
        {
            if (reloadTimer <= 0 && (arrows > 0 || !limitedArrows))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ShootArrow();
                    if (limitedArrows) GameControlPC.instance.SetArrowText("Arrows: " + arrows);
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
            arrows--;
        }

   
    }

}
