using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ����� ���������� �� ���� �������� ����
/// </summary>
public class Gameplay : MonoBehaviour
{
    /// <summary>
    /// ������� ��� ����� ����. ���������: bool - ��� ������
    /// </summary>
    [HideInInspector] public UnityEvent<bool> OnTernChanged;

    /// <summary>
    /// ������� ��� ��������� ����������� ������� ����. ���������: bool - ��� ������
    /// </summary>
    [HideInInspector] public UnityEvent<bool> OnFirstTurnDefined;

    [HideInInspector] public UnityEvent OnMatchFinished;

    [SerializeField] private Timer _timer;

    protected bool _isWinnerDetermined = false; // ���������� ��� ��������, ��������� �� ����� � ������� ������
    protected bool _isPlayerFirst; // ���������� ��� �����������, ����� ������ ����� ��� ��� ��������

    protected Cell[,] _grid;
    protected int _currentTurn = 1; // ���� ��� ������� ����, �� ����� ������ �����, ���� ��� - �� ������.
                                    // ����� ���� ������� ����� ���� ������� � ������� ��� ������ ������ ���������� ����� � �����
    protected int _lineLengthToWin = 3;

    private bool _isInitialized = false; public bool IsInitialized { get { return _isInitialized; } } // ���������� ��� ��������, ����������� �� ������������� ��������
    protected bool _canUseCellButton = true;

    public virtual void Initialise(GridPrefab gridPrefab)
    {
        // �������� ����� 
        _grid = new Cell[gridPrefab.Rows.Length, gridPrefab.Rows[0].Cells.Length];
        for (int i = 0; i < gridPrefab.Rows.Length; i++)
        {
            for (int j = 0; j < gridPrefab.Rows[0].Cells.Length; j++)
            {
                _grid[i, j] = gridPrefab.Rows[i].Cells[j];
                _grid[i, j].OnClick.AddListener(TryActivateCell);
                _grid[i, j].OnValueChanged.AddListener(MakeMove);
            }
        }
        // ��������, ��� ����� ������ ���
        _isPlayerFirst = ChooseFirstPlayer();
        OnFirstTurnDefined?.Invoke(_isPlayerFirst);

        _isInitialized = true;
    }

    private bool ChooseFirstPlayer()
    {
        // � ����� ����� ������ �� ����������� ������� ���� � ����������� �� ����, ��� ����� ���������/���������� ����� � ��������� ������
        return Random.Range(0, 2) == 0;
    }

    public virtual void StartGame()
    {
        Debug.Log("���� ��������!");
        _timer.StartTimer();

        if (_isPlayerFirst)
            TurnPlayer();
        else
            TurnOpponent();
    }

    public void StopGame()
    {
        _timer.StopTimer();
        // ���������� �������������� ���������� ��� �������������� ����� (��������, ��������� � ���������� ������� � ���� � �����)
    }

    public void ContinueGame()
    {
        _timer.StartTimer();
    }

    protected virtual void TurnPlayer()
    {
        Debug.Log("��� ������!");
        OnTernChanged?.Invoke(true);
    }

    protected virtual void TurnOpponent()
    {
        Debug.Log("��� ����������!");
        OnTernChanged?.Invoke(false);
    }

    private void SetNextMove()
    {
        if ((_isPlayerFirst && _currentTurn % 2 == 1) || (!_isPlayerFirst && _currentTurn % 2 == 0))
            TurnPlayer();
        else
            TurnOpponent();
    }

    private void TryActivateCell(Cell cell)
    {
        if (!_canUseCellButton) return; // ���� �� ������ ������ ������ ������� ���, �� ������ �� ���������. ������������ � ����������� �������

        if (cell.IsUsed) return; // ���� ������ ��� ������������, �� ������ �� ���������.
                                 // ����� ����� ���������� ������, ���� � ���������� �������� ����������� ������ ��� ��������� �������� ������
                                 // ����� ����� �������� ������ ��� ������� �� ��� �������������� ������

        cell.ChangeValue(_currentTurn % 2 == 1);
    }

    private void MakeMove(Cell cell)
    {
        // ���������, ��� �� ��� ��������
        CheckWinLines(cell);

        if (_isWinnerDetermined) // ���� ���������� ��� ���������, �� ���������� �������� �� ���������
            return;
        // ���� ���� ��������� ���������, �� ���� ������������� ������
        if (CheckGridFilled())
        {
            GameOver(CellValue.Empty);
            return;
        }

        _currentTurn++;
        // �������� ��� ���������
        SetNextMove();
    }

    private void CheckWinLines(Cell cell)
    {
        // ������� �������, � ����� ������ ������� ��������� ���������� ������
        int row = 0;
        int col = 0;
        for (int i = 0; i < _grid.GetLength(0); i++)
            for (int j = 0; j < _grid.GetLength(1); j++)
                if (cell == _grid[i, j])
                {
                    row = i;
                    col = j;
                }

        // ������������ �� �������� ��������� ������ ��������� ��� ���������, ����������� � ��������� �� ������� ������������ ���������� ����� �� �������� ������
        // ��� �������� ��������� ��������� �������� ���������� ����� � �������� � �����
        // ��������� ������
        CheckWinRow(row, cell);

        // ��������� �������
        if (_isWinnerDetermined) //���� ���������� ��������� ������, �� �������� �� ���������
            return;
        CheckWinColumn(col, cell);

        // ��������� ���������
        if (_isWinnerDetermined) //���� ���������� ��������� ������, �� �������� �� ���������
            return;
        CheckWinDiagonals(col, row, cell);
    }

    /// <summary>
    /// �������� ������� ������ �� ������, � ������� ���� �������� �������� ������
    /// </summary>
    /// <param name="row">������ ������, ������� ����������� ������</param>
    /// <param name="cell">������</param>
    private void CheckWinRow(int row, Cell cell)
    {
        int currentLineLength = 0; // ������� ���������� ����������� �������� (O ��� X) ������
        for (int j = 0; j < _grid.GetLength(1); j++)
        {
            if (_grid[row, j].IsUsed && _grid[row, j].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin)
            {
                //Debug.Log("������ �� ������: " + (row + 1));
                Win(cell.CellValue);
                break;
            }
        }
    }

    /// <summary>
    /// �������� ������� ������ �� �������, � ������� ���� �������� �������� ������
    /// </summary>
    /// <param name="col">������ �������, �������� ����������� ������</param>
    /// <param name="cell">������</param>
    private void CheckWinColumn(int col, Cell cell)
    {
        int currentLineLength = 0; // ������� ���������� ����������� �������� (O ��� X) ������
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, col].IsUsed && _grid[i, col].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin)
            {
                //Debug.Log("������ �� �������: " + (col + 1));
                Win(cell.CellValue);
                break;
            }
        }
    }

    /// <summary>
    /// �������� ������� ������ �� ���� ����������, � ������� ����������� ������ � ���������� ���������
    /// </summary>
    /// <param name="col">������ �������, �������� ����������� ������</param>
    /// <param name="row">������ ������, ������� ����������� ������</param>
    /// <param name="cell">������</param>
    private void CheckWinDiagonals(int col, int row, Cell cell)
    {
        int currentLineLength = 0; // ������� ���������� ����������� �������� (O ��� X) ������
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
            if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin) 
            { 
                //Debug.Log("������ �� ���������: 1");
                Win(cell.CellValue);
                break;
            }

            extremeCol++;
            // �������� �� ������, ���� ����� � ����� ������, ��� ��������
            if (extremeCol >= _grid.GetLength(1))
                break;
        }
        currentLineLength = 0;

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
            if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin) 
            { 
                //Debug.Log("������ �� ���������: 2"); 
                Win(cell.CellValue);
                break;
            }

            extremeCol--;
            // �������� �� ������, ���� ����� � ����� ������, ��� ��������
            if (extremeCol < 0)
                break;
        }
    }

    private bool CheckGridFilled()
    {
        int filledCellCount = 0;
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j].IsUsed)
                    filledCellCount++;
            }
        }
        return filledCellCount >= _grid.GetLength(0) * _grid.GetLength(1);
    }

    protected virtual void Win(CellValue cellvalue)
    {
        _isWinnerDetermined = true;
        GameOver(cellvalue);
    }

    protected virtual void GameOver(CellValue cellvalue)
    {
        // ���������� ��������� �����
        MatchResult matchResult = MatchResult.Draw;

        if (!_isWinnerDetermined || cellvalue == CellValue.Empty)
        {
            Debug.Log("�����!");
            matchResult = MatchResult.Draw;
        }
        else
        {
            Debug.Log("�������� " + cellvalue);
            if ((cellvalue == CellValue.X && _isPlayerFirst) || (cellvalue == CellValue.O && !_isPlayerFirst))
                matchResult = MatchResult.Win;
            else
                matchResult = MatchResult.Lose;
        }

        // �������� ���������� � ��������� ����� � ����������� ��� � �������� ������� ��������� ����
        GameManager.Instance.SetLastMatchResult(matchResult, _timer.CurrentTime);
        OnMatchFinished?.Invoke();
    }
}
