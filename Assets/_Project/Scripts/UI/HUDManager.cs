using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [Header( "Sound" )]
    public MMFeedbacks Sounds;

    [Header( "MainMenu" )]

    public GameObject HUDINGAME;
    public GameObject MainMenu;
    public GameObject VictoryScreen;
    public Image PlayButton, LeaveButton, Title, Panel;

    [Header( "HUDINGAME" )]
    // [SerializeField] private GameManager gameManager;
    [SerializeField] private Image SliderTime;
    [SerializeField] private Image P1Victory, P2Victory, Tie;
    [SerializeField] private Image P1FinalVictory, P2FinalVictory, Return;
    [SerializeField] private float ActualTime, StartTime;

    [SerializeField] private List<Image> OeufsP1, oeufP2;

    private int ActualWinRateP1 = 0;
    private int ActualWinRateP2 = 0;

    [SerializeField] private GameObject Arrow;

    private float RotationFinal = -360;
    private float RotationActuel;
    private float ActualRotationInSeconds;

    private float BaseArrowRot;

    private bool IsPLayying = true;


    private void Start()
    {
        SliderTime.fillAmount = 1;
        StartTime = ( float ) RoundManager.Instance.RoundDuration;
        ActualTime = StartTime;
        ActualRotationInSeconds = RotationFinal / StartTime;
        RotationActuel = Arrow.transform.rotation.z;
        BaseArrowRot = Arrow.transform.rotation.z;

        foreach ( var item in OeufsP1 )
        {
            item.gameObject.SetActive( false );
        }
        foreach ( var item in oeufP2 )
        {
            item.gameObject.SetActive( false );
        }
        RoundManager.Instance.VictoryEvent.AddListener( DisplayVictory );
        RoundManager.Instance.RestartRound.AddListener( Restart );
        RoundManager.Instance.FinalWin.AddListener( EndGame );
        //RoundManager.Instance.ReloadRound.AddListener( LOL );
    }

    private void LOL()
    {
        //LoadLevel();
    }

    private void Awake()
    {
        VictoryScreen.SetActive(false);
    }

    private void Restart()
    {
        VictoryScreen.SetActive( false );
        P1Victory.gameObject.SetActive( false );
        P2Victory.gameObject.SetActive( false );
        Tie.gameObject.SetActive( false );
        SliderTime.fillAmount = 1;
        RotationActuel = BaseArrowRot;
        ActualTime = StartTime;
    }

    private void EndGame( bool p1 )
    {
        P1Victory.gameObject.SetActive(false);
        P2Victory.gameObject.SetActive(false);
        Tie.gameObject.SetActive(false);

        VictoryScreen.SetActive( true );

        if ( p1 )
        {
            P1FinalVictory.gameObject.SetActive( true );
        }
        else
        {
            P2FinalVictory.gameObject.SetActive( true );
        }

        Return.gameObject.SetActive( true );

        IsPLayying = false;
    }

    private void OnDestroy()
    {
        RoundManager.Instance.VictoryEvent.RemoveListener( DisplayVictory );
        RoundManager.Instance.RestartRound.RemoveListener( Restart );
    }

    private void DisplayVictory( EVictoryStates victory )
    {
        switch ( victory )
        {
            case EVictoryStates.Player1:
                P1Victory.gameObject.SetActive( true );
                OeufsP1[ ActualWinRateP1 ].gameObject.SetActive( true );
                ActualWinRateP1++;
                break;
            case EVictoryStates.Player2:
                P2Victory.gameObject.SetActive( true );
                oeufP2[ ActualWinRateP2 ].gameObject.SetActive( true );
                ActualWinRateP2++;
                break;
            case EVictoryStates.Tie:
                Tie.gameObject.SetActive( true );

                oeufP2[ ActualWinRateP2 ].gameObject.SetActive( true );
                ActualWinRateP2++;
                OeufsP1[ ActualWinRateP1 ].gameObject.SetActive( true );
                ActualWinRateP1++;
                break;
            default:
                break;
        }
    }

    private void CalculTime()
    {
        if ( ActualTime == 0 )
        {
            ActualTime = 0;
            RotationActuel = RotationFinal;
            Quaternion Rotation = Quaternion.Euler( Arrow.transform.rotation.x, Arrow.transform.rotation.y, RotationActuel );
            Arrow.transform.rotation = Rotation;
            return;
        }
        else
        {
            ActualTime = Mathf.Clamp( ActualTime -= Time.deltaTime, 0, StartTime );
            SliderTime.fillAmount = ActualTime / StartTime;
            RotationActuel += ActualRotationInSeconds * Time.deltaTime;
            Quaternion Rotation = Quaternion.Euler( Arrow.transform.rotation.x, Arrow.transform.rotation.y, RotationActuel );
            Arrow.transform.rotation = Rotation;
        }
    }

    public void PLayaSoundOfTouch()
    {
        Sounds.PlayFeedbacks();

    }
    private void Update()
    {
        if ( IsPLayying )
        {
            CalculTime();
        }
    }
}
