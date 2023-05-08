using UnityEngine;

public class PlayerAvoidance : MonoBehaviour
{
    [SerializeField] private float _avoidanceRadius;

    private PlayerController _player1;
    private PlayerController _player2;
    private PlayerController _closestPlayer;
    bool _playerIsInAvoidanceRadius;

    private void Awake()
    {
        _player1 = GameObject.FindWithTag("Player1").GetComponent<PlayerController>();
        _player2 = GameObject.FindWithTag("Player2").GetComponent<PlayerController>();
    }

    public Vector3 ComputeDirection(ChickenMultipliers multipliers)
    {
        float player1Dist = Vector3.Distance(_player1.transform.position, transform.position);
        float player2Dist = Vector3.Distance(_player2.transform.position, transform.position);

        float minDist;

        if(player1Dist < player2Dist)
        {
            minDist = player1Dist;
            _closestPlayer = _player1;
        }
        else
        {
            minDist = player2Dist;
            _closestPlayer = _player2;
        }

        float playerSpeedFactor = Remap(_closestPlayer.Input.magnitude, 0f, 1f, 1f, 2f);
        _playerIsInAvoidanceRadius = minDist < _avoidanceRadius * multipliers.PlayerAvoidance * playerSpeedFactor;

        if(_playerIsInAvoidanceRadius)
        {
            Vector2 direction = (ChickenCore.GetFlattenedVector(transform.position) - ChickenCore.GetFlattenedVector(_closestPlayer.transform.position)).normalized;

            return new Vector3(direction.x, 0f, direction.y);
        }

        return Vector3.zero;
    }

    public float ComputeSpeed()
    {
        if(_playerIsInAvoidanceRadius)
        {
            Quaternion playerChickenRotation = Quaternion.Euler(new Vector3(transform.position.x - _closestPlayer.transform.position.x, 0f, transform.position.z - _closestPlayer.transform.position.z));
            float playerOffsetAngle = Quaternion.Angle(_closestPlayer.transform.rotation, playerChickenRotation);
            float playerSpeedTowardsChicken = Mathf.Cos(playerOffsetAngle * Mathf.Deg2Rad) * _closestPlayer.MaxSpeed;

            return playerSpeedTowardsChicken;
        }

        return 0f;
    }

    public static float Remap(float value, float min1, float max1, float min2, float max2)
    {
        return min2 + (value - min1) * ((max2 - min2) / (max1 - min1));
    }
}
