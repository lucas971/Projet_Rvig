using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiIntroGestion : MonoBehaviour
{
    public List<GameObject> pages;
    public GameObject nextCanvas;
    private int currentPage = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(currentPage > 0 && Input.GetKeyDown("q") && currentPage <= pages.Count)
        {
            pages[currentPage--].SetActive(false);
            pages[currentPage].SetActive(true);
        }
        if(Input.GetKeyDown("d") && currentPage < pages.Count)
        {
            pages[currentPage++].SetActive(false);
            if (currentPage < pages.Count)
                pages[currentPage].SetActive(true);
            else nextCanvas.SetActive(true);
        }
    }
}
