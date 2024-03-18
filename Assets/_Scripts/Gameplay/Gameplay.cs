using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Класс отвечающий за весь геймплей игры
/// </summary>
public class Gameplay : MonoBehaviour
{
    /// <summary>
    /// Событие при смене хода. Параметры: bool - ход игрока
    /// </summary>
    [HideInInspector] public UnityEvent<bool> OnTernChanged;

    /// <summary>
    /// Событие при окончании определения первого хода. Параметры: bool - ход игрока
    /// </summary>
    [HideInInspector] public UnityEvent<bool> OnFirstTurnDefined;

    [HideInInspector] public UnityEvent OnMatchFinished;

    [SerializeField] private Timer _timer;

    protected bool _isWinnerDetermined = false; // Переменная для проверки, определен ли лидер в текущем раунде
    protected bool _isPlayerFirst; // Переменная для определения, ходит первым игрок или его оппонент

    protected Cell[,] _grid;
    protected int _currentTurn = 1; // Если ход кратный двум, то ходит второй игрок, если нет - то первый.
                                    // Также этот счетчик может быть полезен в будущем для показа общего количества ходов в матче
    protected int _lineLengthToWin = 3;

    private bool _isInitialized = false; public bool IsInitialized { get { return _isInitialized; } } // Переменная для проверки, закончилась ли инициализация геймплея
    protected bool _canUseCellButton = true;

    public virtual void Initialise(GridPrefab gridPrefab)
    {
        // Заплняем сетку 
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
        // Выбираем, чей будет первый ход
        _isPlayerFirst = ChooseFirstPlayer();
        OnFirstTurnDefined?.Invoke(_isPlayerFirst);

        _isInitialized = true;
    }

    private bool ChooseFirstPlayer()
    {
        // В бущем можно влиять на вероятность первого хода в зависимости от того, как часто выигрывал/проигрывал игрок в последних матчах
        return Random.Range(0, 2) == 0;
    }

    public virtual void StartGame()
    {
        Debug.Log("Игра началась!");
        _timer.StartTimer();

        if (_isPlayerFirst)
            TurnPlayer();
        else
            TurnOpponent();
    }

    public void StopGame()
    {
        _timer.StopTimer();
        // Необходима дополнительная реализация для предотвращения багов (например, остановка и перезапуск куратин в игре с ботом)
    }

    public void ContinueGame()
    {
        _timer.StartTimer();
    }

    protected virtual void TurnPlayer()
    {
        Debug.Log("Ход игрока!");
        OnTernChanged?.Invoke(true);
    }

    protected virtual void TurnOpponent()
    {
        Debug.Log("Ход противника!");
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
        if (!_canUseCellButton) return; // Если на данный момент нельзя сделать ход, то дальше не выполняем. Используется в наследуемых классах

        if (cell.IsUsed) return; // Если ячейку уже использовали, то дальше не выполняем.
                                 // Здесь можно доработать логику, если в дальнейшем появится возможность менять уже имеющееся значение клетки
                                 // Также можно добавить логику при нажатии на уже использованную ячейку

        cell.ChangeValue(_currentTurn % 2 == 1);
    }

    private void MakeMove(Cell cell)
    {
        // Проверяем, был ли ход победным
        CheckWinLines(cell);

        if (_isWinnerDetermined) // Если победитель уже определен, то дальнейших действий не требуется
            return;
        // Если поле полностью заполнено, то игра заканчивается ничьей
        if (CheckGridFilled())
        {
            GameOver(CellValue.Empty);
            return;
        }

        _currentTurn++;
        // Передаем ход оппоненту
        SetNextMove();
    }

    private void CheckWinLines(Cell cell)
    {
        // Сначала находим, в какой ячейке массива находится измененная ячейка
        int row = 0;
        int col = 0;
        for (int i = 0; i < _grid.GetLength(0); i++)
            for (int j = 0; j < _grid.GetLength(1); j++)
                if (cell == _grid[i, j])
                {
                    row = i;
                    col = j;
                }

        // Отталкиваясь от текущего положения ячейки проверяем все вертикали, горизонтали и диагонали на наличие необходимого количества таких же значений подряд
        // Все проверки учитывают различные значения количества строк и столбцов в сетке
        // Проверяем строку
        CheckWinRow(row, cell);

        // Проверяем столбец
        if (_isWinnerDetermined) //Если победитель определен раньше, то проверка не требуется
            return;
        CheckWinColumn(col, cell);

        // Проверяем диагонали
        if (_isWinnerDetermined) //Если победитель определен раньше, то проверка не требуется
            return;
        CheckWinDiagonals(col, row, cell);
    }

    /// <summary>
    /// Проверка условия победы по строке, в которой было изменено значение ячейки
    /// </summary>
    /// <param name="row">Индекс строки, которой пренадлежит ячейка</param>
    /// <param name="cell">Ячейка</param>
    private void CheckWinRow(int row, Cell cell)
    {
        int currentLineLength = 0; // Текущее количество необходимых значений (O или X) подряд
        for (int j = 0; j < _grid.GetLength(1); j++)
        {
            if (_grid[row, j].IsUsed && _grid[row, j].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin)
            {
                //Debug.Log("Победа по строке: " + (row + 1));
                Win(cell.CellValue);
                break;
            }
        }
    }

    /// <summary>
    /// Проверка условия победы по столбцу, в котором было изменено значение ячейки
    /// </summary>
    /// <param name="col">Индекс столбца, которому пренадлежит ячейка</param>
    /// <param name="cell">Ячейка</param>
    private void CheckWinColumn(int col, Cell cell)
    {
        int currentLineLength = 0; // Текущее количество необходимых значений (O или X) подряд
        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, col].IsUsed && _grid[i, col].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin)
            {
                //Debug.Log("Победа по столбцу: " + (col + 1));
                Win(cell.CellValue);
                break;
            }
        }
    }

    /// <summary>
    /// Проверка условия победы по двум диагоналям, к которым пренадлежит ячейка с измененным значением
    /// </summary>
    /// <param name="col">Индекс столбца, которому пренадлежит ячейка</param>
    /// <param name="row">Индекс строки, которой пренадлежит ячейка</param>
    /// <param name="cell">Ячейка</param>
    private void CheckWinDiagonals(int col, int row, Cell cell)
    {
        int currentLineLength = 0; // Текущее количество необходимых значений (O или X) подряд
        int extremeRow = 0;
        int extremeCol = 0;
        // Находим крайнюю верхнюю точку для диагонали "\"
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
        // Проверяем диагональ "\"
        for (int i = extremeRow; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin) 
            { 
                //Debug.Log("Победа по диагонали: 1");
                Win(cell.CellValue);
                break;
            }

            extremeCol++;
            // Проверка на случай, если строк в сетке больше, чем столбцов
            if (extremeCol >= _grid.GetLength(1))
                break;
        }
        currentLineLength = 0;

        // Находим крайнюю верхнюю точку для диагонали "/"
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
        // Проверяем диагональ "/"
        for (int i = extremeRow; i < _grid.GetLength(0); i++)
        {
            if (_grid[i, extremeCol].IsUsed && _grid[i, extremeCol].CellValue == cell.CellValue)
                currentLineLength++;
            else
                currentLineLength = 0;

            if (currentLineLength >= _lineLengthToWin) 
            { 
                //Debug.Log("Победа по диагонали: 2"); 
                Win(cell.CellValue);
                break;
            }

            extremeCol--;
            // Проверка на случай, если строк в сетке больше, чем столбцов
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
        // Определяем результат матча
        MatchResult matchResult = MatchResult.Draw;

        if (!_isWinnerDetermined || cellvalue == CellValue.Empty)
        {
            Debug.Log("Ничья!");
            matchResult = MatchResult.Draw;
        }
        else
        {
            Debug.Log("Победили " + cellvalue);
            if ((cellvalue == CellValue.X && _isPlayerFirst) || (cellvalue == CellValue.O && !_isPlayerFirst))
                matchResult = MatchResult.Win;
            else
                matchResult = MatchResult.Lose;
        }

        // Передаем информацию о последнем матче в собственный кэш и вызываем событие окончания игры
        GameManager.Instance.SetLastMatchResult(matchResult, _timer.CurrentTime);
        OnMatchFinished?.Invoke();
    }
}
