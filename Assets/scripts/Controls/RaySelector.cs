using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySelector : MonoBehaviour
{
    public SelectorRay selectorRay;
    public Vector3 pointerPosition;
    public float zPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray thisRay = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //selectorRay.selectorRay = thisRay;
        //RaycastHit[] hits;
        //hits = Physics.RaycastAll(thisRay);
        //selectorRay.hits = hits;
        //for (int i = 0; i < hits.Length; i++)
        //{
        //    Debug.Log(hits[i].collider.gameObject);
        //}
        
        pointerPosition = Input.mousePosition;
        pointerPosition.z = zPoint;
        pointerPosition = Camera.main.ScreenToWorldPoint(pointerPosition);
        transform.LookAt(pointerPosition);

    }

}
