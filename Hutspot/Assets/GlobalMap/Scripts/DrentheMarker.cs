using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Examples;
using Mapbox.Map;
using Mapbox.Unity.Map;

public class DrentheMarker : SpawnOnMap
{
    private GameObject _map;
    private GameObject[] _markerstToDisable;
    private GameObject _hunebedMarker;
    private GameObject _haringHappenMarker;

    public override void Start()
    {
        base.Start();
        _map = GameObject.FindWithTag("Map");
        _hunebedMarker = GameObject.FindGameObjectWithTag("Hunebed");
        _haringHappenMarker = GameObject.FindGameObjectWithTag("HaringHappen");
        _hunebedMarker.SetActive(false);
        _haringHappenMarker.SetActive(false);
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("MouseDown");
            // Reset ray with new mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject.tag == "Drenthe")
                {
                    _map.GetComponent<AbstractMap>().SetZoom(8.7f);
                    //map.GetComponent<AbstractMap>().SetCenterLatitudeLongitude(new Mapbox.Utils.Vector2d(52.9167f, 6.5833f));
                    _map.GetComponent<AbstractMap>().UpdateMap(new Mapbox.Utils.Vector2d(52.9167f, 6.5833f));
                    _hunebedMarker.SetActive(true);
                    _haringHappenMarker.SetActive(true);
                }
            }
        }

    }
}
