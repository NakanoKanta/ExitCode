using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.UI.Image;


public class ChaserMoveAI : MonoBehaviour
{
    public enum State
    {
        Patrol,
        Chase
    }
    public State currentState;

    [Header("ターゲット")]
    [SerializeField] Transform _playerTransform;

    [Header("スピード")]
    [SerializeField] float _enemySpeed = 5f;

    [Header("チェイス距離")]
    [SerializeField] float _chaseRange = 10f;
    //[SerializeField] float _loseRange = 11f;


    [Header("片側の視野角")]
    [SerializeField] float _viewAngle = 60f;
    [Header("Debug")]
    [SerializeField] bool _showGizmos = true;

    [Header("移動地点")]
    [SerializeField] Transform[] _wayPoints;

    private Rigidbody _rb;
    NavMeshAgent _agent;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrol;
    }
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;
            case State.Chase:
                Chase();
                break;
        }
    }
    void Chase()
    {
        _agent.SetDestination(_playerTransform.position);
        float dist = Vector3.Distance(transform.position, _playerTransform.position);
        if (dist > _chaseRange)
        {
            currentState = State.Patrol;
        }
    }
    void Patrol()
    {
        //到着したら次の地点へ
        if (!_agent.hasPath || _agent.remainingDistance < 0.5f)
        {
            MovePoint();
        }
        Vector3 toPlayer = _playerTransform.position - transform.position; //プレイヤーの方向を取得
        Vector3 dir = toPlayer.normalized;
        float dist = toPlayer.magnitude;

        if (dist <= _chaseRange)
        {
            float angle = Vector3.Angle(transform.forward, dir);//角度の取得
            if (angle < _viewAngle)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, dir, out hit, _chaseRange))
                {
                    if (hit.collider.CompareTag("Reporter"))
                    {
                        currentState = State.Chase;
                    }
                }
                Debug.DrawRay(transform.position, dir * _chaseRange, Color.red);
            }
        }
    }

    void MovePoint()
    {
        if(_wayPoints.Length == 0) return;

        int index = Random.Range(0, _wayPoints.Length);
        Transform targetPoint = _wayPoints[index];

        _agent.SetDestination(targetPoint.position);
    }

    private void OnDrawGizmos()
    {
        if (!_showGizmos) return;
        Gizmos.color = Color.yellow;

        int segments = 30;

        float totalAngle = _viewAngle * 2f;

        Vector3 prevPoint = transform.position;

        for (int i = 0; i <= segments; i++)
        {
            float angle =
                -totalAngle / 2f +
                (totalAngle / segments) * i;

            Vector3 dir =
                Quaternion.Euler(0, angle, 0)
                * transform.forward;

            Vector3 point =
                transform.position +
                dir * _chaseRange;

            // 中心→外周
            Gizmos.DrawLine(
                transform.position,
                point
            );

            // 外周同士
            if (i > 0)
            {
                Gizmos.DrawLine(
                    prevPoint,
                    point
                );
            }

            prevPoint = point;
        }
        Gizmos.color = Color.blue;

        Gizmos.DrawRay(
            transform.position,
            transform.forward * _chaseRange
        );
    }
}
