/*
 Студент
Розробити клас, що описує студента і зберігає: 
	прізвище,
	ім'я,
	по батькові,
	група,
	номер залікової книжки(формується послідовно, починаючи з деякого числа, формується на основі статичного  лічильника студентів )
	дата народження (DateTime), dt = new DateTime(2000, 12, 31)    DateTime.Parse("12.31.2017")  
	оцінки з наступних предметів (рваний масив, масив масивів) :
		перший рядок(з індексом 0) зберігатиме  оцінки з програмування 
		рядок з індексом 1 зберігатиме  оцінки з адміністрування
		рядок з індексом 2  зберігатиме  оцінки з дизайну
	*число оцінок може бути різне*
	константи максимально та  мінімально допустимої оцінок(1, 12 )

	Визначити перелічувальний тип Subjects з константами, що відповідатимуть індексам предметів у рваному масиві. 	
	enum Subject{ Programming , Admin, Design}

	назва навчального закладу(статичне поле)
	кількість створених студентів(статичне поле)

Додати методи та властивості по роботі з перерахованими даними: 
	конструктор (приймає прізвище, ім'я, по батькові та дату народження (DateTime) )
	властивості для роботи з полями прізвище, імя та по-батькові(не дозволяти присвоювати null-рядки чи рядки з пропусків -String.IsNullOrEmpty(),  видаляти зайві символи для імен(наприклад цифри)
+	*для видалення зайвих знаків з імен визначити приватний метод*
 
+	статична властивість для читання, що повертає число створених на даний момент обєктів класу Студент
+	встановлення оцінок з програмування ( метод, приймає масив оцінок  params int []), *оцінки можна клонувати або створювати масив відповідного розміру та копіювати*
+	встановлення оцінок з адміністрування ( метод, приймає масив оцінок   params int [] )
+	встановлення оцінок з дизайну ( метод, приймає масив оцінок   params int [])
+	середній бал з програмування ( read-only властивість )
+	середній бал з адміністрування ( read-only властивість,)
+	середній бал з дизайну ( read-only властивість )
+	середній бал успішності з усіх предметів ( read-only властивість)
+	повне ім'я студента ( read-only властивість ) 
+	вік (повних років) ( метод, приймає дату (DateTime) і обчислює вік студента на цю дату ( читайте про оператор віднімання ),
+	властивість вік(повних років) на сьогоднішню дату
+	група ( АВТОвластивість для читання і запису )
+	зміна оцінки з певного предмету за певним номером( void SetMark(Subject subject, int numLesson, int mark )), виконувати перевірку номеру пари та самої оцінки на коректність(відповідні приватні методи перевірки)
+	очищення всіх оцінок (метод, без параметрів)
+	вивід інформації про студента( у тому числі  оцінки )
+	введення даних про студента(імя, прізвище, по-батькові, група)

* Передбачити правильні розрахунки у класі, якщо оцінки не виставлені(null)
Перевірити роботу класу.

+   Створити масив(або List<Student>) з обєктів Студентів. 
+	Знайти студента з найбільшим та найменшим середнім балом(визначити у класі Студент відповідні статичні методи). 
	*Використайте методи бібліотеки Linq: Max() та Min() ( students.Max( st => st.Average))
+	Знайти  кількість студентів, які здали певний предмет добре(середній бал більше рівний 7), статичний метод приймає список студентів(Count(), лямбда)	


2. Індексатори. Визначити у  класі Студент :  
	a) 2-вимірний індексатор з 2-ма індексами(перший індекс типу enum Subject, другий  типу int), який повертає доступ до певної оцінки з певного предмету.
	   Наприклад,  student[Subject.Programming, 2] = 12; // поставили з  предмету програмування  2-гу оцінку 12.

	b) 2-вимірний індексатор з 2-ма індексами(рядком та цілим), який повертає доступ до певної оцінки з певного предмету.
	   Наприклад,  student["programming", 0] = 11; // поставили з  предмету програм 0-у оцінку 11.

       3. Змінити клас Студент(рваний масив оцінок).
Визначити властивості обчислення середніх оцінок різних предметів  як double? (Nullable<double>)
Виправити метод друку студента(якщо необхідно).
Перевірити роботу властивостей у функції для деякого екземпляра(ів)  класу Студент.

Користуватися властивостями та методами для nullable типів: HasValue, Value, GetValueOrDefault()

 */

using System;
using System.Linq;

namespace _01_student
{
    class Program
    {
        internal class Student
        {
    
            string surname;
            string name;
            string middlename;
            int gradebook;
            const int constBook = 10000;
            DateTime date;
            int[][] marks = new int[3][]
              {
                new int[0],
                new int[0],
                new int[0]
               };
            const int minMark = 1;
            const int maxMark = 12;
            public enum Subject { Programming, Admin, Design };
            private static string nameUniversity = "Some IT University";
            static int count;

            static Student()
            {
                count = 0;
            }

            public Student()
            {
                this.Surname = "surname";
                this.Name = "name";
                this.Middlename = "middlename";
                this.gradebook = ++count + constBook;
                this.BirthDate = DateTime.Now;
            }

            public Student(string surname, string name, string middlename, DateTime date)
            {
                this.Surname = surname;
                this.Name = name;
                this.Middlename = middlename;
                this.gradebook = ++count + constBook;
                this.BirthDate = date;
            }


            public string Surname
            {
                get => surname;

                set
                {
                    surname = CorrectName(value);
                }
            }

            public string Name
            {
                get => name;

                set
                {
                     name = CorrectName(value);
                }
            }

            public string Middlename
            {
                get => middlename;

                set 
                {
                    middlename = CorrectName(value);
                }
            }

            public DateTime BirthDate
            {
                get => date;

                set 
                {
                    date = value;
                }
            }

            // *для видалення зайвих знаків з імен визначити приватний метод*
            private string CorrectName(string name)
            {
                if (name == null)
                    return "noname";
                if (String.IsNullOrEmpty(name))
                    return "noname";
                foreach (var c in name)
                {
                    if (!char.IsLetter(c))
                        name = name.Remove(name.IndexOf(c), 1);
                }
                return name;
            }

            // повне ім'я студента ( read-only властивість )
            public string FullName
            {
                get => surname + " " + name + " " + middlename;
            }

            //статична властивість для читання, що повертає число створених на даний момент обєктів класу Студент
            public static int QtyStudents
            {
                get => count;
            }

            // додавання елементу в масив
            static void AppendElemToArray(ref int[][] arr, Subject subj, int element)
            {
                if(CorrectMark(element))
                {
                    Array.Resize(ref arr[(int)subj], arr[(int)subj].Length + 1);
                    arr[(int)subj][arr[(int)subj].Length - 1] = element;
                }
            }

            //встановлення оцінок з програмування ( метод, приймає масив оцінок  params int []), 
            //*оцінки можна клонувати або створювати масив відповідного розміру та копіювати*
            public void SetMarksPrograming(params int[] points)
            {
                foreach (int p in points)
                {
                    AppendElemToArray(ref marks, Subject.Programming, p);
                }
            }

            //встановлення оцінок з адміністрування(метод, приймає масив оцінок   params int[] )
            public void SetMarksAdmin(params int[] points)
            {
                foreach (int p in points)
                {
                    AppendElemToArray(ref marks, Subject.Admin, p);
                }
            }

            //встановлення оцінок з дизайну(метод, приймає масив оцінок   params int[])
            public void SetMarksDesign(params int[] points)
            {
                foreach (int p in points)
                {
                    AppendElemToArray(ref marks, Subject.Design, p);
                }
            }


            // друк оцінок
            static string PrintMarks(int[][] arr, string message = "")
            {
                string str = message;
                for (int i = 0; i < arr.Length; i++)
                {
                    str += Convert.ToString((Subject)i) + ":\t\t";
                    for (int j = 0; j < arr[i].Length; j++)
                        str += $"{arr[i][j]}\t";
                    str += "\n";
                }
                return str;
            }
   
            //середній бал з програмування ( read-only властивість )
            public Nullable<double> AvgPrograming
            {
                get
                {
                    Nullable<double> avg = null;
                    int subj = (int)Subject.Programming;
                    if (marks[subj].Length != 0)
                    {
                        avg = 0;
                        for (int i = 0; i < marks[subj].Length; i++)
                            avg += (double)marks[subj][i];
                    }
                    if (avg.HasValue)
                        return avg / (double)marks[subj].Length;
                    else
                        return avg.GetValueOrDefault(0);
                }
            }

            //середній бал з адміністрування(read-only властивість,)
            public Nullable<double> AvgAdmin
            {
                get
                {
                    Nullable<double> avg = null;
                    int subj = (int)Subject.Admin;
                    if (marks[subj].Length != 0)
                    {
                        avg = 0;
                        for (int i = 0; i < marks[subj].Length; i++)
                            avg += (double)marks[subj][i];
                    }
                    if (avg.HasValue)
                        return avg / (double?)marks[subj].Length;
                    else
                        return avg.GetValueOrDefault(0);
                }
            }

            //середній бал з дизайну(read-only властивість)
            public Nullable<double> AvgDesign
            {
                get
                {
                    Nullable<double> avg = null;
                    int subj = (int)Subject.Design;
                    if (marks[subj].Length != 0)
                    {
                        avg = 0;
                        for (int i = 0; i < marks[subj].Length; i++)
                            avg += (double)marks[subj][i];
                    }
                    if (avg.HasValue)
                        return avg / (double?)marks[subj].Length;
                    else
                        return avg.GetValueOrDefault(0);
                }
            }

            //середній бал успішності з усіх предметів ( read-only властивість)
            public Nullable<double> AvgAll
            {
                get
                {
                    Nullable<double> avg = null;
                    int count = 0;
                    for (int k = 0; k < marks.Length; k++)
                        if (marks[k].Length != 0)
                        {
                            if (count == 0)
                                avg = 0;
                            for (int i = 0; i < marks[k].Length; i++)
                                { 
                                    avg += (double)marks[k][i];
                                    ++count;
                                }
                        }
                    if (avg.HasValue)
                        return avg / (double)count;
                    else
                        return avg.GetValueOrDefault(0); 
    
                }
            }

            //вік(повних років) (метод, приймає дату (DateTime) і обчислює вік студента на цю дату(читайте про оператор віднімання),
            public int FullYears(DateTime dt)
            {
                TimeSpan span = dt - date;
                int years = span.Days;
                return years / 365;
            }

            //властивість вік(повних років) на сьогоднішню дату
            public int FullYearsOnToday
            {
                get
                {
                    TimeSpan span = DateTime.Now - date;
                    int years = span.Days;
                    return years / 365;
                }
            }

            //група ( АВТОвластивість для читання і запису )
            public string Group
            { get; set; }

            //зміна оцінки з певного предмету за певним номером( void SetMark(Subject subject, int numLesson, int mark )), 
            //виконувати перевірку номеру пари та самої оцінки на коректність(відповідні приватні методи перевірки)
            public void SetMark(Subject subject, int numLesson, int mark)
            {
                //if (ArrElemPresent((int)subject, numLesson))
                    //if (CorrectMark(mark))
                        this[subject, numLesson] = mark;
            }

            // перевірка на коректність оцінки (в межах 1..12)
            private static bool CorrectMark(int mark)
            {
                if (mark >= minMark && mark <= maxMark)
                    return true;
                else
                    return false;
            }

            // перевірка чи є елемент [x][y] в масиві
            private bool ArrElemPresent(int x, int y)
            {
                if (x < 3 && x >= 0)
                {
                    if (marks[x].Length > y)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }

            //очищення всіх оцінок (метод, без параметрів)
            public void DeleteAllMarks()
            {
                for(int i = 0; i < 3; i++)
                    Array.Resize(ref marks[i], 0);
            }

            //вивід інформації про студента( у тому числі  оцінки )
            public void Print()
            {
                Console.WriteLine($"\nID Gradebook:\t{gradebook}\nFull name:\t{FullName}\nBirth date:\t{date.ToShortDateString()} ({FullYearsOnToday} years)");
                Console.WriteLine($"Un-sity/Group:\t{nameUniversity} / {Group}\n{PrintMarks(marks, "\n\tMarks:\n----------------\n")}");
                Console.WriteLine($"Average mark of Programming:\t{AvgPrograming}");
                Console.WriteLine($"Average mark of Admin:\t{AvgAdmin}");
                Console.WriteLine($"Average mark of Design:\t{AvgDesign}");
                Console.WriteLine($"Average mark of all subjects:\t{AvgAll}");
                Console.WriteLine(" - - - - - - - - - - - - - - - - - - - - -");
            }

            //	введення даних про студента(імя, прізвище, по-батькові, група) через параметри
            public void EnterDataSudent(string _name, string _surname, string _middlename, string _group)
            {
                this.Surname = _surname;
                this.Name = _name;
                this.Middlename = _middlename;
                this.Group = _group;
            }

            //	введення даних про студента(імя, прізвище, по-батькові, група) від користувача
            public void EnterDataSudent()
            {               
                Console.Write("Enter name:\t");
                string _name = Console.ReadLine();
                Console.Write("Enter surname:\t");
                string _surname = Console.ReadLine();
                Console.Write("Enter middlename:\t");
                string _middlename = Console.ReadLine();
                Console.Write("Enter group:\t");
                string _group = Console.ReadLine();
                EnterDataSudent(_name, _surname, _middlename, _group);
            }

            //Знайти студента з найбільшим та найменшим середнім балом(визначити у класі Студент відповідні статичні методи). 
	        //*Використайте методи бібліотеки Linq: Max() та Min() (students.Max(st => st.Average))
            static public void MaxAvgMark(Student[] students)
            {
                Nullable<double> max = students.Max((Student st) => st.AvgAll);
                Console.WriteLine($"Max avg mark:\t{max}"); //потрібно повернути також FullName студента?  якщо так, то як в лямбді шукати по середньому балу а повертати елемент масиву Student
            }

            // Min
            static public void MinAvgMark(Student[] students)
            {
                Nullable<double> min = students.Min((Student st) => st.AvgAll);
                Console.WriteLine($"Min avg mark:\t{min}");
            }

            //Знайти  кількість студентів, які здали певний предмет добре(середній бал більше рівний 7), статичний метод приймає список студентів(Count(), лямбда)	
            static public void CountGoodMark(Student[] students)
            {
                int cnt = 0;
                cnt = students.Count((Student st) => st.AvgPrograming > 6);
                Console.WriteLine($"Subject 'Programming' c-ty students with mark better than 7:\t{cnt}");
                cnt = students.Count((Student st) => st.AvgAdmin > 6);
                Console.WriteLine($"Subject 'Admin' c-ty students with mark better than 7:\t{cnt}");
                cnt = students.Count((Student st) => st.AvgDesign > 6);
                Console.WriteLine($"Subject 'Design' c-ty students with mark better than 7:\t{cnt}");
            }

            public override string ToString()
            {
                //return $"\nID Gradebook:\t{gradebook}\nFull name:\t{FullName}\nBirth date:\t{date.ToShortDateString()}";
                return $"{gradebook}\t{FullName}\t{date.ToShortDateString()}";
            }

            // a) 2-вимірний індексатор з 2-ма індексами(перший індекс типу enum Subject, другий  типу int), який повертає доступ до певної оцінки з певного предмету.
            // Наприклад,  student[Subject.Programming, 2] = 12; // поставили з  предмету програмування  2-гу оцінку 12.
            public int this[Subject subj, int index]
            {
                get
                {
                    if (ArrElemPresent((int)subj, index))
                        return marks[(int)subj][index];
                    else
                        return int.MinValue;
            
                }
                set
                {
                    if (ArrElemPresent((int)subj, index))
                        if (CorrectMark(value))
                            marks[(int)subj][index] = value;
                }
            }

            //b) 2-вимірний індексатор з 2-ма індексами(рядком та цілим), який повертає доступ до певної оцінки з певного предмету.
            //Наприклад,  student["programming", 0] = 11; // поставили з  предмету програм 0-у оцінку 11.
            public int this[string subj, int index]
            {
                get
                {
                    switch (subj)
                    {
                        case "programming":
                            {
                                if (ArrElemPresent(0, index))
                                    return marks[0][index];
                                else
                                    return int.MinValue;
                            }
                        case "admin":
                            {
                                if (ArrElemPresent(1, index))
                                    return marks[1][index];
                                else
                                    return int.MinValue;
                            }
                        case "design":
                            {
                                if (ArrElemPresent(2, index))
                                    return marks[2][index];
                                else
                                    return int.MinValue;
                            }
                        default:
                            return int.MinValue;
                    }
                }
                
                set
                {
                    if (CorrectMark(value))
                    {
                        switch (subj)
                        {
                            
                            case "programming":
                                {
                                    if (ArrElemPresent(0, index))
                                        marks[0][index] = value;
                                    break;
                                }
                            case "admin":
                                {
                                    if (ArrElemPresent(1, index))
                                        marks[1][index] = value;
                                    break;
                                }
                            case "design":
                                {
                                    if (ArrElemPresent(2, index))
                                        marks[2][index] = value;
                                    break;
                                }
                            default:
                                {
                                    break;
                                }

                        }
                    }

                }
                
            }
        }

        static void Main(string[] args)
        {
            // тест на одному обєкті
            Student s = new Student("Pet789)(renko", "I,h2or 3", "Ivano5/-+, vych", new DateTime(2000, 7, 20)); // цифри й інші not letter символи проігнорить

            s.SetMarksPrograming(10, 9, 0, 11, -5, 7); // 0 і -5 не додасть
            s.SetMarksAdmin(7, 8, 7, 11);
            s.SetMarksDesign(6, 8);
            //Console.WriteLine(s);

            //Console.WriteLine(s.FullYearsOnToday); //виводимо повний вік, реалізував у окремим рядком Print()
            s.Group = "31PS9-1SPR"; //присвоюємо групу через автовластивість
            
            s.SetMark(Student.Subject.Admin, 4, 11); //не заміняє, бо нема такого елемента в масиві
            s.SetMark(Student.Subject.Admin, 0, 13); //не замінить, бо оцінка виходить за межі 1..12
            s.Print();
            //s.DeleteAllMarks();
            //s.EnterDataSudent(); //редагуємо ПІБ студента і групу
            //s.Print();

            //Створити масив(або List< Student >) з обєктів Студентів.
            const int size = 5;
            const string group = "31PS9-1SPR";
            Random rand = new Random();
            string[] names = { "Oleg", "Petro", "Olga", "Iryna", "Tetyana", "Fedir", "Mukolay", "Viktor", "Natalya", "Oksana" };
            string[] surnames = { "Kovalenko", "Petrenko", "Shevchenko", "Bondar", "Melnik", "Koval", "Gonchar", "Kravchuk", "Tkach", "Finyuk" };
            string[] middlename = { "Ole.", "Pet.", "Vik.", "Iv.", "Tar.", "Fed.", "Myk.", "Kost.", "Rom.", "Andr." };

            Student [] students = new Student [size];
            // щоб не прописувати врчуну генерю рандомно 5 студентів із рандомними оцінками і датами народження
            for (int i = 0; i < size; i++)
            {
                students[i] = new Student();
                students[i].EnterDataSudent(names[rand.Next(0, 9)], surnames[rand.Next(0, 9)], middlename[rand.Next(0, 9)], group);
                students[i].BirthDate = new DateTime(rand.Next(1995, 2001), rand.Next(1, 12), rand.Next(1, 28));
                for (int k = 0; k < rand.Next(0, 7); k++)
                {
                    students[i].SetMarksPrograming(rand.Next(1, 12));
                    students[i].SetMarksAdmin(rand.Next(1, 12));
                    students[i].SetMarksDesign(rand.Next(1, 12));
                }
                students[i].Print();
            }

            //список усіх студентів коротко
            Console.WriteLine("\n{0}", s);
            foreach (Student st in students)
                Console.WriteLine(st);

            // статистика по студентамм (окрім першого "Petrenko Ihor Ivanovych" який не входить у масив)
            Console.WriteLine($"\nQ-ty of students:\t{Student.QtyStudents}"); //к-ть студентів 6 (5 створено масивом, 1 окремим обєктом)
            Student.MinAvgMark(students);
            Student.MaxAvgMark(students);
            Student.CountGoodMark(students);

            // робота з індексаторами
            Console.WriteLine($"\nMark[{Student.Subject.Admin}, 0] :\t{s[Student.Subject.Admin, 0]}"); // виводимо через Enum
            s[Student.Subject.Admin, 0] = 12; // присвоюємо оцінку через Enum //замінить 7 на 12
            Console.WriteLine($"Mark[{Student.Subject.Admin}, 0] :\t{s[Student.Subject.Admin, 0]}"); //знову через Enum
            Console.WriteLine($"Mark[programming, 0] :\t{s["programming", 0]}"); // вивід через string
            s["programming", 0] = 7; // зміна через індекс string
            Console.WriteLine($"Mark[programming, 0] :\t{s["programming", 0]}"); // вивід
            Console.WriteLine($"Mark[uml, 0] :\t{s["uml", 0]}"); //через string неіснуючий предмет

               
            //  Nullable<double> для деякого екземпляра(ів)  класу Студент.
            Student s1 = new Student("Mykolaenko", "Viktor", "Petrovych", new DateTime(1999, 8, 24));
            s1.Print(); // без оцінок
            s1.SetMarksPrograming(5);
            s1.SetMarksPrograming(10);
            s1.SetMarksAdmin(9);
            s1.Print(); // з оцінками 


            Console.ReadKey();
        }
    }
}


