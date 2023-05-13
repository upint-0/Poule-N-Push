using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EVictoryStates
{
    Player1,
    Player2,
    Tie
}
public class RoundManager : MonoBehaviour
{
    public static RoundManager Instance;
    
    public int RoundDuration;

    public float RoundTiming = 0.0f;

    [SerializeField] CaptureZone[] FeedingZones;

    public UnityEvent<EVictoryStates> VictoryEvent = new UnityEvent<EVictoryStates>();
    public UnityEvent RestartRound = new UnityEvent();
    public UnityEvent RelodRound = new UnityEvent();
    public UnityEvent<bool> FinalWin = new UnityEvent<bool>();

    public int Player1Score = 0;
    public int Player2Score = 0;

    private void Awake()
    {
        if ( Instance == null )
            Instance = this;
        else
            Destroy( gameObject );
    }

    private void Start()
    {
        GameManager.Instance.OnNewGameStarted.AddListener(InitializeGame);
    }

    private void InitializeGame()
    {
        Player1Score = 0;
        Player2Score = 0;
        StartCoroutine(StartRound());
    }

    public IEnumerator StartRound()
    {
        if( RoundDuration == 0 )
        {
            yield break;
        }
        else
        {
            for ( int i = 0; i < RoundDuration; i++ )
            {
                RoundTiming += 1.0f;
                yield return new WaitForSeconds( 1.0f );
            }
        }

        int chickenPlayer1 = 0;
        int chickenPlayer2 = 0;

        foreach ( var feedingZone in FeedingZones )
        {
            if ( feedingZone.Player1 )
            {
                foreach ( var chickens in feedingZone.Chickens )
                {
                    chickenPlayer1++;
                }
            }
            else
            {
                foreach ( var chickens in feedingZone.Chickens )
                {
                    chickenPlayer2++;
                }
            }
        }

        print( chickenPlayer1 );
        print( chickenPlayer2 );

        if ( chickenPlayer1 == chickenPlayer2 )
        {

            VictoryEvent?.Invoke( EVictoryStates.Tie );
            print( "It's a TIE" );
            Player1Score++;
            Player2Score++;
        }
        else
        {
            EVictoryStates victoryState = ( chickenPlayer1 > chickenPlayer2 ) ? EVictoryStates.Player1 : EVictoryStates.Player2;
            switch ( victoryState )
            {
                case EVictoryStates.Player1:
                    Player1Score++;
                    break;
                case EVictoryStates.Player2:
                    Player2Score++;
                    break;
                case EVictoryStates.Tie:
                    break;
                default:
                    break;
            }
            print( $" { victoryState } Won " );
            VictoryEvent?.Invoke( victoryState );
        }

        yield return new WaitForSeconds( 5.0f );

        if ( Player1Score == 3 || Player2Score == 3 )
        {
            if ( Player1Score == 3 )
            {
                FinalWin?.Invoke( true );
            }
            else
            {
                FinalWin?.Invoke( false );
            }

            yield break;
        }



        RestartRound?.Invoke();

        StartCoroutine( StartRound() );


    }
}
