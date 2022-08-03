using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Tile> path = new List<Tile>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    Enemy enemy;
    // Start is called before the first frame update
    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void FindPath()
    {
        path.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Tile tile = child.GetComponent<Tile>();
            if (tile != null)
            {
                path.Add(tile);
            }
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    IEnumerator FollowPath()
    {
        foreach (Tile tile in path)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = tile.transform.position;
            endPosition.y = 1.5f;
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
