using System.Collections.Generic;
using _Scripts.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Helpers
{
    public class WriterText : MonoSingleton<WriterText>
    {
        private List<TextWriterSingle> _textWriterSingle = new List<TextWriterSingle>();
        public void AddWriter(TextMeshProUGUI uiText, string textToWrite, float timerPerCharacter, bool invisibleCharacters)
        {
            _textWriterSingle.Add(new TextWriterSingle(uiText, textToWrite, timerPerCharacter, invisibleCharacters));
        }
        public void ResetWriter()
        {
            foreach (var text in _textWriterSingle)
            {
                text?.ResetWriter();
            }
        }

        private void Update()
        {
            for (int i =0; i< _textWriterSingle.Count; i++)
            {
                bool destroyInstance = _textWriterSingle[i].Update();

                if (!destroyInstance) continue;
                _textWriterSingle.RemoveAt(i);
                i--;
            }
            
        }
    }
}

public class TextWriterSingle
{
    private float _timer;
    private TextMeshProUGUI _uiText;
    private string _textToWrite;
    private float _timePerCharacter;
    private int _characterIndex;
    private bool _invisibleCharacters;

    public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timerPerCharacter, bool invisibleCharacters)
    {
        _uiText = uiText;
        _textToWrite = textToWrite;
        _timePerCharacter = timerPerCharacter;
        _characterIndex = 0;
        _invisibleCharacters = invisibleCharacters;
    }

    public void ResetWriter()
    {
        _characterIndex = 0;
        _uiText = null;
    }
    //return true on complete
    public bool Update()
    {
        _timer -= Time.deltaTime;

        while (_timer <= 0f)
        {
            //display next character
            _timer += _timePerCharacter;
            _characterIndex++;
            string text = _textToWrite.Substring(0, _characterIndex);

            if (_invisibleCharacters)
            {
                text += "<color=#00000000>" + _textToWrite.Substring(_characterIndex) + "</color>";
            }
            
            _uiText.text = text;

            if (_characterIndex >= _textToWrite.Length)
            {
                _uiText = null;
                return true;
            }

        }

        return false;
    }
}