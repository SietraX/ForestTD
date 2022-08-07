using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } }
    void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = InGameManager.Instance.CreateTower(transform.position);
            isPlaceable = !isPlaced;
        }
        else
        {
            InGameManager.Instance.ReturnTransparentTower();
        }
    }
}
