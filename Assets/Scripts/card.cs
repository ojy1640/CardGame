using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator anim;

    public AudioClip flip;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void openCard()
    {
        audioSource.PlayOneShot(flip);

        if (transform.Find("back/isColorful").gameObject.activeSelf == true)
        {
            anim.SetBool("isColorfulFlip", true);
            anim.SetBool("isOpen", true);
            if (GameManager.I.firstCard == null)
            {
                GameManager.I.firstCard = gameObject;
            }
            else
            {
                GameManager.I.secondCard = gameObject;
                GameManager.I.IsMatched();
            }
            transform.Find("back/isColorful").gameObject.SetActive(false);
        }
        else
        {
            anim.SetBool("isOpen", true);

            if (GameManager.I.firstCard == null)
            {
                GameManager.I.firstCard = gameObject;
            }
            else
            {
                GameManager.I.secondCard = gameObject;
                GameManager.I.IsMatched();
            }
        }
    }
    public void destroyCard()
    {
        Invoke("destroyCardInvoke", 0.5f);
    }
    private void destroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void closeCard()
    {
        Invoke("closeCardInvoke", 0.5f);
    }

    private void closeCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("front").gameObject.SetActive(false);
        transform.Find("back").gameObject.SetActive(true);

    }
}
