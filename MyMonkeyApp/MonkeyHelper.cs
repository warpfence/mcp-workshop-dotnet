using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// 원숭이 데이터 관리를 위한 정적 헬퍼 클래스입니다.
/// </summary>
public static class MonkeyHelper
{
    private static List<Monkey>? monkeys;
    private static int randomPickCount = 0;
    private static readonly object lockObj = new();

    /// <summary>
    /// MCP 서버에서 원숭이 데이터를 비동기로 가져옵니다.
    /// </summary>
    public static async Task InitializeAsync(Func<Task<List<Monkey>>> fetchMonkeysAsync)
    {
        if (monkeys == null)
        {
            var data = await fetchMonkeysAsync();
            lock (lockObj)
            {
                if (monkeys == null)
                    monkeys = data;
            }
        }
    }

    /// <summary>
    /// 모든 원숭이 목록을 반환합니다.
    /// </summary>
    public static List<Monkey> GetMonkeys()
    {
        return monkeys ?? new List<Monkey>();
    }

    /// <summary>
    /// 이름으로 원숭이를 찾습니다.
    /// </summary>
    public static Monkey? GetMonkeyByName(string name)
    {
        return monkeys?.FirstOrDefault(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 무작위 원숭이를 반환하고, 호출 횟수를 추적합니다.
    /// </summary>
    public static Monkey? GetRandomMonkey()
    {
        if (monkeys == null || monkeys.Count == 0)
            return null;
        lock (lockObj)
        {
            randomPickCount++;
        }
        var rnd = new Random();
        return monkeys[rnd.Next(monkeys.Count)];
    }

    /// <summary>
    /// 무작위 원숭이 선택 횟수를 반환합니다.
    /// </summary>
    public static int GetRandomPickCount()
    {
        lock (lockObj)
        {
            return randomPickCount;
        }
    }
}
