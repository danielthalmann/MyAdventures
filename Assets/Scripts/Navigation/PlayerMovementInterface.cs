using UnityEditor;
using UnityEngine;


public interface PlayerMovementInterface
{
    public void SetDestination(Vector3 destination, GameObject hitObject = null);
}
