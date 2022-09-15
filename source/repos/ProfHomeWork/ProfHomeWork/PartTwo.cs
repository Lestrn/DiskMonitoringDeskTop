using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Создайте абстрактный класс Гражданин. Создайте классы Студент, Пенсионер, Рабочий
//унаследованные от Гражданина. Создайте непараметризированную коллекцию со следующим
//функционалом:
//1.Добавление элемента в коллекцию.
//1) Можно добавлять только Гражданина.
//2) При добавлении, элемент добавляется в конец коллекции. Если Пенсионер, – то в
//начало с учетом ранее стоящих Пенсионеров. Возвращается номер в очереди.
//3) При добавлении одного и того же человека (проверка на равенство по номеру
//паспорта, необходимо переопределить метод Equals и/или операторы равенства для
//сравнения объектов по номеру паспорта) элемент не добавляется, выдается
//сообщение.
//2.Удаление
//1) Удаление – с начала коллекции.
//2) Возможно удаление с передачей экземпляра Гражданина.
//3.Метод Contains возвращает true/false при налчичии/отсутствии элемента в коллекции и
//номер в очереди.
//4. Метод ReturnLast возвращsает последнего чеолвека в очереди и его номер в очереди.
//5. Метод Clear очищает коллекцию.
//6. С коллекцией можно работать опертаором foreach.
namespace ProfHomeWork
{
    public abstract class Citizen : IList
    {
        public abstract string Name { get; set; }
        public abstract string PasportId { get;}
        private char[] _symbols = new char[] { 'A', 'H', 'B', 'C', 'D', 'E', 'U' ,'1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        protected object[] _citizens = new object[10];
        public object this[int index] { get => _citizens[index]; set => _citizens[index] = value; }

        public bool IsFixedSize => false;

        public bool IsReadOnly => false;

        public int Count => _citizens.Length;

        public bool IsSynchronized => true;

        public object SyncRoot => new object[10];

        public int Add(object value)
        {
            //1.Добавление элемента в коллекцию.
            //1) Можно добавлять только Гражданина.
            //2) При добавлении, элемент добавляется в конец коллекции. Если Пенсионер, – то в
            //начало с учетом ранее стоящих Пенсионеров. Возвращается номер в очереди.
            //3) При добавлении одного и того же человека (проверка на равенство по номеру
            //паспорта, необходимо переопределить метод Equals и/или операторы равенства для
            //сравнения объектов по номеру паспорта) элемент не добавляется, выдается
            //сообщение.
            if(value is Citizen)
            {
                
            }
            else
            {
                return 0;
            }
            for (int i = 0; i < _citizens.Length; i++)
            {
                if (value.Equals(_citizens[i])) 
                {
                    Console.WriteLine("Cant add same Citizen!");
                    return 0;
                }
                    
            }
                        
            bool isAdded = false;
            int iPosition = 0;
            if(value is Pensioner)
            {
                object tempCitizen = null;
                for (int i = 0; i < _citizens.Length; i++)
                {
                    if (_citizens[i] == default)
                    {
                        _citizens[i] = value;
                        isAdded = true;
                        break;
                    }
                    if (_citizens[i] is Pensioner)
                    {
                        continue;
                    }
                    else
                    {
                        tempCitizen = _citizens[i];
                        _citizens[i] = value;
                        iPosition = i;
                        break;
                    }
                }
                if (!isAdded)
                {
                    ++iPosition;
                    bool iIsFound = false;
                    object[] temp = new object[_citizens.Length + 1];
                    temp[iPosition] = tempCitizen;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (i == iPosition)
                        {
                            iIsFound = true;
                            continue;
                            
                        }
                        else if (iIsFound)
                        {
                            temp[i] = _citizens[i - 1];
                        }
                        else
                        {
                            temp[i] = _citizens[i];
                        }
                    }
                    _citizens = temp;
                    return 0;
                }
            }
            for (int i = 0; i < _citizens.Length; i++)
            {
                if(_citizens[i] == default) {
                    _citizens[i] = value;
                    isAdded = true;
                    break;
                }
            }
            if (!isAdded)
            {
                object[] temp = new object[_citizens.Length + 1];
                _citizens.CopyTo(temp, 0);
                temp[temp.Length - 1] = value;
                _citizens = temp;
            }
            return 0;
        }

        public void Clear()
        {
            _citizens = new object[10];
        }

        public bool Contains(object value)
        {
            for (int i = 0; i < _citizens.Length; i++)
            {
                if (_citizens[i] == value)
                    return true;
            }
            return false;
        }

        public void CopyTo(Array array, int index)
        {
            _citizens.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _citizens.GetEnumerator();
        }

        public int IndexOf(object? value)
        {
            for (int i = 0; i < _citizens.Length; i++)
            {
                if (_citizens[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, object value)
        {
            object[] temp = new object[_citizens.Length + 1];
            object tempValue = _citizens[index];
            for (int i = 0; i < temp.Length; i++)
            {
                if (i < index)
                {
                    temp[i] = _citizens[i];
                }
                else if (i == index)
                {
                    temp[i] = value;
                }
                else if (i > index)
                {
                    temp[i] = _citizens[i - 1];
                }
            }
            _citizens = temp;


        }

        public void Remove(object? value)
        {
            bool isFound = false;
            int temp = -1;
            for (int i = 0; i < _citizens.Length; i++)
            {
                if(value == _citizens[i])
                {
                    isFound = true;
                    temp = i;
                }
                if (isFound && temp < _citizens.Length - 1)
                {
                    _citizens[i] = _citizens[i + 1];
                    temp = i;
                }
                if(isFound && temp >= _citizens.Length - 1)
                {
                    _citizens[i] = default;
                }

            }
        }
        public void RemoveAt(int index)
        {

            for (int i = 0; i < _citizens.Length; i++)
            {
                if (i >= index && i < _citizens.Length -1)
                {
                    _citizens[i] = _citizens[i + 1];
                }
                else if(i == _citizens.Length - 1)
                {
                    _citizens[i] = default;
                }                               
            }
        }
        protected string GetPassportID()
        {
            Random random = new Random();
            string id ="UA:";
            for (int i = 0; i < 10; i++)
            {
                id += _symbols[random.Next(0, _symbols.Length - 1)].ToString();
            }
            return id;
        }
    }
    public class Student : Citizen
    {
        public override string Name { get; set; }
        public override string PasportId { get; }
        public Student()
        {
            PasportId = base.GetPassportID();
        }
        public override string ToString()
        {
            return $" Name = {Name}, Pasport ID = {PasportId}";
        }
        public override bool Equals(object? obj)
        {
            Student student = obj as Student;
            if (student == null)
                return false;
            return student.PasportId == this.PasportId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
    public class Worker : Citizen
    {
        public override string Name { get; set; }
        public override string PasportId { get; }
        public Worker()
        {
            PasportId = base.GetPassportID();
        }
        public override string ToString()
        {
            return $" Name = {Name}, Pasport ID = {PasportId}";
        }
        public override bool Equals(object? obj)
        {
            Worker worker = obj as Worker;
            if (worker == null)
                return false;
            return worker.PasportId == this.PasportId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class Pensioner : Citizen
    {
        public override string Name { get; set; }
        public override string PasportId { get; }
        public override string ToString()
        {
            return $" Name = {Name}, Pasport ID = {PasportId}";
        }
        public Pensioner()
        {
            PasportId = base.GetPassportID();
        }
        public override bool Equals(object? obj)
        {
            Worker pensioner = obj as Worker;
            if (pensioner == null)
                return false;
            return pensioner.PasportId == this.PasportId;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

