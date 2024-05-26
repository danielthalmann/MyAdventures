using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Selectable : MonoBehaviour
{

    public Material selectMaterial;
    public GameObject selectGameObject;

    private bool selected = false;


    // Start is called before the first frame update
    void Start()
    {
        if (selectGameObject == null)
            selectGameObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {

        if (!selected)
        {
            Renderer render = selectGameObject.GetComponent<Renderer>();

            Material[] materialsArray = new Material[(render.GetComponent<Renderer>().materials.Length + 1)];
            render.GetComponent<Renderer>().materials.CopyTo(materialsArray, 0);
            materialsArray[materialsArray.Length - 1] = selectMaterial;

            render.GetComponent<Renderer>().materials = materialsArray;
            selected = true;
        }

    }

   

    public void OnMouseExit()
    {

        if (selected)
        {
            Renderer render = selectGameObject.GetComponent<Renderer>();

            Material[] materialsArray = new Material[(render.GetComponent<Renderer>().materials.Length - 1)];
            for (int i = 0; i < render.GetComponent<Renderer>().materials.Length - 1; i++)
            {
                materialsArray[i] = render.GetComponent<Renderer>().materials[i];
            }

            render.GetComponent<Renderer>().materials = materialsArray;
            selected = false;
        }
    }
}
