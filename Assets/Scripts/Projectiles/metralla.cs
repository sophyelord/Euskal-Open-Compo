﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class metralla : MonoBehaviour
{
  public float speed = -100f;
  private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
      rb = gameObject.GetComponent<Rigidbody2D>();
      rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update(){}

    private void OnTriggerEnter2D(Collider2D hitInfo) {
      if (hitInfo.transform.tag == "player" || hitInfo.transform.tag == "ground"){
          Destroy(gameObject);
      }
    }
}
