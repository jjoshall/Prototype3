using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchControl : MonoBehaviour
{
    public Camera povCamera;
    public Camera OverCamera;
    public GameObject ObjParent;
    public string tagName;
    GameObject[] ObjChildren;
    // Start is called before the first frame update
    void Start()
    {
        povCamera.gameObject.SetActive(true);
        OverCamera.gameObject.SetActive(false);
        ObjChildren = GameObject.FindGameObjectsWithTag(tagName);
        setBlockMove(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("r")){
            Debug.Log("switch called");
            if(povCamera.gameObject.activeInHierarchy == true)
            {
                povCamera.gameObject.SetActive(false);
                OverCamera.gameObject.SetActive(true);
                setBlockMove(true);
                gameObject.GetComponent<PlayerController>().enabled = false;
            }else{
                povCamera.gameObject.SetActive(true);
                OverCamera.gameObject.SetActive(false);
                setBlockMove(false);
                gameObject.GetComponent<PlayerController>().enabled = true;
            }
        }
    }

    void setBlockMove(bool set){
        foreach(GameObject child in ObjChildren)
        {
            child.GetComponent<MoveBlocks>().enabled = set;
        }
    }
}
