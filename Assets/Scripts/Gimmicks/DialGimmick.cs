using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DialGimmick : MonoBehaviour
{
    [SerializeField] int _lenght = 5;
    [SerializeField] bool _isOperation = false;
    [SerializeField] TextMeshProUGUI _textInput;
    [SerializeField] TextMeshProUGUI _textAns;
    private string text = "";

    char[] _answer;
    char[] _dial;
    int selct = 0;

    [SerializeField] PlayerMove plyer;
    [SerializeField] DoorMove _door;
    [SerializeField] Image[] _dialImage;
    float _anglePerChar = 360f / 26f;
    void Start()
    {
        _textInput.gameObject.SetActive(false);
        _textAns.gameObject.SetActive(false);
        for (int i = 0; i < _dialImage.Length; i++)
        {
            _dialImage[i].gameObject.SetActive(false);
        }
        _answer = new char[_lenght];
        _dial = new char[_lenght];
        string chars = "abcdefghijklmnopqrstuvwxyz";
        //答えの生成
        for (int i = 0; i < _answer.Length; i++)
        {
            _answer[i] += chars[Random.Range(0,chars.Length)];
            _dial[i] = 'a';
            Debug.Log($"{i}:{_answer[i]}");
        }
        _textAns.text = new string(_answer);
    }
    void Update()
    {
        if (_isOperation)
        {
            //文字の変更
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                LeftRotate(selct);
                text = $"Dial{selct} = {_dial[selct]}";
                _textInput.text = text;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RightRotate(selct);
                text = $"Dial{selct} = {_dial[selct]}";
                _textInput.text = text;
            }
            
            //ダイヤルの変更
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                selct++;
                
                if (selct >= _lenght)
                {
                    selct = 0;
                }
                UpdateDialView();
                text = $"Dial{selct} = {_dial[selct]}";
                _textInput.text = text;
                Debug.Log($"Dial{selct} = {_dial[selct]}");
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                selct--;
                
                if (selct < 0)
                {
                    selct = _lenght - 1;
                }
                UpdateDialView();
                text = $"Dial{selct} = {_dial[selct]}";
                _textInput.text = text;
                Debug.Log($"Dial{selct} = {_dial[selct]}");
            }
            //決定
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(new string(_dial));
                if (_isDoorMove())
                {
                    Debug.Log("一致");
                    _door.CloseDoor();
                    //_door.OpenDoor();

                    for (int i = 0; i < _dialImage.Length; i++)
                    {
                        _dialImage[i].gameObject.SetActive(false);
                    }
                    _textInput.gameObject.SetActive(false);
                    _textAns.gameObject.SetActive(false);
                    _isOperation = false;
                    plyer._isMoving = true;
                    //ドアが閉まるアニメーションを動かす
                }
                else if (!_isDoorMove())
                {
                    Debug.Log("不一致");
                }

            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Reporter"))
        {
            if (!_isOperation)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    UpdateDialView();
                    _textInput.gameObject.SetActive(true);
                    _textAns.gameObject.SetActive(true);
                    _isOperation = true;
                    plyer._isMoving = false;
                    text = $"Dial{selct} = {_dial[selct]}";
                    _textInput.text = text;
                }
            }
        }
    }

    void UpdateDialView()
    {
        for (int i = 0; i < _dialImage.Length; i++)
        {
            bool active = (i == selct);

            _dialImage[i].gameObject.SetActive(active);
            Debug.Log($"Dial{i} = {active}");
        }
    }

    void LeftRotate(int num)
    {
        _dial[num]++;
        if (_dial[num] > 'z')
        {
            _dial[num] = 'a';
        }
        _dialImage[num].rectTransform.Rotate(0, 0, _anglePerChar);
    }
    void RightRotate(int num)
    {
        _dial[num]--;
        if (_dial[num] <= 'a')
        {
            _dial[num] = 'z';
        }
        _dialImage[num].rectTransform.Rotate(0, 0, -_anglePerChar);
    }
    //コード一致時の処理
    bool _isDoorMove()
    {
        for (int i = 0; i < _lenght; i++)
        {
            if (_dial[i] != _answer[i])
            {
                return false;
            }
        }
        return true;
    }
}
