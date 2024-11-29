using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField][Range(0f, 5f)] float speed = 1f;
    int currentWaypointIndex = 0;

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    void FindPath()
    {
        path.Clear();
        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path");

        // Sort waypoints by their hierarchy order or position
        System.Array.Sort(waypoints, (a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));

        foreach (GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>());
        }
    }

    void ReturnToStart()
    {
        currentWaypointIndex = 0;
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        while (currentWaypointIndex < path.Count)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = path[currentWaypointIndex].transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return null;
            }

            currentWaypointIndex++;
        }

        gameObject.SetActive(false);
    }
}