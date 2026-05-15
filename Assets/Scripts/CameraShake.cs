using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 original = transform.localPosition;

        float timer = 0f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = original + new Vector3(x, y, 0);

            yield return null;

        }

        transform.localPosition = original;
    }
}
