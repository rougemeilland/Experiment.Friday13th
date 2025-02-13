using System;
using System.Diagnostics;
using System.Numerics;

namespace Experiment.Friday13th.CUI
{
    internal class Program
    {
        private const int _BASE_YEAR = 2000;    // 起点となる年
        private const int _N_YEARS = 400;       // うるう年が一周する周期(年)
        private const int _DAYS_OF_WEEK = 7;    // 一週間の日数

        static void Main(string[] args)
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
                var n = 0;
                for (var month = 1; month <= 12; ++month)
                {
                    var d = new DateTime(year, month, 13, 12, 0, 0, DateTimeKind.Utc);
                    if (d.DayOfWeek == DayOfWeek.Friday)
                    {
                        ++count;
                        ++n;
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
            }

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
