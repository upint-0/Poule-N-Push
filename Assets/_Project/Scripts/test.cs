using MoreMountains.Feedbacks;
using UnityEngine;

public class test : MonoBehaviour
{
    private MMFeedbacks _feedbacks;

    private void Awake()
    {
        _feedbacks = GetComponent<MMFeedbacks>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Chicken"))
        {
            _feedbacks.PlayFeedbacks();
            other.gameObject.GetComponent<ChickenCore>().CurrentState = new EatingChickenState(other.gameObject.GetComponent<ChickenCore>());
        }
    }
}
