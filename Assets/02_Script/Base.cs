using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    public Image winImage;
    private Inputer inputer;
    private GameObject ring;
    private GameObject topRing;   
    private Rigidbody body;

    public bool isFirstTower;
    public bool isLastTower;

    public GameObject[] ringsPrefab;
    public List<GameObject> rings = new List<GameObject>();
    [SerializeField]
    
    void Start()
    {
        winImage.enabled = false;
        
        inputer = GetComponentInParent<Inputer>();

        if(isFirstTower)
        {
            foreach (GameObject go in ringsPrefab)
            {
                rings.Add(go);
            } 
        }
    }

    public void AddNewRings(GameObject newRing)
    {
        rings.Add(newRing);

        if(isLastTower)CheckComplete();
    }

    private void CheckComplete()
    {
        if(rings.Count == 5)winImage.enabled = true;
    }

    public void TakeHighest()
    {
        topRing = rings[rings.Count - 1]; 
        topRing.transform.position = topRing.transform.position = new Vector3(topRing.transform.position.x,15f,topRing.transform.position.z);
        topRing.GetComponent<Rigidbody>().isKinematic = true; 
        inputer.currentRing = topRing;
        rings.Remove(topRing);
    }

    public void PutOnNewPlace(Transform target, Base targetBase)
    {
        //rings.Remove(topRing);
        targetBase.AddNewRings(topRing);

        topRing.transform.position = new Vector3(target.transform.position.x,15f,target.transform.position.z);
        topRing.GetComponent<Rigidbody>().isKinematic = false; 
        inputer.currentRing = null;
    }
    public void Comparison(GameObject upperRing)
    {  
        float above = upperRing.GetComponent<RingIndex>().index;
        float below = rings[rings.Count - 1].GetComponent<RingIndex>().index;

        if(above > below)
        {
            upperRing.GetComponent<Rigidbody>().isKinematic = false; 
            inputer.Realise();
            //print("Сравнение False класть нельзя");
        }else
        {
            PutOnThisBase(upperRing);
            
        }

    }
    public void PutOnThisBase(GameObject upperRing)
    {
        //print("Сравнение удачно положить можно");
        //rings.Remove(topRing);
        AddNewRings(upperRing);
        
        upperRing.transform.position = new Vector3(transform.position.x,15f,transform.position.z);
        upperRing.GetComponent<Rigidbody>().isKinematic = false; 
        inputer.currentRing = null;

        if(isLastTower)CheckComplete();
    }


}