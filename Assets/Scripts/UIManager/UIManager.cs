using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject noSaveGameFound;
    [SerializeField] GameObject LoadSaveGame;
    [SerializeField] RectTransform sparkEffect;

    public void LoadNextPanel()
    {
        bool isTrue = true;

        if (isTrue)
        {
            LoadSaveGame.SetActive(true);
        }
        else
        {
            noSaveGameFound.SetActive(true);
            isTrue = false;
        }
    }

    public void SparkOnHover(GameObject hoverTower)
    {
        sparkEffect.position = hoverTower.GetComponent<RectTransform>().position;
        sparkEffect.gameObject.SetActive(true);
        RectTransform towerCard = hoverTower.GetComponent<RectTransform>();
        towerCard.localScale = Vector3.one * 1.1f;
    }

    public void SparkOnExit(GameObject hoverTower)
    {
        sparkEffect.gameObject.SetActive(false);
        RectTransform towerCard = hoverTower.GetComponent<RectTransform>();
        towerCard.localScale = Vector3.one;
    }

    public void ChooseTower(GameObject chosenTower)
    {
        InGameManager.Instance.activeTower = chosenTower;
    }
}
