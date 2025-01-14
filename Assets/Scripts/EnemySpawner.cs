﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject scripter;
    public GameObject player;
    public GameObject Crab;
    public GameObject Blob;
    public GameObject Eye;
    public GameObject cafetera;
    private Camera cam;
    public int spawnedCount;
    private float camwidth;
    private float height;

    public void Start()
    {
        cam = transform.parent.gameObject.GetComponent<Camera>();
       height = 2f * cam.orthographicSize;
       camwidth = height * cam.aspect;
    }

  public void spawnEnemy(float cadencyMult, int type)
    {
        if (type == 0)
        {
            Vector3 position = new Vector3(Random.Range(-camwidth / 2,camwidth/2), Random.Range(0 , height/2), 0);
            GameObject crab = Instantiate(Crab, position, this.transform.rotation);
            crab.GetComponent<LivesCount>().scripter = scripter;
            crab.GetComponent<shieldDestroy>().scripter = scripter;
            crab.GetComponent<CrabEnemy>().Player = player;
            crab.GetComponent<CrabEnemy>().cadency = crab.GetComponent<CrabEnemy>().cadency*cadencyMult;
            crab.transform.parent = transform;
        }
        else if (type == 1)
        {
            Vector3 position = new Vector3(Random.Range(-camwidth / 2, camwidth / 2), Random.Range(0, height / 2), 0);
            GameObject blob = Instantiate(Blob, position, this.transform.rotation);
            blob.GetComponent<LivesCount>().scripter = scripter;
            blob.GetComponent<FlyingBlob>().cadency = blob.GetComponent<FlyingBlob>().cadency*cadencyMult;
            blob.transform.parent = transform;
        }
        else if (type == 2)
        {
            Vector3 position = new Vector3(Random.Range(-camwidth / 2, camwidth / 2), Random.Range(0, height / 2), 0);
            GameObject eye = Instantiate(Eye, position, this.transform.rotation);
            eye.GetComponent<LivesCount>().scripter = scripter;
            eye.GetComponent<FlyingEye>().Player = player;
            eye.GetComponent<FlyingEye>().scripter = scripter;
            eye.GetComponent<shieldDestroy>().scripter = scripter;
            eye.transform.parent = transform;
        }else if (type == 3)
        {
          Vector3 position = new Vector3(Random.Range(-camwidth / 2, camwidth / 2), Random.Range(0, height / 2), 0);
          GameObject coffe = Instantiate(cafetera, position, this.transform.rotation);
            coffe.GetComponent<LivesCount>().scripter = scripter;
            coffe.GetComponent<shieldDestroy>().scripter = scripter;
            coffe.GetComponent<CrabEnemy>().Player = player;
            coffe.GetComponent<CrabEnemy>().cadency = coffe.GetComponent<CrabEnemy>().cadency*cadencyMult;
            coffe.transform.parent = transform;
        }

    }

}
