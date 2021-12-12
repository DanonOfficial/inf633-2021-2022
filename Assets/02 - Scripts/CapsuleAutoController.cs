using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleAutoController : MonoBehaviour {

    public float max_speed = 0.001f;
    protected Terrain terrain;
    protected CustomTerrain cterrain;
    protected float width, height;
    protected Animator animator;
    private bool isEating = false;
    TerrainData terrainData;
    void Start() {
        terrain = Terrain.activeTerrain;
        cterrain = terrain.GetComponent<CustomTerrain>();
        width = terrain.terrainData.size.x;
        height = terrain.terrainData.size.z;
        
        animator = this.GetComponentInChildren<Animator>();
        animator.SetBool("isWalking", true);
        animator.SetFloat("animSpeedMultiplier", 1.8f + 0.5f);
        //animator.Play("Idle");
        terrainData = Terrain.activeTerrain.terrainData;
    }

    void Update() {
        Vector3 scale = terrain.terrainData.heightmapScale;
        Transform tfm = transform;
        Vector3 v =  tfm.rotation * Vector3.forward * max_speed;

        if (isEating)
        {
            //animator.SetTrigger("isEating");
            isEating = false;
            return;
        } else if(v.magnitude > 0)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                return;
            }
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        Vector3 loc = tfm.position + v * 0.5f;
        if (loc.x < 0)
            loc.x += width;
        else if (loc.x > width)
            loc.x -= width;
        if (loc.z < 0)
            loc.z += height;
        else if (loc.z > height)
            loc.z -= height;
        loc.y = cterrain.getInterp(loc.x/scale.x, loc.z/scale.z) ;
        loc.y = terrainData.GetInterpolatedHeight(loc.x / width, loc.z / height) + 0.4f;
        Vector3 normal = cterrain.getNormal(loc.x / scale.x, loc.z / scale.z);

        Quaternion curRotation = tfm.rotation;
        Quaternion additionalRotation = Quaternion.FromToRotation(normal, tfm.up);
        curRotation *= additionalRotation;
        tfm.rotation = curRotation;
        tfm.position = loc;
    }

    public void setEatingAnimation()
    {
        isEating = true;
        animator.SetTrigger("isEating");
    }
}
