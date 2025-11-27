using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

//armazena o resistor e se ele esta ativo ou nao (caso ja tenha queimado)
[System.Serializable]
public class Resistor
{
    public float resistance;
    public bool isActive = true;
}

public class CircuitUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tensionText;          // V
    [SerializeField] TextMeshProUGUI currentText;          // I
    [SerializeField] TextMeshProUGUI[] resistorTexts;      // R
    [SerializeField] TextMeshProUGUI invResistanceText;    //inverso de R

    [SerializeField] float step = 1f; //intervalo de valor adicionado ou subtraido da tensao a cada input do usuario

    float tension = 20f;

    [SerializeField] List<Resistor> resistors = new List<Resistor>();

    void Start()
    {
        //atualiza os valores iniciais da tela
        UpdateUI();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        //diminui a tensao ao apertar z
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            tension -= step;
            tension = Mathf.Max(0, tension); //garante que nao havera tensao negativa
            UpdateUI();
        }

        //aumenta a tensao ao apertar x
        if (Keyboard.current.xKey.wasPressedThisFrame)
        {
            tension += step;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        tensionText.text = Mathf.Ceil(tension).ToString();

        var values = CalculateCurrent();
        currentText.text = Mathf.Ceil(values.c).ToString();
        invResistanceText.text = values.i.ToString("F2");

        UpdateResistorTexts();
    }

    (float c, float i) CalculateCurrent()
    {
        //as resistencias estao em paralelo entao o calculo eh com o inverso de cada resistencia
        float invSum = 0f;

        foreach (var r in resistors)
        {
            if (r.isActive && r.resistance > 0f)
                invSum += 1f / r.resistance;
        }
        //se nao ha mais resistores ativos
        if (invSum <= 0f)
            return (0f, 0f);

        float eqResistance = 1f / invSum;
        float current = tension / eqResistance;

        return (current, invSum);
    }

    void UpdateResistorTexts()
    {
        for (int i = 0; i < resistors.Count; i++)
        {
            if (resistors[i].isActive)
            {
                resistorTexts[i].text = resistors[i].resistance + "";
                resistorTexts[i].color = Color.white;
            }
            else
            {
                //resistor queimado
                resistorTexts[i].text = "X";
                resistorTexts[i].color = Color.red;
            }
        }
    }

    //um dos resistores queima toda vez que o jogador usa o turbo
    void ExplodeResistor(int index)
    {
        if (index < 0 || index >= resistors.Count) return;

        resistors[index].isActive = false;

        UpdateUI();
    }

    public void ExplodeRandomResistor()
    {
        //lista dos resistores ativos
        List<int> activeIndexes = new List<int>();

        for (int i = 0; i < resistors.Count; i++)
            if (resistors[i].isActive)
                activeIndexes.Add(i);

        if (activeIndexes.Count == 0)
            return;

        int target = activeIndexes[Random.Range(0, activeIndexes.Count)];
        ExplodeResistor(target);
    }
}
