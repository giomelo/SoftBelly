using System.Collections.Generic;
using System.IO;
using _Scripts.Helpers;
using _Scripts.Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Helpers
{
    /// <summary>
    /// Basic typewriter text for texts
    /// </summary>
    public class WriterText : MonoSingleton<WriterText>
    {
        private List<TextWriterSingle> _textWriterSingle = new List<TextWriterSingle>();
        public TextWriterSingle AddWriter(TextMeshProUGUI uiText, string textToWrite, float timerPerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd)
        {
            if (removeWriterBeforeAdd)
            {
                RemoveWriter(uiText);
            }

            TextWriterSingle textWriter =
                new TextWriterSingle(uiText, textToWrite, timerPerCharacter, invisibleCharacters);
            _textWriterSingle.Add(textWriter);
            return textWriter;
        }
        public void ResetWriter()
        {
            foreach (var text in _textWriterSingle)
            {
                text?.ResetWriter();
            }
        }
        
        public void RemoveWriter(TextMeshProUGUI uiText)
        {
            for (int i = 0; i < _textWriterSingle.Count; i++)
            {
                if (_textWriterSingle[i].GetText() != uiText) continue;
                _textWriterSingle.RemoveAt(i);
                i--;
            }
        }

        private void Update()
        {
            for (int i =0; i< _textWriterSingle.Count; i++)
            {
                var destroyInstance = _textWriterSingle[i].Update();

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
    
    /// <summary>
    /// Reset the text
    /// </summary>
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

            if (_uiText != null)
            {
                _uiText.text = text;
            }

            if (_characterIndex >= _textToWrite.Length)
            {
                _uiText = null;
                return true;
            }

        }

        return false;
    }

    public TextMeshProUGUI GetText()
    {
        return _uiText;
    }

    public bool IsActive()
    {
        return _characterIndex < _textToWrite.Length;
    }

    public void WriteAllAndDestroy()
    {
        _uiText.text = _textToWrite;
        _characterIndex = _textToWrite.Length;
        WriterText.Instance.RemoveWriter(_uiText);
    }
}