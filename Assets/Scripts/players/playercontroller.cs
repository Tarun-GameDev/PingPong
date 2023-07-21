using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    [SerializeField] bool player1 = true;

    Vector3 pos;

    public float speed = 20f; 

    void Update()
    {
        pos = transform.position;
        if(player1)
            pos.y += Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        else
            pos.y += Input.GetAxisRaw("arrowkeys") * speed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, -13f, 13f);
        transform.position = pos;
    }
}
