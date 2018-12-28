using System;

using System.Collections.Generic;

using System.Linq;

namespace ConsoleApp8

{

class Program

{

public class Group //отделы

{

public int g_id; //id отдела 
            public string name;//его название

public Group(int g, string n)

{

this.g_id = g;
                this.name = n;

}

public override string ToString()

{

return "(group_id=" + this.g_id.ToString() + "; name_of_group=" + this.name;

}

}

public class Worker

{

public int w_id; //id сотрудника 
            public string surname;//фамилия

public int g_id;

public Worker(int i, string s, int v) //конструктор

{

this.w_id = i;
                surname = s; this.g_id = v;

}

public override string ToString()

{

return "(worker_id=" + this.w_id.ToString() + "; surname=" + this.surname + "; group_id="

+ this.g_id + ")";

}

}

static List<Worker> employees = new List<Worker>() { new Worker(1, "Флименко", 2), new Worker(2, "Зубарева", 1), new Worker(3, "Константинов", 3), new Worker(4, "Горячкин", 3), new Worker(5, "Любимова", 3), new Worker(6, "Образцова", 1), new Worker(7, "Устинова", 2), new Worker(8, "Булкин", 2), new Worker(9, "Лебедев", 2), new Worker(10, "Афанасьев", 3), new Worker(11, "Афанасьева", 1), new Worker(12, "Жарова", 3), new Worker(13, "Дружинина", 2), new Worker(14, "Петров", 3), new Worker(15, "Смольный", 1),

};

static List<Group> departments = new List<Group> { new Group(1, "Кадровое бюро"), new Group(2, "Контроль качества"), new Group(3, "Отдел разработки"),

};

public class WorkersGroup

{

public int g_id;
            public int w_id;

public WorkersGroup(int w, int g)

{
                w_id = w;
 g_id = g;

}

}

static List<WorkersGroup> DepartmentEmployees = new List<WorkersGroup> { new WorkersGroup(1, 2), new WorkersGroup(2, 1), new WorkersGroup(3, 3), new WorkersGroup(4, 3), new WorkersGroup(5, 3), new WorkersGroup(6, 1), new WorkersGroup(7, 2), new WorkersGroup(8, 2), new WorkersGroup(9, 2), new WorkersGroup(10, 3), new WorkersGroup(11, 1), new WorkersGroup(12, 3), new WorkersGroup(13, 2), new WorkersGroup(14, 3), new WorkersGroup(15, 1),

};

static void Main(string[] args)

{

Console.WriteLine("1 - М\n");

Console.WriteLine("\nРаботник-отделы: \n");
            var a = from x in departments

join y in employees on x.g_id equals y.g_id into g
                    orderby x.g_id ascending

select new { Dep = x, Emps = g.OrderBy(g => g.surname) };
            foreach (var x in a)

{

Console.WriteLine(x.Dep + ":");
                foreach (var y in x.Emps)

{

Console.WriteLine(" " + y);

}

}

Console.WriteLine("Фамилия на А:\n");
            IEnumerable<object> с = from x in employees

where x.surname[0] == 'А' //Условие 
                                    orderby x.surname ascending //Сортировка 
                                    select x; foreach (var x in с)

{

Console.WriteLine(x);

}

Console.WriteLine("Кол-во работников:\n");
            var b = from x in departments

join y in employees on x.g_id equals y.g_id into g
                    select new { Dep = x.name, Emps = g.Count() }; foreach (var x in b)

{

Console.WriteLine(x.Dep + ": {0}", x.Emps);

}

Console.WriteLine("Отделы, в которых фамилии всех сотрудников начинаются на \"А\":\n");
            var d = from x in departments

join y in employees on x.g_id equals y.g_id into g
                    where g.All(g => g.surname[0] == 'А') select new { Dep = x.name, Emps = g }; foreach (var x in d)

{

Console.WriteLine(x.Dep + ":");
                foreach (var y in x.Emps)

{

Console.WriteLine(" " + y);

}

}

Console.WriteLine("\nОтделы, в которых есть сотрудники с фамилией на \"А\":\n");
            var e = from x in departments

join y in employees on x.g_id equals y.g_id into g
                    where g.Any(g => g.surname[0] == 'А') select new { Dep = x.name, Emps = g }; foreach (var x in e)

{

Console.WriteLine(x.Dep + ":");
                foreach (var y in x.Emps)

{

Console.WriteLine(" " + y);

}

}

Console.WriteLine("\nМ - М\n");

Console.WriteLine("\nРаботники-отделы:\n");
            var f = from x in departments

join y in DepartmentEmployees on x.g_id equals y.g_id into temp1
                    from t1 in temp1

join z in employees on t1.w_id equals z.w_id into temp2
                    from t2 in temp2

orderby t1.g_id, t2.surname ascending
                    group t2 by x into g select g;
            foreach (var x in f)

{

Console.WriteLine(x.Key + ":");
                foreach (var y in x)

{

Console.WriteLine(" " + y);

}

}

Console.WriteLine("Кол-во работников:\n");
            var j = from x in departments

join y in DepartmentEmployees on x.g_id equals y.g_id into temp1
                    from t1 in temp1

join z in employees on t1.w_id equals z.g_id into temp2
                    from t2 in temp2 group t2 by x.name into g

select new { Dep = g.Key, Emps = g.Count() };
            foreach (var x in j)

{

Console.WriteLine(x.Dep + ": {0}", x.Emps);

}

Console.ReadLine();

}

}

}