using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject gameObject;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 15, -25);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, gameObject.transform.position + offset, 0.5f);
       // transform.rotation = Quaternion.Lerp(Quaternion.identity, gameObject.transform.rotation, 0.5f);
    }
}
