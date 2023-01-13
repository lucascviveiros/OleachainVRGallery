using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePaticleScale : MonoBehaviour
{
    private float scale = 40.0f;
    public ParticleSystem[] particleSystemList;

    void Start()
    {
        foreach (var ps in particleSystemList)
        {
            var main = ps.main;
            main.scalingMode = ParticleSystemScalingMode.Local;
            ps.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
