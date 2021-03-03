using System.Collections;
using UnityEngine;

namespace Tools
{
    public class SelfDestroyer : MonoBehaviour
    {
        [SerializeField] private float timeToDestroy = 1;

        private void Awake()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(timeToDestroy);
            Destroy(gameObject);
        }
    }
}