using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube {get; private set; }
    public static MovingCube LastCube {get; private set; }

[SerializeField]
    private float moveSpeed = 1f;

    private void OnEnable()
    {
        if(LastCube == null)
        LastCube = GameObject.Find("Start").GetComponent<MovingCube>();

        CurrentCube = this;
        GetComponent<Renderer>().material.color=GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f),UnityEngine.Random.Range(0, 1f),UnityEngine.Random.Range(0, 1f) ) ;
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = transform.position.z - LastCube.transform.position.z;

        float direction = hangover > 0 ? 1f : -1f;
        SplitCubeOnZ(hangover, direction);
    }

        private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZsize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZsize;
    
        float newZPosition = LastCube.transform.position.z + (hangover / 2);
    transform.localScale = new Vector3( transform.localScale.x,transform.localScale.y, newZsize);
    transform.position = new Vector3(transform.position.x,transform.position.y,newZPosition);

    float cubeEdge = transform.position.z + (newZsize/ 2f * direction);
        float fallingBlockZposition = cubeEdge + fallingBlockSize / 2f * direction;

    SpawnDropCube(fallingBlockZposition, fallingBlockSize);
    }

        private void SpawnDropCube(float fallingBlockZposition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y,fallingBlockSize);
        cube.transform.position = new Vector3(transform.position.x,transform.position.y,fallingBlockZposition);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = cube.GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);



    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
