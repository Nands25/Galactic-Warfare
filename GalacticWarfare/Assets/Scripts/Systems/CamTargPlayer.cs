using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerac : MonoBehaviour
{
    private Transform alvo;
    

    private Vector3 direcao;

    private float y;

    private float x;
    // Start is called before the first frame update
    void Start()
    {
        alvo = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Local();    
    }

    void Local()
    {
        y = alvo.position.y;
        x = alvo.position.x;
        if (y > 2)
            y = 2;
        if (y < -2)
            y = -2;
        if (x > 10)
            x = 10;
        if (x < -10)
            x = -10;

        direcao = new Vector3(x, y, -15f);
        transform.position = direcao;
    }
}