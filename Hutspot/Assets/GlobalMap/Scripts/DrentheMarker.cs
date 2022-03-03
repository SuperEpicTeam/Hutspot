using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;

public class DrentheMarker : SpawnOnMap
{
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                print("test");
            }
        }

    }
}
