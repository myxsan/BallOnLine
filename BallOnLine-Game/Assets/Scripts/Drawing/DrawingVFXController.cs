using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingVFXController : MonoBehaviour
{
    //public GameObject particle;
    public ParticleSystem particle;
    Vector2 pos;
    private void OnEnable()
    {
        particle.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        particle.gameObject.SetActive(false);
    }

    void Update()
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        particle.transform.position = pos;
    }
}
