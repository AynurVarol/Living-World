using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextSpawner : MonoBehaviour
{
    public static FloatingTextSpawner Instance;
    public GameObject textPrefab;
    public Canvas canvas;

    private void Awake()
    {
        Instance = this;
    }

    public void Spawn(float value, Vector3 worldPos)
    {
        GameObject obj = Instantiate(textPrefab, canvas.transform);
        obj.transform.position = Camera.main.WorldToScreenPoint(worldPos);

        Text txt = obj.GetComponent<Text>();
        txt.text = value > 0 ? "+" + value.ToString("F0") : value.ToString("F0");

        txt.color = value > 0 ? Color.green : Color.red;
    }


}
