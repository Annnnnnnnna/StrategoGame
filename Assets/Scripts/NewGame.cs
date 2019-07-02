using System.Collections.Generic;

namespace Assets.scripts
{
    class NewGame
    {
        public List<List<bool>> playground = new List<List<bool>>();
        public List<int> notassigned = new List<int>();
        public int size;
        public int round = 0;
        public int wrong = 0;
        public int wrongp = 0;
        public int p1 = 0;
        public int p2 = 0;

       public void Init(int N)
        {
            for (int i = 0; i < N; i++)
            {
                List<bool> row = new List<bool>();
                for (int j = 0; j < N; j++)
                {
                    bool temp = false;
                    row.Add(temp);
                    notassigned.Add(j + N * i);
                }
                playground.Add(row);
            }
            size = N;
        }

        public void Move(int index)
        {
            int column = index % size;
            int row = index / size;
            if (playground[row][column] == true)
            {
                if (round % 2 == 1)
                    wrong++;
                else
                    wrongp++;
                round++;
            }
            else
            {
                playground[row][column] = true;
                notassigned.Remove(index);
                if (round % 2 == 0)
                    p1 += getPoints(index);
                else
                    p2 += getPoints(index);
                round++;
            }
        }
        List<bool> AssignRightDiagonal(int cur_row, int cur_col)
        {
            List<bool> diagonal1 = new List<bool>();
            bool isChecked = false;
            while (!isChecked)
            {
                if (cur_row != 0 && cur_col != 0)
                {
                    cur_row--;
                    cur_col--;
                }
                else
                {
                    while (cur_row < size && cur_col < size)
                    {
                        diagonal1.Add(playground[cur_row][cur_col]);
                        cur_row++;
                        cur_col++;
                    }
                    isChecked = true;
                }
            }
            return diagonal1;
        }

        List<bool> AssignLeftDiagonal(int cur_row, int cur_col)
        {
            List<bool> diagonal2 = new List<bool>();
            bool isChecked = false;
            while (!isChecked)
            {
                if (cur_row != 0 && cur_col < size - 1)
                {
                    cur_row--;
                    cur_col++;
                }
                else
                {
                    while (cur_row < size && cur_col > -1)
                    {
                        diagonal2.Add(playground[cur_row][cur_col]);
                        cur_row++;
                        cur_col--;
                    }
                    isChecked = true;
                }
            }
            return diagonal2;
        }
        List<bool> AssignRow(int column)
        {
            List<bool> rows = new List<bool>();
            for (int i = 0; i < size; i++)
                rows.Add(playground[i][column]);
            return rows;
        }
        List<bool> AssignColumn(int row)
        {
            List<bool> columns = new List<bool>();
            for (int i = 0; i < size; i++)
                columns.Add(playground[i][row]);
            return columns;

        }
        int CountRightDiagonalPoints(List<bool> diagonal1) {
            int points = 0;
            bool isFull = true;
            if (diagonal1.Count > 1)
                for (int i = 0; i < diagonal1.Count; i++)
                {
                    if (diagonal1[i] == false)
                        isFull = false;
                }
            else
                isFull = false;

            if (isFull)
                points += diagonal1.Count;
            return points;
        }
        int CountLeftDiagonalPoints(List<bool> diagonal2)
        {
            int points = 0;
            bool isFull = true;
            if (diagonal2.Count > 1)
                for (int i = 0; i < diagonal2.Count; i++)
                {
                    if (diagonal2[i] == false)
                        isFull = false;
                }
            else
                isFull = false;

            if (isFull)
                points += diagonal2.Count;
            return points;
        }

        int CountRowsPoints(List<bool> rows) {
            int points = 0;
            bool isFull = true;
            if (rows.Count > 1)
                for (int i = 0; i < rows.Count; i++)
                {
                    if (rows[i] == false)
                        isFull = false;
                }
            else
                isFull = false;

            if (isFull)
                points += rows.Count;
            return points;
        }
        int CountColumnsPoints(List<bool> columns)
        {
            int points = 0;
            bool isFull = true;
            if (columns.Count > 1)
                for (int i = 0; i < columns.Count; i++)
                {
                    if (columns[i] == false)
                        isFull = false;
                }
            else
                isFull = false;

            if (isFull)
                points += columns.Count;
            return points;
        }
        public int getPoints(int index)
        {
            int column = index % size;
            int row = index / size;
            List<bool> diagonal1 = AssignRightDiagonal(row,column);
            List<bool> diagonal2 = AssignLeftDiagonal(row,column);
            List<bool> rows = AssignRow(column);
            List<bool> columns = AssignColumn(row);
           
            return CountColumnsPoints(columns)+CountRowsPoints(rows)+CountRightDiagonalPoints(diagonal1)+CountLeftDiagonalPoints(diagonal2);
        }

        public int getPointsHeur(int index)
        {
            int column = index % size;
            int row = index / size;
            List<bool> diagonal1 = AssignRightDiagonal(row, column);
            List<bool> diagonal2 = AssignLeftDiagonal(row, column);
            List<bool> rows = AssignRow(column);
            List<bool> columns = AssignColumn(row);

            int points = CountColumnsPoints(columns) + CountRowsPoints(rows) + CountRightDiagonalPoints(diagonal1) + CountLeftDiagonalPoints(diagonal2);

            if (round < size * size * 3 / 4)
                if (row != 0 || row != size - 1 || column != 0 || column != size - 1)
                    points++;
            else if (row == 1 && column == 1)
                    points--;
                else if (row == 1 && column == size - 1)
                    points--;
                else if (row == size - 1 && column == 1)
                    points--;
                else if (row == size - 1 && column == size - 1)
                    points--;
                else if (row == 0 || row == size - 1 || column == 0 || column == size - 1)
                    points++;
            return points;
        }
    }
}