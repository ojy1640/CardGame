using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public GameObject card;
    public GameObject firstCard = null;
    public GameObject secondCard = null;
    public GameObject Endtext;
    public GameObject MatchFailText;
    public GameObject MatchSuccessText;

    public Animator anim;

    public AudioSource audioSource;
    public AudioClip match;
    public AudioClip wrong_answer;
    public AudioClip startmusic;

    public Text Timetext;
    public float time = 0.00f;
    float firstTime = 0.00f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(startmusic);

        Time.timeScale = 1.0f;
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10, 11, 11 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();

        for (int i = 0; i < 24; i++)
        {
            GameObject newCard = Instantiate(card);
            newCard.transform.parent = GameObject.Find("cards").transform;

            float x = (i % 4) * 1.25f - 2.0f; //0,1,2,3,4,5/6,7,8,9,10,11,
            float y = (i / 4) * 1.5f - 4.5f; //0,1,2,3,/,4,5,6,7,/,8,9,10,11/
            newCard.transform.position = new Vector3(x, y, 0);

            string rtanName = "rtan" + rtans[i].ToString();
            newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 30.0f)
        {
            Timetext.text = "30";
            Endtext.SetActive(true);
            Time.timeScale = 0.0f;
        }
        else if ((time >= 20.0f)&&(time <= 30.0f))
        {
            time += Time.deltaTime;
            Timetext.text = time.ToString("N2");
            Timetext.GetComponent<Text>().color = new Color(255f / 255f, (255f - (time - 20.0f) * 25.5f) / 255f, (255f - (time - 20.0f) * 25.5f) / 255f);
        }
        else
        {
            time += Time.deltaTime;
            Timetext.text = time.ToString("N2");
        }

        if (firstCard != null)
        {
            firstTime += Time.deltaTime;

            if (firstTime >= 4.50f)
            {
                firstCard.GetComponent<Card>().closeCard();  //firstcard flip after 5 second
                firstCard = null;
                firstTime = 0.00f;
            }
        }
    }

    void Awake()
    {
        I = this;
    }

    public void IsMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;

        if (firstCardImage == secondCardImage)
        {
            audioSource.PlayOneShot(match);

            firstCard.GetComponent<Card>().destroyCard();
            secondCard.GetComponent<Card>().destroyCard();

            int cardsLeft = GameObject.Find("cards").transform.childCount;


            int whoRU = Random.Range(0, 2);
            if (whoRU == 0)
            {
                MatchSuccessText.GetComponent<Text>().text = "이하택";
            }
            else
            {
                MatchSuccessText.GetComponent<Text>().text = "오준용";
            }
            MatchSuccessText.SetActive(true);
            Invoke("SuccessTextWait", 0.7f);

            if (cardsLeft == 2)
            {
                Endtext.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
        else
        {
            audioSource.PlayOneShot(wrong_answer);

            firstCard.GetComponent<Card>().closeCard();
            secondCard.GetComponent<Card>().closeCard();

            MatchFailText.SetActive(true);
            Invoke("FaitTextWait", 0.7f);
        }

        firstCard = null;
        secondCard = null;
    }

    public void SuccessTextWait()
    {
        MatchSuccessText.SetActive(false);
    }

    public void FaitTextWait()
    {
        MatchFailText.SetActive(false);
    }


}
