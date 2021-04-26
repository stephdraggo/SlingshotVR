using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScore : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private CapsuleCollider outer, middle, inner, bullseye;
    int where = 0;
    private void OnTriggerEnter(Collider _other)
    {
        if (_other == outer) where = 1;
        if (_other == middle) where = 2;
        if (_other == inner) where = 3;
        if (_other == bullseye) where = 4;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider == boxCollider)
        {
            switch (where)
            {
                case 0:
                    Debug.Log("Missed.");
                    break;
                case 1:
                    Debug.Log("Hit outter ring.");
                    break;
                case 2:
                    Debug.Log("Hit middle ring.");
                    break;
                case 3:
                    Debug.Log("Hit inner ring.");
                    break;
                case 4:
                    Debug.Log("Bullseye!");
                    break;
                default:
                    Debug.Log("Missed?");
                    break;
            }
        }
    }
}
