using UnityEngine;

public class GlassTriggerBottom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glass"))
        {
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
