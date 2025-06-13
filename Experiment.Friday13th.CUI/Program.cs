using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;

namespace Experiment.Friday13th.CUI
{
    internal sealed class Program
    {
        private const int _BASE_YEAR = 2000;    // 起点となる年
        private const int _N_YEARS = 400;       // うるう年が一周する周期(年)
        private const int _DAYS_OF_WEEK = 7;    // 一週間の日数

        private static void Main()
        {

            // _N_YEARS が何日分に相当するかを求める
            var days =
                (int)(
                    new DateTime(_BASE_YEAR + _N_YEARS, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                    - new DateTime(_BASE_YEAR, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalDays;

            // 年月日と曜日が一致する周期(日数)を求める
            var gcm = (int)BigInteger.GreatestCommonDivisor(days, _DAYS_OF_WEEK);
            Debug.Assert(days % gcm == 0);
            var totalDays = days / gcm * _DAYS_OF_WEEK;

            // 年月日と曜日が一致する周期(年数)を求める
            Debug.Assert(totalDays % days == 0);
            var totalYears = _N_YEARS * (totalDays / days);

            var count = 0; // totalYears 年間の13日の金曜日の回数
            var count_0 = 0; // totalYears 年間で、13日の金曜日が1日もない年の回数
            var count_1 = 0; // totalYears 年間で、13日の金曜日が1日だけある年の回数
            var count_2 = 0; // totalYears 年間で、13日の金曜日が2日だけある年の回数
            var count_3 = 0; // totalYears 年間で、13日の金曜日が3日だけある年の回数
            var count_4 = 0; // totalYears 年間で、13日の金曜日が4日以上ある年の回数

            // totalYears 年間の13日の金曜日をすべて洗い出す
            for (var year = _BASE_YEAR; year < _BASE_YEAR + totalYears; ++year)
            {
                var dates = new List<string>();
                var n = 0;
                for (var month = 1; month <= 12; ++month)
                {
                    var d = new DateTime(year, month, 13, 12, 0, 0, DateTimeKind.Utc);
                    if (d.DayOfWeek == DayOfWeek.Friday)
                    {
                        ++count;
                        ++n;
                        dates.Add($"{month}月13日");
                    }
                }

                switch (n)
                {
                    case 0:
                        ++count_0;
                        break;
                    case 1:
                        ++count_1;
                        break;
                    case 2:
                        ++count_2;
                        break;
                    case 3:
                        ++count_3;
                        break;
                    default:
                        ++count_4;
                        break;
                }

                Console.WriteLine($"{year} 年の13日の金曜日: {string.Join(", ", dates)}{(DateTime.DaysInMonth(year, 2) > 28 ? " (うるう年)" : "")}");
            }

            Console.WriteLine($"以降、{totalYears:N0} 年周期で繰り返します。");
            Console.WriteLine("-----");
            Console.WriteLine($"うるう年を考慮すると、年月日と曜日の対応は {totalYears:N0} 年で一周します。これは {totalDays:N0} 日間であり、{totalDays / _DAYS_OF_WEEK:N0} 週間です。");
            Console.WriteLine($"13日の金曜日は平均して年に {(double)count / totalYears:F2} 回あります。");
            Console.WriteLine($"任意の月の13日が金曜日である確率は {(double)count / (totalYears * 12):P2} です。(参考: 1/7 = {1.0 / 7.0:P2})");
            Console.WriteLine($"13日の金曜日がない年は全体の {(double)count_0 / totalYears:P2} です。");
            Console.WriteLine($"13日の金曜日が1日だけある年は全体の {(double)count_1 / totalYears:P2} です。");
            Console.WriteLine($"13日の金曜日が2日だけある年は全体の {(double)count_2 / totalYears:P2} です。");
            Console.WriteLine($"13日の金曜日が3日だけある年は全体の {(double)count_3 / totalYears:P2} です。");
            Console.WriteLine($"13日の金曜日が4日以上ある年は全体の {(double)count_4 / totalYears:P2} です。");

            Console.WriteLine("ENTERキーを押すと終了します。");
            Console.Beep();
            _ = Console.ReadLine();

        }
    }
}
