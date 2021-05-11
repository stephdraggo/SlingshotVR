using UnityEngine;

public class TargetScore : MonoBehaviour
{
    [SerializeField] private MeshCollider trigger;
    [SerializeField] private MeshCollider c2, c4, c6, c8, c10;
    
    static int score = 0;
    int tempScore = 0;

    private void Start() => Debug.LogWarning($"Change the {gameObject.name}'s OnCollisionEnter function to appropriately find arrow.");

    private void OnCollisionEnter(Collision _collision)
    {
        if (_collision.gameObject.CompareTag("Arrow"))
        {
            var hit = _collision.contacts[0].point;
            var dis = Vector3.Distance(transform.position, hit);
            print("Distance = " + dis);

            tempScore++;
            score += tempScore;
            print("Score: " + score.ToString());
            tempScore = 0;
        }
    }
}