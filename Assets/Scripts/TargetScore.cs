using UnityEngine;
namespace Bow
{
    public class TargetScore : HittableObject
    {
        [SerializeField] int score = 0;
        int tempScore = 0;

        //private void Start() => Debug.LogWarning($"Change the {gameObject.name}'s OnCollisionEnter function to appropriately find arrow.");

        public override void OnArrowHit(Arrow _arrow)
        {
            if (_arrow)
            {
                Debug.Log("Collision with " + score);
                if (_arrow.scoreFromHit < score)
                {
                    GameControl.AddScore(score - _arrow.scoreFromHit);
                    _arrow.scoreFromHit = score;
                }
            }
        }
        void Stab()
        {
            
            
        }
        
        /*private void OnCollisionEnter(Collision _collision)
        {
            if (_collision.gameObject.CompareTag("Arrow"))
            {
                //var hit = _collision.contacts[0].point;
                //var dis = Vector3.Distance(transform.position, hit);
                //print("Distance = " + dis);

                tempScore++;
                score += tempScore;
                print("Score: " + score.ToString());
                tempScore = 0;
                GameControl.AddScore(score);
            }
        }
        */
    }
}