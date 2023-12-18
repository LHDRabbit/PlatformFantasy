using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{

    [SerializeField] private TMP_Text gemText;
    [SerializeField] private TMP_Text cherryText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gemText.text = "Gem Count: " + PlayerMovement.gemCount.ToString();
        cherryText.text = "Cherry Count: " + PlayerMovement.cherryCount.ToString();
    }
}
