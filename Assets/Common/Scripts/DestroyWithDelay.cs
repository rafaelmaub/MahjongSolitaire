using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DestroyWithDelay : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private UnityEvent OnDestroyObject;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, delay);
    }

    private void OnDestroy()
    {
        OnDestroyObject?.Invoke();
    }
}
