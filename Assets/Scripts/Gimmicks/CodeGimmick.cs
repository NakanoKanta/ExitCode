using TMPro;
using UnityEngine;

public class CodeGimmick : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textInput;
    [SerializeField] TextMeshProUGUI _textAns;
    [SerializeField] bool isTyping = false;
    private string text = "";
    private string _answer;
    private bool skipInputFrame = false;

    private string[] _randomTexts = new string[5];

    [SerializeField] PlayerMove player;
    [SerializeField] GuardianMove Guardian;
    void Start()
    {
        _answer = RandomString(5);
        Debug.Log(_answer);

        _textInput.gameObject.SetActive(false);
        _textAns.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isTyping)
        {
            if (skipInputFrame)
            {
                skipInputFrame = false;
                return;
            }
            string _input = Input.inputString;
            _textAns.text = _answer;

            foreach (char c in _input)
            {
                // BackSpace
                if (c == '\b')
                {
                    if (text.Length > 0)
                    {
                        text = text.Substring(0, text.Length - 1);
                        
                    }
                    _textInput.text = text;
                }
                // Enter
                else if (c == '\n' || c == '\r')
                {
                    Debug.Log("決定: " + text);
                    if (text == _answer)
                    {
                        Debug.Log("一致！");
                        Guardian._isMove = true;
                    }
                    else 
                    {
                        Debug.Log("不一致");
                    }
                    isTyping = false;
                    player._isMoving = true;
                    _textInput.gameObject.SetActive(false);
                    _textAns.gameObject.SetActive(false);
                    text = "";
                    _textInput.text = text;
                }
                // 通常文字
                else
                {
                    text += c;
                    _textInput.text = text;
                    Debug.Log("入力中");
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Reporter"))
        {
            if (!isTyping)
            {
                //Debug.Log("エリア内");
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isTyping = true;
                    player._isMoving = false;
                    skipInputFrame = true;
                    _textInput.gameObject.SetActive(true);
                    _textAns.gameObject.SetActive(true);
                }
            }
        }
    }
    string RandomString(int length)
    {
        string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        string result = "";
        for (int i = 0; i < length; i++)
        {
            result += chars[Random.Range(0, chars.Length)];
        }
        return result;
    }
}
