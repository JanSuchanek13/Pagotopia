using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAreas : MonoBehaviour
{
    public Transform cells;
    List<Transform> Children = new List<Transform>();
    List<Transform> GrandChildren = new List<Transform>();
    List<Transform> GrandGrandChildren = new List<Transform>();

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

        for (int i = 0; i < GrandChildren.Count; i++)
        {
            //Debug.Log("How many: " + Children.Count);

            foreach (Transform grandgrandchild in GrandChildren[i])
            {
                GrandGrandChildren.Add(grandgrandchild.transform);

            }

        }
    }

    public void toggle()
    {
        foreach (Transform area in GrandChildren)
        {
            //Debug.Log("geschafft");
            area.gameObject.active = !area.gameObject.active;
        }

    }

    public void UpdateShaders()
    {
        foreach (Transform area in GrandGrandChildren)
        {
            //Debug.Log("geschafft");
            area.gameObject.GetComponent<MeshRenderer>().enabled = false;


        }

        foreach (Transform child in cells)
        {
            child.gameObject.GetComponent<ActivateCell>().hasEnergy = false;
            child.gameObject.GetComponent<ActivateCell>().hasEnvironment = false;
            child.gameObject.GetComponent<ActivateCell>().hasHappiness = false;
            child.gameObject.GetComponent<ActivateCell>().hasNeighbor = false;
        }
    }
}
