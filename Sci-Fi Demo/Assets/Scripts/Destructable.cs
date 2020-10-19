using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject crateDesroyed;

    public void DestroyCrate()
    {
        GameObject prefub = Instantiate(crateDesroyed, transform.position, transform.rotation);
        Destroy(prefub, 10f);
        Destroy(gameObject);
    }
}
