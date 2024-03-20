using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����� ��� ����������� ������� ����� � ���������� �������� 
/// </summary>
// � ���� �� ����� ����� �������� ����������� ����� � ������� 100�, 1,3� � �.�.
public static class NumbersConverter
{
    /// <summary>
    /// ������������ ������� � ������ m:ss
    /// </summary>
    /// <returns></returns>
    public static string GetTimeMinSec(float time)
    {
        return ((int)(time / 60)).ToString() + ":" + string.Format("{0:00}", (int)(time % 60));
    }

    /// <summary>
    /// ������� �������� ���� "$1.99"
    /// </summary>
    public static string GetPrice(string price, string currency)
    {
        string result = (currency == "usd"? "$" : "") + price;
        return result;
    }
}
