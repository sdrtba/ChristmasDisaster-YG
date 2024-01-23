using Unity.Mathematics;
using UnityEngine;

public class EntityHandler : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private float speed;

    private void Update()
    {
        gameObject.transform.position = Vector2.Lerp(gameObject.transform.position, new Vector2(0, 0), speed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        ObjectHandler.saves += 1;
        Destroy(gameObject);
        Instantiate(effect, transform.position, quaternion.identity);
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
