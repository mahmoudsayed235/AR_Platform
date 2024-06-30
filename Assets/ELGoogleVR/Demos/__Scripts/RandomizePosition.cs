using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    public bool randomizeX;
    public bool randomizeY;
    public bool randomizeZ;

    public float maxX;
    public float maxY;
    public float maxZ;

    void Start ()
    {
        gameObject.transform.localPosition = new Vector3(
            randomizeX ? Random.Range(-maxX, maxX) : gameObject.transform.localPosition.x,
            randomizeY ? Random.Range(-maxY, maxY) : gameObject.transform.localPosition.y,
            randomizeZ ? Random.Range(-maxZ, maxZ) : gameObject.transform.localPosition.z
            );
	}
}
