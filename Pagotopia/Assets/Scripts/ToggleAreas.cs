using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAreas : MonoBehaviour
{
    GameObject cells;
    List<Transform> Children;
    List<Transform> GrandChildren;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("setList", 3f);

    }

    void setList()
    {
        Debug.Log("i started");
        cells = GameObject.Find("Cells");
        Transform[] allChildren = cells.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            
            Children.Add(child);
            Debug.Log(allChildren);
            foreach (Transform grandchild in Children)
            {
                GrandChildren.Add(grandchild);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggle()
    {
        foreach (Transform area in GrandChildren)
        {
            area.gameObject.active = !area.gameObject.active;
        }

    }
}
