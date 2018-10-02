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
	вік (повних років) ( метод, приймає дату (DateTime) і обчислює вік студента на цю дату ( читайте про оператор віднімання ),
	властивість вік(повних років) на сьогоднішню дату
	група ( АВТОвластивість для читання і запису )
	зміна оцінки з певного предмету за певним номером( void SetMark(Subject subject, int numLesson, int mark )), виконувати перевірку номеру пари та самої оцінки на коректність(відповідні приватні методи перевірки)
	очищення всіх оцінок (метод, без параметрів)
	вивід інформації про студента( у тому числі  оцінки )
	введення даних про студента(імя, прізвище, по-батькові, група)

* Передбачити правильні розрахунки у класі, якщо оцінки не виставлені(null)
Перевірити роботу класу.

Створити масив(або List<Student>) з обєктів Студентів. 
	Знайти студента з найбільшим та найменшим середнім балом(визначити у класі Студент відповідні статичні методи). 
	*Використайте методи бібліотеки Linq: Max() та Min() ( students.Max( st => st.Average))
	Знайти  кількість студентів, які здали певний предмет добре(середній бал більше рівний 7), статичний метод приймає список студентів(Count(), лямбда)	

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_student
{
    class Program
    {
        internal class Student
        {
            string surname;
            string name;
            string middlename;
            string group;
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
            enum Subject { Programming, Admin, Design };
            private static string nameUniversity = "Some IT University";
            static int count;

            static Student()
            {
                count = 0;
            }

            public Student()
            { }

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
                    // як отримувати в сеттер стрінг, парсити його і віддавати DateTime?
                    date = value;
                }
            }

            // *для видалення зайвих знаків з імен визначити приватний метод*
            private string CorrectName(string name)
            {
                if (name == null)
                    name = "noname";
                if (String.IsNullOrEmpty(name))
                    name = "noname";
                foreach(var c in name)
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
                if (element >= minMark && element <= maxMark )
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
            public double AvgPrograming
            {
                get
                {
                    double avg = 0;
                    int subj = (int)Subject.Programming;
                    if (marks[subj].Length != 0)
                    {
                        for (int i = 0; i < marks[subj].Length; i++)
                            avg += (double)marks[subj][i];
                        return avg / marks[subj].Length;
                    }
                    else 
                        return avg;
                }
            }

            //середній бал з адміністрування(read-only властивість,)
            public double AvgAdmin
            {
                get
                {
                    double avg = 0;
                    int subj = (int)Subject.Admin;
                    if (marks[subj].Length != 0)
                    {
                        for (int i = 0; i < marks[subj].Length; i++)
                            avg += (double)marks[subj][i];
                        return avg / marks[subj].Length;
                    }
                    else
                        return avg;
                }
            }

            //середній бал з дизайну(read-only властивість)
            public double AvgDesign
            {
                get
                {
                    double avg = 0;
                    int subj = (int)Subject.Design;
                    if (marks[subj].Length != 0)
                    {
                        for (int i = 0; i < marks[subj].Length; i++)
                            avg += (double)marks[subj][i];
                        return avg / marks[subj].Length;
                    }
                    else
                        return avg;
                }
            }

            //середній бал успішності з усіх предметів ( read-only властивість)
            public double AvgAll
            {
                get
                {
                    double avg = 0;
                    int count = 0;
                    for(int k = 0; k < marks.Length; k++)
                    if (marks[k].Length != 0)
                    {
                        for (int i = 0; i < marks[k].Length; i++)
                            {
                                avg += (double)marks[k][i];
                                ++count;
                            }
                    }
                    return avg / count;
                }
            }

            public override string ToString()
            {
                return $"\nID Gradebook:\t{group}{gradebook}\nFull name:\t{FullName}\nDate:\t\t{date}\n{PrintMarks(marks, "Marks:\n")}";
            }
        }

        static void Main(string[] args)
        {
            DateTime dt = DateTime.Now;            
            Student s = new Student("Pet789)(renko", "I,h2or 3", "Ivano5/-+, vych", dt);
     
            s.SetMarksPrograming(10, 9, 0, 11, -5, 7 ); // 0 і -5 не додасть
            s.SetMarksAdmin(7, 8, 7, 11);
            s.SetMarksDesign(6, 8);
            Console.WriteLine(s);
            Console.WriteLine($"Average marks of Programming:\t{s.AvgPrograming}");
            Console.WriteLine($"Average marks of Admin:\t{s.AvgAdmin}");
            Console.WriteLine($"Average marks of Design:\t{s.AvgDesign}");
            Console.WriteLine($"Average marks of all subjects:\t{s.AvgAll}");
            Console.WriteLine($"Q-ty of students:\t{Student.QtyStudents}");
            
        }
    }
}
