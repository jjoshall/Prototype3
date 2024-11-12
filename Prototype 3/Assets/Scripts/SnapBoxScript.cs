using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class SnapBoxScript : MonoBehaviour
{
    public string tagName;
    public float proximityToSnap = 0.5f;
    public bool isAttaching;
    List<GameObject> snapPoints; 
    // Start is called before the first frame update
    void Start()
    {
        snapPoints = GameObject.FindGameObjectsWithTag(tagName).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttaching)
        {
            transform.GetChild(0).GameObject().SetActive(false);
            transform.GetChild(1).GameObject().SetActive(false);
            transform.GetChild(2).GameObject().SetActive(false);
            transform.GetChild(3).GameObject().SetActive(false);
            transform.GetChild(4).GameObject().SetActive(false);
            snapObj(this.gameObject);
        }else{
            transform.GetChild(0).GameObject().SetActive(true);
            transform.GetChild(1).GameObject().SetActive(true);
            transform.GetChild(2).GameObject().SetActive(true);
            transform.GetChild(3).GameObject().SetActive(true);
            transform.GetChild(4).GameObject().SetActive(false);
        }
    }

    public void snapObj(GameObject obj)
    {
        foreach(GameObject point in snapPoints)
        {
            if (Vector3.Distance(point.transform.position, obj.transform.position) <= proximityToSnap)
            {
                obj.transform.position = point.transform.position;
                return;
            }
        }
    }
}
