using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] string[] lines;
    [SerializeField] float textSpeed;

    int _index;
    bool _isTyping;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        _index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        _isTyping = true;
        textComponent.text = "";

        foreach (char c in lines[_index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        _isTyping = false;
    }

    void NextLine()
    {
        if(_index < lines.Length - 1)
        {
            _index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void OnDialogue(InputValue inputValue)
    {
        if (_isTyping)
        {
            //mostra a linha completa com o clique do botao
            StopAllCoroutines();
            textComponent.text = lines[_index];
            _isTyping = false;
        }
        else
        {
            //vai para a proxima linha de texto
            NextLine();
        }
    }
}
