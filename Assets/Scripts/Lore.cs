using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lore : MonoBehaviour
{
    [SerializeField]private int NumberOfSentencesInParagraph = 4;
    private int index = 0;
    
    public Text text;

    public string[] lore;
    List<string> resultList = new List<string>();
    void Start()
    {
        DividingIntoParagraphs();
        text.text = resultList[0];
    }

    void TextReplace()
    {
        index++;

        if (index >= resultList.Count)
        {
            SceneManager.LoadScene(3);
        }

        else
        {
            text.text = resultList[index];
        }
    }

    public void DividingIntoParagraphs()
    {
        string temp = string.Join("",lore);
            
        string[] sentences = temp.Split(".");
        
        // Расставить точки в конце предложений(а то после Split они убираются)
        for (int i = 0; i < sentences.Length; i++)
        {
            sentences[i] += ".";
        }

        string resultString = "    ";
        StringBuilder TextWithFourSentences = new StringBuilder(resultString);

            // Вне условия просто аппендим в TextWithFourSentences 4 предложения. И когда мы добавили 4-е предложение
            // добавляем результат в resultList и обнуляем TextWithFourSentences(чтоб прошлый текст из 4 предложений удалился)
            int sentencesLenght = sentences.Length;
            for (int sentence = 0; sentence < sentences.Length; sentence++, sentencesLenght--)
            {
                // index != 0 добавил, потом что без него 0 элемент массива пустой будет.
                if(sentence % NumberOfSentencesInParagraph == 0 && sentence != 0)
                {
                    resultList.Add(TextWithFourSentences.ToString());
                    TextWithFourSentences = new StringBuilder(resultString);
                }

                /*
                Я объявил новую переменную, которая содержит в себе длину массива sentences
                Условно когда добавляется новое предложение(или же по другому с каждым тактом цикла) в TextWithFourSentences
                Этот sentencesLenght уменьшался. У меня алгоритм построен так, что если добавлятся в resultList
                TextWithFourSentences будет только тогда, когда index % NumberOfSentencesInParagraph == 0, а что делать если условно в массиве 6 предложений(и NumberOfSentencesInParagraph = 4)?
                Остальные предложения просто не занесутся в массив. По этому Я с каждым тактом цикла уменьшаю sentencesLenght,
                И когда sentencesLenght < NumberOfSentencesInParagraph(т.е в тексте остается меньше NumberOfSentencesInParagraph предложений), я начиная новый цикл и начинаю отсчет с sentencesLenght(т.е с конечного предложения)
                И до конца sentences и добавляю все оставшиеся предложения в TextWithFourSentences(предварительно его очистив конечно), и в итоге просто дабавляю TextWithFourSentences в resultList.
                Удачи в понимании)
                */
                if(sentencesLenght < NumberOfSentencesInParagraph)
                {
                    TextWithFourSentences = new StringBuilder(resultString);
                    for (int j = sentencesLenght; j < sentences.Length; j++)
                    {
                        TextWithFourSentences.Append(sentences[j]);
                    }
                    resultList.Add(TextWithFourSentences.ToString());
                    break;
                }

                TextWithFourSentences.Append(sentences[sentence]);       
            }



            for (int i = 0; i < resultList.Count; i++)
            {
                System.Console.WriteLine($"{i}) {resultList[i]}");
            }
    }
    

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TextReplace();
        }
    }
}
