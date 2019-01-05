using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public Text toolTipInfo;
    int layer;



    // Start is called before the first frame update
    void Start()
    {
        toolTipInfo.enabled = false;
        layer = LayerMask.GetMask("Interactable");
        Debug.Log("Layer: " + layer.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000.0f, layer)) 
        {
           ShowInfo(hit.collider.gameObject.GetComponent<Interactable>().interactableName);
        } else 
        {
            toolTipInfo.enabled = false;
        }

    }

    void ShowInfo(string name)
    {
        
        toolTipInfo.text = name;
        gameObject.transform.position = Input.mousePosition;
        toolTipInfo.enabled = true;
    }
}
