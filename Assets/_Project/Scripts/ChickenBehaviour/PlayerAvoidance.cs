using UnityEditor;
using UnityEngine;

public class PlayerAvoidance : AChickenModule
{
    private PlayerController _player1;
    private PlayerController _player2;
    private float _actualFarthestPlayerDetection;
    private float _player1Threat;
    private float _player2Threat;

    public override void Execute(ChickenModuleData moduleData)
    {
        Vector3 chickenDirectionFromPlayer1 = new Vector3(transform.position.x - _player1.transform.position.x, 0f, transform.position.z - _player1.transform.position.z);
        Vector3 chickenDirectionFromPlayer2 = new Vector3(transform.position.x - _player2.transform.position.x, 0f, transform.position.z - _player2.transform.position.z);
        _actualFarthestPlayerDetection = _chicken.Data.FarthestPlayerDetection * moduleData.Multiplier;

        _player1Threat = ComputeThreat(_player1, chickenDirectionFromPlayer1);
        _player2Threat = ComputeThreat(_player2, chickenDirectionFromPlayer2);

        //average between the opposite directions of each players, but the biggest threat has more impact
         Vector3 player1WeightedDirection = _player1Threat * chickenDirectionFromPlayer1;
         Vector3 player2WeightedDirection = _player2Threat * chickenDirectionFromPlayer2;
        ResultingDirection = (player1WeightedDirection + player2WeightedDirection).normalized;

        //the speed is equal to the biggest threat, but never faster than max speed
        float biggestThreat = Mathf.Max(_player1Threat, _player2Threat);
        ResultingSpeed = Mathf.Min(biggestThreat, _chicken.Data.MaxMetersPerSecond);

        //the player avoidance shouldn't slow down the chicken
        ResultingSpeed = Mathf.Max(ResultingSpeed, _chicken.Movement.CurrentSpeed);
    }

    private float ComputeThreat(PlayerController player, Vector3 chickenDirectionFromPlayer)
    {
        float playerThreat = 0f;
        float distanceWithPlayer = Vector3.Distance(player.transform.position, transform.position);

        if(distanceWithPlayer < _chicken.Data.FarthestPlayerDetection)
        {
            //speed towards chicken
            float playerOffsetAngle = Vector3.Angle(player.transform.forward, chickenDirectionFromPlayer);
            float playerSpeedTowardsChicken = Mathf.Cos(playerOffsetAngle * Mathf.Deg2Rad) * new Vector3(player.Input.x, 0f, player.Input.y).magnitude * player.MaxSpeed;

            //computes the threat from the distance with the player and their speed towards the chicken.
            //when the player is at a distance of [DistanceForMatchingSpeed], their threat is equal to their speed towards the chicken.
            playerThreat = Math.Remap(
                value: distanceWithPlayer,
                min1: _actualFarthestPlayerDetection,
                max1: _chicken.Data.DistanceForMatchingSpeed,
                min2: _chicken.Data.MinMetersPerSecond,
                max2: playerSpeedTowardsChicken,
                mustClampAtMin: true
                );
        }

        return playerThreat;
    }

    private void Awake()
    {
        _player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
        _player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }

    private void OnDrawGizmos()
    {
        //directed threat from both players in their respective color
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_player1.transform.position.x - transform.position.x, 0f, _player1.transform.position.z - transform.position.z).normalized * _player1Threat);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_player2.transform.position.x - transform.position.x, 0f, _player2.transform.position.z - transform.position.z).normalized * _player2Threat);

        //final velocity
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + ResultingDirection * ResultingSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        //player detection
        if(ResultingSpeed > 0f)
        {
            Handles.color = Color.red;
        }
        else
        {
            Handles.color = Color.green;
        }

        Handles.DrawWireDisc(transform.position, Vector3.up, _actualFarthestPlayerDetection);
    }
}
