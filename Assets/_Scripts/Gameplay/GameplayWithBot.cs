using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !!! ������ ���� �� ������ ����� ������ �������� � ������, ����� ����� �������� ������ ������� ������������ ������� ����
// � ������� ���������� ������������� � �������� �������� �������� �����
// ��� �� � ������� ����� ���������� ������ ����� ����. � ������ � ����� 3*3 ������ ������ ����������
public class GameplayWithBot : Gameplay
{
    [SerializeField] private float _averageBotTurnDelay = 2f;
    private BotConfig _botConfig;

    public override void Initialise(GridPrefab gridPrefab)
    {
        _botConfig = GameManager.Instance.SessionConfig.CurrentBot;
        base.Initialise(gridPrefab);
    }

    protected override void TurnPlayer()
    {
        base.TurnPlayer();
        _canUseCellButton = true;
    }

    protected override void TurnOpponent()
    {
        base.TurnOpponent();
        _canUseCellButton = false;
        MakeAutomaticTurn();
    }

    private void MakeAutomaticTurn()
    {
        StartCoroutine(MakeAutomaticTurnWithDelay());

        IEnumerator MakeAutomaticTurnWithDelay()
        {
            yield return new WaitForSeconds(Random.Range(_averageBotTurnDelay * 0.7f, _averageBotTurnDelay * 1.3f));
            Cell target = ChooseTargetCell();
            target.ChangeValue(_currentTurn % 2 == 1);
        }
    }

    private Cell ChooseTargetCell()
    {
        // ������� ��� ��������� ����
        List<Cell> availableMoves = new List<Cell>();

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (!_grid[i, j].IsUsed)
                    availableMoves.Add(_grid[i, j]);
            }
        }

        // � ������������ ������ ��� �� ������ ��������� ��� � ����������� �� ����� ���������
        if (Random.Range(0, 100) > _botConfig.RandomTurnChance)
        {
            // ���� ������� ����� ������ ��� ��� ������ ������, �������� ���
            Cell bestMove = ChooseBestMove(availableMoves);
            if (bestMove != null)
                return bestMove;
        }

        // ���� ������ ��� ����� �� ������� ��� ��� ������ ������� ��������� ���, �� �� ������ ���
        Debug.Log("[BOT] ��������� ���");
        return availableMoves[Random.Range(0, availableMoves.Count)];
    }

    private Cell ChooseBestMove(List<Cell> availableMoves)
    {
        Cell bestMove = null;

        // ���� ��� ����� �������� �� ������� ����, �� ��� ������
        bestMove = GetMoveToWin(availableMoves);
        if (bestMove != null)
            return bestMove;
        // ���� ��� ����� �������� �������� ����������, �� ������ ���
        bestMove = GetMoveToNotLose(availableMoves);
        if (bestMove != null)
            return bestMove;
        // ���� ��� ����� ���������� ���� ������ (��������� ��� � �����, �� ������� ���������� ���������� ���������� ���
        // ������ � ����� � ���������� ����������� ��������� ������), �� �� ������ ���
        // � ������ � ������ 3*3 ������ ������ �� ����� ����� ���������� �� ���������� ����

        return null;
    }

    /// <summary>
    /// ���������� ������, ��� ��������� ������� ��������� ���. ���� ����� ������ ���, ������ null
    /// </summary>
    private Cell GetMoveToWin(List<Cell> availableMoves)
    {
        Cell finishMove = null;
        CellValue botValue = _isPlayerFirst ? CellValue.O : CellValue.X;
        finishMove = CheckValueInLines(availableMoves, botValue);
        if (finishMove != null)
            Debug.Log("[BOT] ������ �������� ���");
        return finishMove;
    }

    /// <summary>
    /// ���������� ������, ��� ��������� ������� ��������� �����. ���� ����� ������ ���, ������ null
    /// </summary>
    private Cell GetMoveToNotLose(List<Cell> availableMoves)
    {
        Cell finishMove = null;
        CellValue playerValue = _isPlayerFirst ? CellValue.X : CellValue.O;
        finishMove = CheckValueInLines(availableMoves, playerValue);
        if (finishMove != null)
            Debug.Log("[BOT] ������ ��� ��� �������������� ������ ������");
        return finishMove;
    }

    private Cell CheckValueInLines(List<Cell> availableMoves, CellValue cellValue)
    {
        foreach (Cell cell in availableMoves) // ��������� ���������� ��� ������ ��������� ������
        {
            // ������� �������, � ����� ������ ������� ��������� ��������� ������
            int row = 0;
            int col = 0;
            for (int i = 0; i < _grid.GetLength(0); i++)
                for (int j = 0; j < _grid.GetLength(1); j++)
                    if (cell == _grid[i, j])
                    {
                        row = i;
                        col = j;
                    }

            // ������������ �� �������� ��������� ������ ��������� ��� ���������, ����������� � ��������� �� ������� ����� � ������� ���������
            // ��� �������� ��������� ��������� �������� ���������� ����� � �������� � �����
            // ��������� ������
            if (CheckValueInRow(row, cellValue) == _lineLengthToWin - 1)
            {
                //Debug.Log("������� ������ ������");
                return cell;
            }

            // ��������� �������
            if (CheckValueInColumn(col, cellValue) == _lineLengthToWin - 1)
            {
                //Debug.Log("������ ������ �������");
                return cell;
            }

            // ��������� ���������
            if (CheckValueInDiagonals(col, row, cellValue) == _lineLengthToWin - 1)
            {
                //Debug.Log("������� ������ ���������");
                return cell;
            }
        }

        return null;
    }

    /// <summary>
    /// ���������� ���������� ����� � ������ � ������� ���������
    /// </summary>
    /// <param name="row">������ ������, ������� ����������� ����������� ������</param>
    /// <param name="cellValue">������� ��������</param>
    private int CheckValueInRow(int row, CellValue cellValue)
    {
        int currentCount = 0; // ������� ���������� ����������� �������� (O ��� X) ������
        for (int j = 0; j < _grid.GetLength(1); j++)
        {
            if (_grid[row, j].IsUsed && _grid[row, j].CellValue == cellValue)
                currentCount++;
            else if (_grid[row, j].IsUsed && _grid[row, j].CellValue != cellValue)
                currentCount = 0;
        }
        return currentCount;
    }

    /// <summary>
    /// ���������� ���������� ����� � ������� � ������� ���������
    /// </summary>
    /// <param name="col">������ �������, �������� ����������� ������</param>
    /// <param name="cellValue">������� ��������</param>
    private int CheckValueInColumn(int col, CellValue cellValue)
    {
        int currentCount = 0; // ������� ���������� ����������� �������� (O ��� X) ������
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, col].IsUsed && _grid[i, col].CellValue == cellValue)
                currentCount++;
            else if (_grid[i, col].IsUsed && _grid[i, col].CellValue != cellValue)
                currentCount = 0;
        }
        return currentCount;
    }

    /// <summary>
    /// ���������� ������������ �� ���� ����������, ������������� ������, ���������� ����� � ������� ��������� 
    /// </summary>
    /// <param name="col">������ �������, �������� ����������� ������</param>
    /// <param name="row">������ ������, ������� ����������� ������</param>
    /// <param name="cellValue">������� ��������</param>
    private int CheckValueInDiagonals(int col, int row, CellValue cellValue)
    {
        int currentCount = 0; // ������� ���������� ����������� �������� (O ��� X) ������
        int maxCount = 0; // ������������ ���������� ����������� �������� ����� ���� ����������
        int extremeRow = 0;
        int extremeCol = 0;
        // ������� ������� ������� ����� ��� ��������� "\"
        if (row > col)
        {
            extremeCol = 0;
            extremeRow = row - col;
        }
        else
        {
            extremeRow = 0;
            extremeCol = col - row;
        }
        // ��������� ��������� "\"
        for (int i = extremeRow; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue == cellValue)
                currentCount++;
            else if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue != cellValue)
                currentCount = 0;

            extremeCol++;
            // �������� �� ������, ���� ����� � ����� ������, ��� ��������
            if (extremeCol >= _grid.GetLength(1))
                break;
        }
        maxCount = currentCount * 1;
        currentCount = 0;

        // ������� ������� ������� ����� ��� ��������� "/"
        if (row > _grid.GetLength(1) - col - 1)
        {
            extremeCol = _grid.GetLength(1) - 1;
            extremeRow = row - (_grid.GetLength(1) - 1 - col);
        }
        else
        {
            extremeRow = 0;
            extremeCol = col + row;
        }
        // ��������� ��������� "/"
        for (int i = extremeRow; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue == cellValue)
                currentCount++;
            else if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue != cellValue)
                currentCount = 0;

            extremeCol--;
            // �������� �� ������, ���� ����� � ����� ������, ��� ��������
            if (extremeCol < 0)
                break;
        }
        maxCount = maxCount >= currentCount ? maxCount : currentCount;
        return maxCount;
    }


}
