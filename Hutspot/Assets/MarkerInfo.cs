using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkerInfo : MonoBehaviour
{
    [SerializeField] private Text _title;
    [SerializeField] private Text _paragraph;
    [SerializeField] private Text _collectables;

    // Start is called before the first frame update
    void Awake()
    {
        //GetComponentInChildren<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        StartCoroutine(test());
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponentInChildren<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        GetComponentInChildren<Canvas>().enabled = false;
    }

    public void SetMarkerInfo(string title, string paragraph, int collectables)
    {
        _title.text = title;
        _paragraph.text = paragraph;
        _collectables.text = "Collectables: " + collectables.ToString();
    }

    public void ShowInfo()
    {
        if (GetComponentInChildren<Canvas>().enabled)
        {
            HideInfo();
        }
        else
        {
            GetComponentInChildren<Canvas>().enabled = true;
            GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
    }

    public void HideInfo()
    {
        GetComponentInChildren<Canvas>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
    }
}
