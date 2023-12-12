using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class GemManager : MonoBehaviour
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
        gemText.text = "Gem Count: " + Shohei_PlayerController.gemCount.ToString();
        cherryText.text = "Cherry Count: " + Shohei_PlayerController.cherryCount.ToString();
    }
}
