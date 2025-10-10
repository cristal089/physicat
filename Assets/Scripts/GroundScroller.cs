using UnityEngine;

public class GroundScroller : MonoBehaviour
{
    [SerializeField] GameObject groundPrefab;
    [SerializeField] float speed;
    bool _hasSpawnGround = false;

    private void Update()
    {
        if (Vector3.Distance(new Vector3(-0.108099997f, 1.16647005f, 0), transform.position) < 0.2f && !_hasSpawnGround)
        {
            Instantiate(groundPrefab, new Vector3(-0.108099997f + 20.473f, 1.16647005f, 0), Quaternion.identity);
            _hasSpawnGround = true;
        }
        else if(Vector3.Distance(new Vector3(-21f, 1.16647005f, 0), transform.position) < 0.2f)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime); ;
    }
}
