using Assets.scripts;
using System;

class NewRandom
{
    NewGame game;
    Random r;
    int size;

    public NewRandom(int _size)
    {
        size = _size;
        game = new NewGame();
        game.Init(_size);
        r = new Random();
    }
    public int Cost(NewGame _game)
    {
        game = _game;
        int next = r.Next(game.notassigned.Count);
        return game.notassigned[next];
    }
}