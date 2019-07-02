using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    static int countQeeue = 0;
    const float offsetX = 2f;
    const float offsetY = 1.9f;

    public MainCard originalCard;
    public Sprite[] images;
    [SerializeField]
    private TextMesh scoreLabel;

    NewGame game = new NewGame();
    NewMinMax ab;
    NewMinMax mx;
    GameMode gameMode;
    List<MainCard> squares = new List<MainCard>();
    int _score = 0;
    int N;
    
    void Start()
    {
        string elements = Menu.elem;
        N = int.Parse(elements);
        gameMode = (GameMode)Enum.Parse(typeof(GameMode),Menu.mode);
        Vector3 startPos = originalCard.transform.position;
        int[] numbers = { 0, 1 };

        ab = new NewMinMax(N, AlgorithmType.alphabeta, 3, Heuristic.kraw);
        mx = new NewMinMax(N, AlgorithmType.minmax, 3, Heuristic.kraw);

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                    card = originalCard;
                else
                    card = Instantiate(originalCard) as MainCard;

                card.ChangeSprite(j + N * i, images[1]);
                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
                squares.Add(card);//brak przy pvp 
            }
        }
        game.Init(N);
        if (gameMode == GameMode.ComputerVsComputer)
            WaitFor();
    }

    private IEnumerator Wait(int temp)
    {
        print("wait");
        yield return new WaitForSeconds(5.0f);
        CardRevealedByComputer(temp);      
    }
        private void WaitFor()// by computer
    {

        if (game.notassigned.Count > 0)
        {
            int temp=0;
            if (countQeeue % 2 == 0 && gameMode == GameMode.ComputerVsComputer)
            { temp = ab.alfabeta(game); print("cvc"); }
            else
                temp = mx.alfabeta2(game);

            if (gameMode == GameMode.ComputerVsComputer)
                StartCoroutine(Wait(temp));
            else
                CardRevealedByComputer(temp);

        }
    }
    public void CardRevealedByComputer(int index)
    {
        bool nextPlayerMove = false;
        MainCard card = squares.Find(x => x._id == index);
        if(countQeeue % 2 == 0)
            card.ChangeSpriteVersion2(index, images[0]);
        else
            card.ChangeSpriteVersion2(index, images[1]);
        if (gameMode == GameMode.PlayerVsComputer)
            nextPlayerMove = true;
        SetCardParameters(index, nextPlayerMove);


    }
    public void SetCardParameters(int index, bool nextPlayerMove)
    {
        game.Move(index);
        scoreLabel.text = "Player 1: " + game.p1 + " Player 2: " + game.p2;
        countQeeue++;
        if(!nextPlayerMove)
            WaitFor();
    }

    public void CardRevealed(MainCard card)
    {
        bool nextPlayerMove = true;
        if (countQeeue % 2 == 0)
            card.ChangeSprite(card._id, images[0]);
        else
            card.ChangeSprite(card._id, images[1]);
        if (gameMode == GameMode.PlayerVsComputer)
            nextPlayerMove = false;
        SetCardParameters(card._id, nextPlayerMove);
    }
}