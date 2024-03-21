using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// !!! Логика бота не всегда будет хорошо работать в случае, когда длина винлайна меньше размера максимальной стороны поля
// В будущем необходимы корректировки в условиях проверки победных ходов
// Так же в бужущем можно доработать логику ходов бота. В случае с полем 3*3 данной логики достаточно
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
        // Находим все возможные ходы
        List<Cell> availableMoves = new List<Cell>();

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (!_grid[i, j].IsUsed)
                    availableMoves.Add(_grid[i, j]);
            }
        }

        // С определенным шансом бот не делает рандомный ход в зависимости от своей сложности
        if (Random.Range(0, 100) > _botConfig.RandomTurnChance)
        {
            // Если удалось найти лучший ход при помощи логики, выбираем его
            Cell bestMove = ChooseBestMove(availableMoves);
            if (bestMove != null)
                return bestMove;
        }

        // Если лучший ход найти не удалось или бот должен выбрать рандомный ход, то он делает его
        Debug.Log("[BOT] Рандомный ход");
        return availableMoves[Random.Range(0, availableMoves.Count)];
    }

    private Cell ChooseBestMove(List<Cell> availableMoves)
    {
        Cell bestMove = null;

        // Если бот может победить на текущем ходу, он это делает
        bestMove = GetMoveToWin(availableMoves);
        if (bestMove != null)
            return bestMove;
        // Если бот может помешать выиграть противнику, он делает это
        bestMove = GetMoveToNotLose(availableMoves);
        if (bestMove != null)
            return bestMove;
        // Если бот может приблизить свою победу (совершить ход в линии, по которой существует наибольшее количество его
        // клеток в сумме с наибольшим количеством свободных клетов), то он делает это
        // В случае с сеткой 3*3 данная логика не особо будет отличаться от рандомного хода

        return null;
    }

    /// <summary>
    /// Возвращает клетку, при изменении которой побеждает бот. Если такой клетки нет, вернет null
    /// </summary>
    private Cell GetMoveToWin(List<Cell> availableMoves)
    {
        Cell finishMove = null;
        CellValue botValue = _isPlayerFirst ? CellValue.O : CellValue.X;
        finishMove = CheckValueInLines(availableMoves, botValue);
        if (finishMove != null)
            Debug.Log("[BOT] Найден победный ход");
        return finishMove;
    }

    /// <summary>
    /// Возвращает клетку, при изменении которой побеждает игрок. Если такой клетки нет, вернет null
    /// </summary>
    private Cell GetMoveToNotLose(List<Cell> availableMoves)
    {
        Cell finishMove = null;
        CellValue playerValue = _isPlayerFirst ? CellValue.X : CellValue.O;
        finishMove = CheckValueInLines(availableMoves, playerValue);
        if (finishMove != null)
            Debug.Log("[BOT] Найден ход для предотвращения победы игрока");
        return finishMove;
    }

    private Cell CheckValueInLines(List<Cell> availableMoves, CellValue cellValue)
    {
        foreach (Cell cell in availableMoves) // Проверяем комбинации для каждой свободной ячейки
        {
            // Сначала находим, в какой ячейке массива находится свободная ячейка
            int row = 0;
            int col = 0;
            for (int i = 0; i < _grid.GetLength(0); i++)
                for (int j = 0; j < _grid.GetLength(1); j++)
                    if (cell == _grid[i, j])
                    {
                        row = i;
                        col = j;
                    }

            // Отталкиваясь от текущего положения ячейки проверяем все вертикали, горизонтали и диагонали на наличие ячеек с искомым значением
            // Все проверки учитывают различные значения количества строк и столбцов в сетке
            // Проверяем строку
            if (CheckValueInRow(row, cellValue) == _lineLengthToWin - 1)
            {
                //Debug.Log("найдена нужная строка");
                return cell;
            }

            // Проверяем столбец
            if (CheckValueInColumn(col, cellValue) == _lineLengthToWin - 1)
            {
                //Debug.Log("найден нужный столбец");
                return cell;
            }

            // Проверяем диагонали
            if (CheckValueInDiagonals(col, row, cellValue) == _lineLengthToWin - 1)
            {
                //Debug.Log("найдена нужная диагональ");
                return cell;
            }
        }
        return null;
    }
}
