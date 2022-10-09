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
            Student[] students = new Student[15];
            Worker[] workers = new Worker[15];
            string[] names = new string[]
            {   "Aaran",
                "Aaren", "Aarez", "Aarman", "Aaron",
                "Aaron-James", "Aarron", "Aaryan", "Aaryn",
                "Aayan", "Aazaan", "Abaan", "Abbas",
                "Abdallah", "Abdalroof", "Abdihakim",
                "Abdirahman", "Abdisalam", "Abdul",
                "Abdul-Aziz", "Abdulbasir", "Abdulkadir",
                "Abdulkarem", "Abdulkhader", "Abdullah",
                "Abdul-Majeed", "Abdulmalik", "Abdul-Rehman",
                "Abdur", "Abdurraheem", "Abdur-Rahman",
                "Abdur-Rehmaan", "Abel", "Abhinav",
                "Abhisumant", "Abid", "Abir",
                "Abraham", "Abu", "Abubakar",
                "Ace", "Adain", "Adam",
                "Adam-James", "Addison", "Addisson",
                "Adegbola", "Adegbolahan", "Aden",
                "Adenn", "Adie", "Adil", "Aditya",
                "Adnan", "Adrian", "Adrien", "Aedan", "Aedin",
                "Aedyn", "Aeron", "Afonso", "Ahmad", "Ahmed", "Ahmed-Aziz",
                "Ahoua", "Ahtasham", "Aiadan", "Aidan", "Aiden", "Aiden-Jack",
                "Aiden-Vee", "Aidian", "Aidy", "Ailin", "Aiman", "Ainsley", "Ainslie",
                "Airen", "Airidas", "Airlie", "AJ", "Ajay", "A-Jay", "Ajayraj", "Akan",
                "Akram", "Al", "Ala", "Alan", "Alanas", "Alasdair", "Alastair", "Alber",
                "Albert", "Albie", "Aldred", "Alec", "Aled", "Aleem", "Aleksandar",
                "Aleksander", "Aleksandr", "Aleksandrs", "Alekzander", "Alessandro",
                "Alessio", "Alex", "Alexander", "Alexei", "Alexx", "Alexzander",
                "Alf", "Alfee", "Alfie", "Alfred", "Alfy", "Alhaji", "Al-Hassan",
                "Ali", "Aliekber", "Alieu", "Alihaider", "Alisdair", "Alishan",
                "Alistair", "Alistar", "Alister", "Aliyaan", "Allan", "Allan-Laiton",
                "Allen", "Allesandro", "Allister", "Ally", "Alphonse", "Altyiab", "Alum",
                "Alvern", "Alvin", "Alyas", "Amaan", "Aman", "Amani", "Ambanimoh", "Ameer",
                "Amgad", "Ami", "Amin", "Amir", "Ammaar", "Ammar", "Ammer",
                "Amolpreet", "Amos", "Amrinder", "Amrit", "Amro", "Anay",
                "Andrea", "Andreas", "Andrei", "Andrejs", "Andrew",
                "Andy", "Anees", "Anesu", "Angel", "Angelo", "Angus", "Anir", "Anis", "Anish", "Anmolpreet", "Annan" };
            Random rand = new Random();

            citizens.Add(new Student() {Name = "LESTRNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNNN IS FIRST" } );
            Console.ForegroundColor = ConsoleColor.Magenta;
            for (int i = 0; i < students.Length; i++)
            {
                students[i] = new Student();
                students[i].Name = names[rand.Next(0, names.Length - 1)];
            }
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Worker();
                workers[i].Name = names[rand.Next(0, names.Length - 1)];
            }
            for (int i = 0; i < students.Length; i++)
            {
                citizens.Add(workers[i]);
                citizens.Add(students[i]);
            }
           citizens.Add(new Pensioner() { Name = "AmericanBABKA" });
           citizens.Add(new Pensioner() { Name = "BABKA229" });
            foreach (var item in citizens)
            {
                Console.WriteLine(item.ToString());
            }
            citizens.Add(workers[5]);
            citizens.Add(new object());
            Console.ResetColor();

        }
    }
}