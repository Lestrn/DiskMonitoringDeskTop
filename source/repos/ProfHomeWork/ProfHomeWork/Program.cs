//Задание 2
//Создайте коллекцию, в которой бы хранились наименования 12 месяцев, порядковый номер и
//количество дней в соответствующем месяце. Реализуйте возможность выбора месяцев, как по
//порядковому номеру, так и количеству дней в месяце, при этом результатом может быть не
//только один месяц.
using System.Collections;
class Months : IEnumerable, IEnumerator
{
    private readonly string[] _monthsString = new string[]
    {
        "January", "February", "March", "April", "May",
        "June", "July", "August", "September", "October",
        "November", "December"
    };
    private int position = -1;
    private readonly int[] _monthDays = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public string this[int id]
    {
        get
        {
            if(id < 0 || id >= _monthsString.Length)
                throw new ArgumentOutOfRangeException("id");
            return _monthsString[id];
        }
        set
        {
            _monthsString[id] = value;
        }
    }
    public int Count { get { return _monthsString.Length; } }
   
    public object Current => _monthsString[position];

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        if(position < _monthsString.Length - 1)
        {
            position++;
            return true;
        }
        Reset();
        return false;
    }
    public IEnumerable GetByMonthByDays(int days)
    {
        for (int i = 0; i < 12; i++)
        {
            if (_monthDays[i] == days)
            {
                yield return _monthsString[i];
            }
        }
    }
    public void Reset()
    {
       position = -1;
    }
}

namespace ProfHomeWork
{
    class Program
    {

        private static void Main()
        {
            Months months = new Months();
            foreach (var item in months.GetByMonthByDays(30))
            {
                Console.WriteLine(item);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("All months");
            Console.ResetColor();
            foreach (var item in months)
            {
                Console.WriteLine(item);
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("By Id");
            Console.ResetColor();
            Console.WriteLine(months[2]);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Test part 2");
            Citizen citizens = new Pensioner();
            for (int i = 0; i < 15; i++)
            {
                citizens.Add(new Student() { Name = "Baran"});
            }
            citizens.Add(new Pensioner() { Name = "Babulya"});
            citizens.Add(new Pensioner() { Name = "Babka" });
            citizens.Add(new Worker() { Name = "Worker" });
            citizens.Insert(7, new Worker() { Name = "Inserted Worker" });
           // citizens.RemoveAt(0);
            citizens.Remove(citizens[1]);
            foreach (var item in citizens)
            {
                Console.WriteLine(item.ToString());
            }

        }
    }
}