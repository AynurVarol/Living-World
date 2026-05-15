using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloatingTextSimple : MonoBehaviour
{
    float speed = 50f;
    float lifeTime = 1f;

    Text txt;
    Color startColor;

    private void Start()
    {
        txt = GetComponent<Text>();
        startColor = txt.color;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        //fade out
        float alpha = lifeTime / 1f;
       txt.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
