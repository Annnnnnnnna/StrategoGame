using Assets.scripts;
using System;
using System.Collections.Generic;

class NewMinMax
{
    NewGame game;
    Random r;
    int size;
    AlgorithmType type;
    int maxDepth;
    Heuristic heur;

    public NewMinMax(int _size, AlgorithmType _type, int _maxDepth, Heuristic _heur)
    {
        game = new NewGame();
        r = new Random();
        size = _size;
        type = _type;
        maxDepth = _maxDepth;
        heur = _heur;
    }

    public int alfabeta(NewGame _game)
    {
        game = _game;
        List<int> routes = new List<int>();
        routes.AddRange(game.notassigned);
        int maxPoints = -100000;
        int index = -1;
        foreach (var v in routes)
        {
            int depth = 1;
            if (game.notassigned.Count < maxDepth)
                depth = maxDepth + 1 - game.notassigned.Count;

            game.notassigned.Remove(v);
            int column = v % size;
            int row = v / size;
            game.playground[row][column] = true;
            int pts = 0;

            if (heur == Heuristic.normal)
                pts = game.getPoints(v);
            else
                pts = game.getPointsHeur(v);

            game.notassigned.Add(v);
            game.playground[row][column] = false;
            int tempPoints = Alphabeta(v, true, -1000, 1000, depth, 0);

            if (tempPoints > maxPoints)
            {
                maxPoints = tempPoints;
                index = v;
            }
        }
        return index;
    }

    public int Alphabeta(int node, bool isMax, int alpha, int beta, int depth, int pts)
    {
        game.notassigned.Remove(node);
        int column = node % size;
        int row = node / size;
        game.playground[row][column] = true;
        int bestVal = 0;
        if (depth == maxDepth && isMax)
        {
            int points = 0;
            if (heur == Heuristic.normal)
                points = game.getPoints(node);
            else
                points = game.getPointsHeur(node);

            game.notassigned.Add(node);
            game.playground[row][column] = false;
            return pts + points;
        }
        else
        if (depth == maxDepth && !isMax)
        {
            int points = 0;
            if (heur == Heuristic.normal)
                points = game.getPoints(node);
            else
                points = game.getPointsHeur(node);

            game.notassigned.Add(node);
            game.playground[row][column] = false;

            return pts - points;
        }
        else
        if (isMax)
        {
            List<int> childr = new List<int>();
            childr.AddRange(game.notassigned);
            bestVal = -1000;
            foreach (var child in childr)
            {
                int tempPoints = 0;
                if (heur == Heuristic.normal)
                    tempPoints = game.getPoints(node);
                else
                    tempPoints = game.getPointsHeur(node);

                int points = Alphabeta(child, false, alpha, beta, depth + 1, pts + tempPoints);

                if (points > bestVal)
                    bestVal = points;
                if (bestVal > alpha)
                    alpha = bestVal;
                if (beta <= alpha && type == AlgorithmType.alphabeta)
                    break;
            }
            game.notassigned.Add(node);
            game.playground[row][column] = false;
            return bestVal;
        }
        else
        {
            List<int> childr = new List<int>();
            childr.AddRange(game.notassigned);
            bestVal = 1000;
            foreach (var child in childr)
            {
                int tempPoints = 0;
                if (heur == Heuristic.normal)
                    tempPoints = game.getPoints(node);
                else
                    tempPoints = game.getPointsHeur(node);

                int points = Alphabeta(child, true, alpha, beta, depth + 1, pts - tempPoints);
                if (points < bestVal)
                    bestVal = points;
                if (bestVal < beta)
                    beta = bestVal;
                if (beta <= alpha && type == AlgorithmType.alphabeta)
                    break;
            }
            game.notassigned.Add(node);
            game.playground[row][column] = false;
            return bestVal;
        }
    }

    public int alfabeta2(NewGame _game)
    {
        game = _game;
        List<int> routes = new List<int>();
        routes.AddRange(game.notassigned);
        int maxPoints = -100000;
        int index = -1;
        foreach (var v in routes)
        {
            int depth = 1;

            if (game.notassigned.Count * game.notassigned.Count > 200)
                maxDepth = 1;
            else if (game.notassigned.Count * game.notassigned.Count > 150)
                maxDepth = 2;
            else if (game.notassigned.Count * game.notassigned.Count > 100)
                maxDepth = 3;
            else if (game.notassigned.Count * game.notassigned.Count > 50)
                maxDepth = 4;
            else if (game.notassigned.Count * game.notassigned.Count > 30)
                maxDepth = 5;
            else
                maxDepth = 6;
            if (game.notassigned.Count < maxDepth)
                depth = maxDepth + 1 - game.notassigned.Count;
            int tempPoints = Alphabeta2(v, true, -1000, 1000, depth, 0);

            if (tempPoints > maxPoints)
            {
                maxPoints = tempPoints;
                index = v;
            }
        }
        return index;
    }

    public int Alphabeta2(int node, bool isMax, int alpha, int beta, int depth, int pts)
        {
            game.notassigned.Remove(node);
            int column = node % size;
            int row = node / size;
            game.playground[row][column] = true;
            int bestVal = 0;
            if (depth == maxDepth && isMax)
            {
                int points = 0;
                if (heur == Heuristic.normal)
                    points = game.getPoints(node);
                else
                    points = game.getPointsHeur(node);

                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return pts + points;
            }
            else
            if (depth == maxDepth && !isMax)
            {
                int points = 0;
                if (heur == Heuristic.normal)
                    points = game.getPoints(node);
                else
                    points = game.getPointsHeur(node);

                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return pts - points;
            }
            else
            if (isMax)
            {
                List<int> childr = new List<int>();
                childr.AddRange(game.notassigned);
                bestVal = -1000;
                foreach (var child in childr)
                {
                    int tempPoints = 0;
                    if (heur == Heuristic.normal)
                        tempPoints = game.getPoints(node);
                    else
                        tempPoints = game.getPointsHeur(node);

                    int points = Alphabeta(child, false, alpha, beta, depth + 1, pts + tempPoints);

                    if (points > bestVal)
                        bestVal = points;
                    if (bestVal > alpha)
                        alpha = bestVal;
                    if (beta <= alpha && type == AlgorithmType.alphabeta)
                    {
                        break;
                    }
                }
                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return bestVal;

            }
            else
            {
                List<int> childr = new List<int>();
                childr.AddRange(game.notassigned);
                bestVal = 1000;
                foreach (var child in childr)
                {

                    int tempPoints = 0;
                    if (heur == Heuristic.normal)
                        tempPoints = game.getPoints(node);
                    else
                        tempPoints = game.getPointsHeur(node);

                    int points = Alphabeta(child, true, alpha, beta, depth + 1, pts - tempPoints);

                    if (points < bestVal)
                        bestVal = points;
                    if (bestVal < beta)
                        beta = bestVal;
                    if (beta <= alpha && type == AlgorithmType.alphabeta)
                    {
                        break;
                    }
                }
                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return bestVal;

            }
        }

        public int minimax(NewGame _game)
        {
            
            game = _game;
            List<int> routes = new List<int>();
            routes.AddRange(game.notassigned);
            int maxPoints = -100000;
            int index = -1;
            foreach (var v in routes)
            {
                int depth = 1;
                if (game.notassigned.Count < maxDepth)
                    depth = maxDepth + 1 - game.notassigned.Count;

                game.notassigned.Remove(v);
                int column = v % size;
                int row = v / size;
                game.playground[row][column] = true;
                int pts = game.getPoints(v);
                game.notassigned.Add(v);
                game.playground[row][column] = false;
                int tempPoints = Minmax(v, true, depth, 0);
                if (tempPoints > maxPoints)
                {
                    maxPoints = tempPoints;
                    index = v;
                }
            }
            return index;
        }

        public int Minmax(int node, bool isMax, int depth, int pts)
        {
            game.notassigned.Remove(node);
            int column = node % size;
            int row = node / size;
            game.playground[row][column] = true;

            int bestVal = 0;
            if (depth == maxDepth && isMax)
            {
                int points = game.getPoints(node);
                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return pts + points;
            }
            else
            if (depth == maxDepth && !isMax)
            {
                int points = game.getPoints(node);
                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return pts - points;
            }
            else
            if (isMax)
            {
                List<int> childr = new List<int>();
                childr.AddRange(game.notassigned);
                bestVal = -1000;
                foreach (var child in childr)
                {
                    int tempPoints = game.getPoints(node);
                    int points = Minmax(child, false, depth + 1, pts + tempPoints);

                    if (points > bestVal)
                        bestVal = points;
                }
                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return bestVal;

            }
            else
            {
                List<int> childr = new List<int>();
                childr.AddRange(game.notassigned);
                bestVal = 1000;
                foreach (var child in childr)
                {
                    int tempPoints = game.getPoints(node);
                    int points = Minmax(child, true, depth + 1, pts - tempPoints);
                    if (points < bestVal)
                        bestVal = points;
                }
                game.notassigned.Add(node);
                game.playground[row][column] = false;
                return bestVal;

            }
        }
    }