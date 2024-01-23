using System.Collections;
using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject entity;
    [SerializeField] private float defInterval;
    [SerializeField] private float speed;
    private float interval;

    private void OnEnable()
    {
        interval = defInterval;
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(entity, spawners[Random.Range(0, spawners.Length)].transform);
            yield return new WaitForSeconds(interval);
            if (interval - speed >= 0.5) interval -= speed;
        }
    }
}
