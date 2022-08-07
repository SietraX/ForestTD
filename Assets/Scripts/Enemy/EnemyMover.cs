using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    Enemy enemy;
    public Vector3 endPosition;
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
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if (waypoint != null)
            {

                path.Add(waypoint);
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
        foreach (Waypoint waypoint in path)
        {
            Vector3 startPosition = transform.position;
            endPosition = waypoint.transform.position;
            endPosition.y = 0.5f;
            float travelPercent = 0f;
            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed * InGameManager.Instance.gameSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
                yield return new WaitForEndOfFrame();
            }
        }
        FinishPath();
    }
}
