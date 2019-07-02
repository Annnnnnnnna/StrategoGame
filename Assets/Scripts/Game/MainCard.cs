using UnityEngine;

public class MainCard : MonoBehaviour {

    [SerializeField]
    private Game controller;
    [SerializeField]
    private GameObject Card_Back;

    public void OnMouseDown()
    {
        if (Card_Back.activeSelf)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    public int _id { get; set; }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void ChangeSpriteVersion2(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
        Card_Back.SetActive(false);
    }
}
