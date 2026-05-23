using UnityEngine;

public class CodeGimmick : MonoBehaviour
{
    [SerializeField] bool isTyping = false;
    private string text = "";
    [SerializeField] PlayerMove player;
    void Start()
    {
    }

    void Update()
    {
        if (isTyping)
        {
            text = "";
            string _input = Input.inputString;

            foreach (char c in _input)
            {
                // BackSpace
                if (c == '\b')
                {
                    if (text.Length > 0)
                    {
                        text = text.Substring(0, text.Length - 1);
                        
                    }
                }
                // Enter
                else if (c == '\n' || c == '\r')
                {
                    Debug.Log("åàíË: " + text);
                    isTyping = false;
                    player._isMoving = true;
                }
                // í èÌï∂éö
                else
                {
                    text += c;
                    Debug.Log("ì¸óÕ: " + text);
                }

            }
            //if (Input.GetKeyDown(KeyCode.Return))
            //{
            //    isTyping = false ;
            //}
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Reporter"))
        {
            if (!isTyping)
            {
                //Debug.Log("ÉGÉäÉAì‡");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isTyping = true;
                    player._isMoving = false;
                }
            }
        }
    }
}
