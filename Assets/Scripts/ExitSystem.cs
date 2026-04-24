using UnityEngine;

public class ExitSystem : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Reporter"))
        {
            Debug.Log("Reporter");
        }
        else if (other.CompareTag(""))
        {
            Debug.Log("");
        }
    }
}
