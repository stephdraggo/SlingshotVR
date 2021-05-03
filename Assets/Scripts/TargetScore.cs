using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScore : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private MeshCollider c2, c4, c6, c8, c10;
    
    static int score = 0;
    int tempScore = 0;
    
    private void OnTriggerEnter(Collider _other)
    {
        if (_other == c10) tempScore = 10;
        else if (_other == c8) tempScore = 8;
        else if (_other == c6) tempScore = 6;
        else if (_other == c4) tempScore = 4;
        else if (_other == c2) tempScore = 2;
    }

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.collider == boxCollider)
        {
            score += tempScore;
            print(score.ToString());
            tempScore = 0;
        }
    }
}
// wait no. this script is the wrong way around. crud