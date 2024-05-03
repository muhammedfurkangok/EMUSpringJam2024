using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float SizeNormalized)
    {
        bar.localScale = new Vector3(SizeNormalized, 1f);
    }
}
