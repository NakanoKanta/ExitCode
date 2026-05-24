using TMPro;
using UnityEngine;

public class CodeGimmick : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] TextMeshProUGUI _textAns;
    [SerializeField] public bool _isMoving = false;
    [SerializeField] bool isTyping = false;
    private string text = "";
    private string _answer;
    private bool skipInputFrame = false;

    private string[] _randomTexts = new string[5];

    [SerializeField] PlayerMove player;
    void Start()
    {
        _answer = RandomString(5);
        Debug.Log(_answer);
        _textMeshPro.gameObject.SetActive(false);
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
                    _textMeshPro.text = text;
                }
                // Enter
                else if (c == '\n' || c == '\r')
                {
                    Debug.Log("決定: " + text);
                    if (text == _answer)
                    {
                        Debug.Log("一致！");
                        _isMoving = true;
                    }
                    else 
                    {
                        Debug.Log("不一致");
                    }
                    isTyping = false;
                    player._isMoving = true;
                    _textMeshPro.gameObject.SetActive(false);
                    _textAns.gameObject.SetActive(false);
                    text = "";
                    _textMeshPro.text = text;
                }
                // 通常文字
                else
                {
                    text += c;
                    _textMeshPro.text = text;
                    Debug.Log("入力: " + text);
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
                    _textMeshPro.gameObject.SetActive(true);
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
