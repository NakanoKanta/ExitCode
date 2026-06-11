using UnityEngine;
using UnityEngine.AI;

public class DoorMove : MonoBehaviour
{
    [SerializeField] NavMeshObstacle _doorObstacle;
    private void Start()
    {
        _doorObstacle.enabled = false; 
    }

    public void OpenDoor()
    {
        _doorObstacle.enabled = false;
    }

    public void CloseDoor()
    {
        _doorObstacle.enabled = true;
    }
}
