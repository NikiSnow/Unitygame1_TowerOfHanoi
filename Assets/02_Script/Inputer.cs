using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inputer : MonoBehaviour
{
    private Camera Cam;
    public LayerMask layerMask;
    public GameObject currentRing;
    public Base currentBase;
    public Base targetBase; 


    void Start()
    {
        Cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            NewClickEvent();
        }  
    }

    private void NewClickEvent()
    {
        
        RaycastHit hit;
        Ray ray = Cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //coliderNameDetected = hit.collider.tag;
        }
        
        if(hit.collider.tag == "Base")
        {
            if(currentRing == null)
            {
                currentBase = hit.collider.GetComponent<Base>();
                if(currentBase.rings.Count > 0)
                {
                    currentBase.TakeHighest();
                }else
                {
                    currentBase = null;
                }  
            }else /// Если есть текущее кольцо в руках
            {
                targetBase = hit.collider.GetComponent<Base>();
                if(targetBase.rings.Count > 0)
                {
                    targetBase.Comparison(currentRing);
                }
                else 
                {
                    currentBase.PutOnNewPlace(hit.collider.GetComponent<Transform>(),targetBase); 
                    currentRing = null;
                } 
            }
 
        }

    }
 
    public void Realise()
    {
        currentBase.AddNewRings(currentRing);
        currentRing = null;
        currentBase = null;
    }
}