using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<Transform> _spawners;

    public Transform SetRandomSpawner()
    {
        int randomIndex = Random.Range(0, _spawners.Count);
        return _spawners[randomIndex];
    }

    public Vector3 SetRandomDirection(string parentName)
    {
        string spawner = parentName;
        float horizontalDirection = 1.0f;
        float randomVerticalDirection = 1.0f;

        switch (spawner)
        {
            case "down-left":
                randomVerticalDirection = Random.Range(0.0f, 1.0f);
                break;
            case "down-right":
                horizontalDirection = -horizontalDirection;
                randomVerticalDirection = Random.Range(0.0f, 1.0f);
                break;
            case "up-left":
                randomVerticalDirection = Random.Range(-1.0f, 0.0f);
                break;
            case "up-right":
                horizontalDirection = -horizontalDirection;
                randomVerticalDirection = Random.Range(-1.0f, 0.0f);
                break;
            default:
                Debug.Log("Spawner not Found!");
                break;
        }

        return new Vector3(horizontalDirection, 0.0f, randomVerticalDirection);
    }
}
