using UnityEngine;

public class PourButtonController : MonoBehaviour
{
    private float pressTime;
    public PouringGame pouringGameScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void StartTimer()
    {
        pressTime = Time.time;
        pouringGameScript.SetPourImageActive(true);
    }

    public void EndTimer()
    {
        float holdDuration = Time.time - pressTime;
        Debug.Log($"Held for: {holdDuration:F2} seconds");
        pouringGameScript.Pour(holdDuration);
        pouringGameScript.SetPourImageActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
