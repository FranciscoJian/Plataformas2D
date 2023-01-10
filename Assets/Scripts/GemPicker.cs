using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPicker : MonoBehaviour
{
    [SerializeField]
    private int Gems;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gem")
        {
            Gems++;
            collision.gameObject.SetActive(false);
        }
    }
}
