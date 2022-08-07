using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance;
    public GameObject activeTower;
    int towerCost;
    [SerializeField] public Transform transparentTowerHolder;
    [Range(1, 2)] public int gameSpeed = 1;
    void Awake()
    {
        Instance = this;
    }

    public void SetGameSpeed()
    {
        GameObject speedButton = GameObject.FindGameObjectWithTag("SpeedChange");
        if (gameSpeed == 1)
        {
            gameSpeed = 2;
            speedButton.transform.GetChild(0).gameObject.SetActive(false);
            speedButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            gameSpeed = 1;
            speedButton.transform.GetChild(0).gameObject.SetActive(true);
            speedButton.transform.GetChild(1).gameObject.SetActive(false);
        }

    }

    public void SetCost(int setTowerCost)
    {
        towerCost = setTowerCost;
    }

    public bool CreateTower(Vector3 position)
    {
        if (activeTower == null) return false;
        Bank bank = FindObjectOfType<Bank>();
        if (bank == null)
        {
            return false;
        }

        if (bank.CurrentBalance >= towerCost)
        {
            Instantiate(activeTower, position + Vector3.up / 2, Quaternion.identity);
            bank.Withdraw(towerCost);
            ReturnTransparentTower();
            return true;
        }
        return false;
    }

    public void FollowMouseForTower()
    {
        if (activeTower == null) return;
        Vector2 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300))
        {
            Vector3 hitPosition = hit.collider.transform.position;
            hitPosition.y = hit.point.y;
            if (hit.collider.gameObject.GetComponent<Waypoint>() == null || !hit.collider.gameObject.GetComponent<Waypoint>().IsPlaceable)
            {
                return;
            }
            transparentTowerHolder.position = hitPosition;
        }
    }

    public void ReturnTransparentTower()
    {
        transparentTowerHolder.position = new Vector3(500f, 0f, 500f);
        activeTower = null;
    }
}