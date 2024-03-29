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
}
