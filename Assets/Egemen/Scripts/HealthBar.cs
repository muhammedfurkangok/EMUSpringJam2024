using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    void Awake()
    {
        bar = transform.Find("Bar");
        Debug.Log(bar);
    }

    public void SetSize(float SizeNormalized)
    {
        if (SizeNormalized > 1f)
        {
            Debug.LogWarning("1den büyük yollama amk");
        }
        bar.localScale = new Vector3(SizeNormalized, 1f);
    }
}
