using System;
using System.Text;

namespace Lab03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Встановлення UTF-8 кодування для виведення українських літер та символів рамки
            Console.OutputEncoding = Encoding.UTF8;

            // Оголошення констант
            const double HalfDivider = 2.0;

            Console.WriteLine("=== РОЗРАХУНОК РАХУНКУ ЗА МОБІЛЬНИЙ ЗВ'ЯЗОК ===\n");

            // 1. Введення даних через TryParse 
            Console.Write("Абонплата (грн): ");
            bool ok1 = double.TryParse(Console.ReadLine(), out double baseFee);

            Console.Write("Витрачено хвилин: ");
            bool ok2 = int.TryParse(Console.ReadLine(), out int usedMinutes);

            Console.Write("Включено хвилин: ");
            bool ok3 = int.TryParse(Console.ReadLine(), out int includedMinutes);

            Console.Write("Ціна зайвої хвилини (грн): ");
            bool ok4 = double.TryParse(Console.ReadLine(), out double pricePerMinute);

            Console.Write("Витрачено ГБ: ");
            bool ok5 = double.TryParse(Console.ReadLine(), out double usedGb);

            Console.Write("Включено ГБ: ");
            bool ok6 = double.TryParse(Console.ReadLine(), out double includedGb);

            Console.Write("Ціна зайвого ГБ (грн): ");
            bool ok7 = double.TryParse(Console.ReadLine(), out double pricePerGb);

            bool okAll = ok1 && ok2 && ok3 && ok4 && ok5 && ok6 && ok7;
            Console.WriteLine($"\nВведення розпізнано: {okAll}\n");

            //  2. Обчислення 
            // Знаходження перевищення за допомогою Math.Abs
            int minDiff = usedMinutes - includedMinutes;
            int extraMinutes = (Math.Abs(minDiff) + minDiff) / 2;

            double gbDiff = usedGb - includedGb;
            double extraGb = (Math.Abs(gbDiff) + gbDiff) / HalfDivider;

            // Розрахунок доплат
            double extraMinutesCost = extraMinutes * pricePerMinute;
            double extraGbCost = extraGb * pricePerGb;

            // Підсумок із використанням складеного оператора присвоєння +=
            double totalCost = baseFee;
            totalCost += extraMinutesCost;
            totalCost += extraGbCost;

            // Середня ціна 1 хвилини
            double avgMinuteCost = usedMinutes > 0 ? totalCost / usedMinutes : 0;


            Console.WriteLine("┌──────────────────────────────┬─────────────────┐");
            Console.WriteLine($"│ {"Показник",-28} │ {"Значення",15} │");
            Console.WriteLine("├──────────────────────────────┼─────────────────┤");
            Console.WriteLine($"│ {"Абонентська плата",-28} │ {baseFee,12:F2} грн│");
            Console.WriteLine($"│ {"Зайві хвилини",-28} │ {extraMinutes,12:N0} хв │");
            Console.WriteLine($"│ {"Зайвий інтернет",-28} │ {extraGb,12:F2} ГБ │");
            Console.WriteLine($"│ {"Доплата за хвилини",-28} │ {extraMinutesCost,12:F2} грн│");
            Console.WriteLine($"│ {"Доплата за інтернет",-28} │ {extraGbCost,12:F2} грн│");
            Console.WriteLine($"│ {"Підсумок до сплати",-28} │ {totalCost,12:F2} грн│");
            Console.WriteLine($"│ {"Середня ціна 1 хвилини",-28} │ {avgMinuteCost,9:F3} грн/хв│");
            Console.WriteLine("└──────────────────────────────┴─────────────────┘");

            // 4. Аналіз округлень та Math.Sqrt 
            Console.WriteLine("\n=== РІВЕНЬ Б: ПЕРЕВІРКА ОКРУГЛЕНЬ ТА ДОДАТКОВИЙ ПОКАЗНИК ===");

            // 4.1 Виведення головного дробового результату (зайві ГБ) у 4 виглядах округлення
            Console.WriteLine($"\nАналіз округлення для перевищення ГБ ({extraGb}):");
            Console.WriteLine($"{"Floor",12}{"Ceiling",12}{"Round (F2)",12}{"Truncate",12}");
            Console.WriteLine($"{Math.Floor(extraGb),12}{Math.Ceiling(extraGb),12}{Math.Round(extraGb, 2),12}{Math.Truncate(extraGb),12}");

            // 4.2 Обчислення додаткового показника за допомогою Math.Sqrt у трьох форматах
            double sqrtGb = Math.Sqrt(usedGb);
            Console.WriteLine($"\nДодатковий показник (квадратний корінь із використаних ГБ = √{usedGb}):");
            Console.WriteLine($"• Формат F2: {sqrtGb:F2}");
            Console.WriteLine($"• Формат N2: {sqrtGb:N2}");
            Console.WriteLine($"• Формат E3: {sqrtGb:E3}");
        }
    }
}