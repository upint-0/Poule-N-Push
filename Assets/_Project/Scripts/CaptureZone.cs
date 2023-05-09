using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureZone : MonoBehaviour
{
    public bool Player1;

    public List<ChickenCore> Chickens = new List<ChickenCore>();
    private MMFeedbacks _feedbacks;

    private void Awake()
    {
        _feedbacks = GetComponent<MMFeedbacks>();
    }

    private void Start()
    {
        RoundManager.Instance.RestartRound.AddListener( CleanList );
    }

    private void CleanList()
    {
        Chickens.Clear();
    }

    private void OnTriggerEnter( Collider other )
    {
        ChickenCore chicken = other?.GetComponent<ChickenCore>();
        if ( chicken )
        {
            Chickens.Add( chicken );
            _feedbacks.PlayFeedbacks();
            other.gameObject.GetComponent<ChickenCore>().CurrentState = new EatingChickenState(other.gameObject.GetComponent<ChickenCore>());
        }
        
    }

    private void OnTriggerExit( Collider other )
    {
        ChickenCore chicken = other?.GetComponent<ChickenCore>();

        if ( Chickens.Contains( chicken ) )
        {
            chicken.StopAllCoroutines();
            Chickens.Remove( chicken );
        }
    }

   

   
}
