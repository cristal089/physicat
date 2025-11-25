using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetRollingController : MonoBehaviour
{
    Collider2D _collider2D;
    Animator _animator;

    void Awake()
    {
        _collider2D = GetComponentInChildren<Collider2D>();
        _animator = GetComponent<Animator>();

        _animator.enabled = true;
    }
}
