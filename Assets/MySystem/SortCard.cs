using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SortCard : MonoBehaviour
{
    // 花色优先级
    private static readonly List<string> suitOrder = new List<string> { "spade", "heart", "club", "diamond" };

    // 点数优先级
    private static readonly List<string> rankOrder = new List<string> { "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

    // 按花色排序
    public static List<string> SortBySuit(List<string> cards)
    {
        return cards.OrderBy(card => GetSuitIndex(card)).ThenBy(card => GetRankIndex(card)).ToList();
    }

    // 按点数排序
    public static List<string> SortByRank(List<string> cards)
    {
        return cards.OrderBy(card => GetRankIndex(card)).ThenBy(card => GetSuitIndex(card)).ToList();
    }

    private static int GetSuitIndex(string card)
    {
        foreach (var suit in suitOrder)
        {
            if (card.ToLower().Contains(suit))
                return suitOrder.IndexOf(suit);
        }
        return int.MaxValue; // 未识别的花色放最后
    }

    private static int GetRankIndex(string card)
    {
        foreach (var rank in rankOrder)
        {
            if (card.ToUpper().EndsWith(rank))
                return rankOrder.IndexOf(rank);
        }
        return int.MaxValue; // 未识别的点数放最后
    }

}

