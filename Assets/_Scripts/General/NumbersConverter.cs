/// <summary>
/// Класс для конвертации данных в стринговые значения 
/// </summary>
// В этот же класс можно добавить конвертацию очков в форматы 100К, 1,3М и т.д.
public static class NumbersConverter
{
    /// <summary>
    /// Конвертирует секунды в формат m:ss
    /// </summary>
    /// <returns></returns>
    public static string GetTimeMinSec(float time)
    {
        return ((int)(time / 60)).ToString() + ":" + string.Format("{0:00}", (int)(time % 60));
    }

    /// <summary>
    /// Возвращает значение в формате "$1.99", где "1.99" - это string price
    /// </summary>
    public static string GetPrice(string price, string currency)
    {
        string result = (currency == "usd"? "$" : "") + price;
        return result;
    }
}
