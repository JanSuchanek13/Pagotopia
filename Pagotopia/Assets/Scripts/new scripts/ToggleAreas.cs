using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAreas : MonoBehaviour
{
    public Transform cells;
    List<Transform> Children = new List<Transform>();
    List<Transform> GrandChildren = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        Invoke("setList", 3f);

    }

    void setList()
    {
        //Debug.Log("start");
        foreach (Transform child in cells)
        {
            Children.Add(child.transform);
        }

        for (int i = 0; i < Children.Count; i++)
        {
            //Debug.Log("How many: " + Children.Count);

            foreach (Transform grandchild in Children[i])
            {
                GrandChildren.Add(grandchild.transform);
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
            //Debug.Log("geschafft");
            area.gameObject.active = !area.gameObject.active;
        }

    }
}
