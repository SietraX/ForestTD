using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardShortcuts : MonoBehaviour
{
    [SerializeField] GameObject mushroomButtonTrigger;
    [SerializeField] GameObject cannonButtonTrigger;
    [SerializeField] GameObject wizardButtonTrigger;
    void Update()
    {
        InGameManager.Instance.FollowMouseForTower();

        TowerShortcuts();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            InGameManager.Instance.ReturnTransparentTower();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InGameManager.Instance.SetGameSpeed();
        }
    }

    void TowerShortcuts()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ExecuteEvents.Execute<IPointerDownHandler>(mushroomButtonTrigger, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ExecuteEvents.Execute<IPointerDownHandler>(cannonButtonTrigger, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ExecuteEvents.Execute<IPointerDownHandler>(wizardButtonTrigger, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
    }
}
