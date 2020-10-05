using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAround : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (this.gameObject.name == "Baseball(Clone)")
        {
            this.gameObject.transform.rotation = Quaternion.Euler(-110f, time * -100f, 0f);
        }

        else
        {
            this.gameObject.transform.rotation = Quaternion.Euler(0f, time * -100f, 0f);
        }

        

        
    }
}
