using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_7
{
    class Program
    {
        static List<Member> memberList = new List<Member>()
        {
            new Member(1,"Kareniks",1),
            new Member(2,"Alekseev",1),
            new Member(3,"Vodka",1),
            new Member(4,"Dmitrieva",3),
            new Member(5,"Smirnov",3),
            new Member(6,"Hapov",3),
            new Member(7,"Andreev",2),
            new Member(8,"Afanasyev",2)
        };

        static List<Department> departmentList = new List<Department>()
        {
            new Department(1,"Managment Department"),
            new Department(2,"Bookkeeping"),
            new Department(3, "Purchasing Department")
        };

        static List<DepMemLink> oneToMany = new List<DepMemLink>()
        {
            new DepMemLink(1,1),
            new DepMemLink(2,1),
            new DepMemLink(3,1),
            new DepMemLink(4,2),
            new DepMemLink(5,1),
            new DepMemLink(6,2),
            new DepMemLink(7,2),
            new DepMemLink(4,3),
            new DepMemLink(5,2),
            new DepMemLink(6,1),
            new DepMemLink(7,1),
            new DepMemLink(8,3)
        };

        static void Main(string[] args)
        {
            for (int i = 0; i < 160; i++) Console.Write('#');
            Console.WriteLine("All members which are sorted by Department ID\n");

            var allMemb = from t in departmentList
                          join s in memberList on t.property_1 equals s.departmentID into temp
                          select new { Department = t.property_1, Member = temp };

            foreach (var s in allMemb)
            {
                Console.WriteLine("!!!!!!!!!!!!!!DepartmentID!!!!!!!!!!!! = " + s.Department);
                foreach (var y in s.Member)
                    Console.WriteLine(y);
            }

            for (int i = 0; i < 160; i++) Console.Write('#');
            //=======================================================================================================================
            //=======================================================================================================================



            Console.WriteLine("\nAll members which surname starts at 'A'\n");

            var MembFirstA = from t in memberList where t.surname.StartsWith("A") select t;
            foreach (Member s in MembFirstA) Console.WriteLine(s);


            for (int i = 0; i < 160; i++) Console.Write('#');
            //=======================================================================================================================
            //=======================================================================================================================



            Console.WriteLine("\nAll departments and quantity of members\n");


            var DepartAndQuantity = from a in departmentList
                                    join b in memberList on a.property_1 equals b.departmentID into temp
                                    select new { Department = a, Quantity = temp.Count() };


            foreach (var c in DepartAndQuantity)
            {
                Console.WriteLine(c.Department + "\nQuantity of members = " + c.Quantity);
            }
            for (int i = 0; i < 160; i++) Console.Write('#');
            //=======================================================================================================================
            //=======================================================================================================================
            Console.WriteLine("\nAll departments, where all member's surname starts 'A' \n");

            var DepartAllMembFirstA = (from s in departmentList
                                       from t in memberList
                                       group t by t.departmentID into g
                                       where g.All(t => t.surname.StartsWith("A"))
                                       select new { Department = (from s in departmentList where s.property_1 == g.Key select s) });

            foreach (var s in DepartAllMembFirstA)
            {
                foreach (var b in s.Department)
                {

                    Console.WriteLine(b);
                }


            }

            for (int i = 0; i < 160; i++) Console.Write('#');
            //=======================================================================================================================
            //=======================================================================================================================





            Console.WriteLine("\nAll departments, where is at least one member which surname starts 'A' \n");

            var DepartMembFirstA = (from s in departmentList
                                    from t in memberList
                                    group t by t.departmentID into g
                                    where g.Any(t => t.surname.StartsWith("A"))
                                    select new { Department = (from s in departmentList where s.property_1 == g.Key select s) });

            foreach (var s in DepartMembFirstA)
            {
                foreach (var b in s.Department)
                {

                    Console.WriteLine(b);
                }


            }


            for (int i = 0; i < 160; i++) Console.Write('#');
            //=======================================================================================================================
            //=======================================================================================================================

            Console.WriteLine("\nAll departments and all members in this department  \n");

            var AllDepartAndMembers = (from t in memberList
                                       join r in oneToMany on t.memberID equals r.memberID into temp
                                       from t1 in temp
                                       group t by t1.departmentID into g

                                       from t in departmentList
                                       where t.property_1==g.Key

                                       select new {  Members=g, department=t});

            foreach (var s in AllDepartAndMembers)
                {
                for (int i = 0; i < 80; i++) Console.Write('_');
                Console.WriteLine(s.department);
                for (int i = 0; i < 80; i++) Console.Write('_');
                foreach (var f in s.Members) Console.WriteLine(f);

                }

            //=======================================================================================================================
            //=======================================================================================================================



            Console.WriteLine("\nAll departments and quantity of members in this department  \n");


            var AllDepartAndQuantityOfMemb = (from t in memberList
                                              join r in oneToMany on t.memberID equals r.memberID into temp
                                              from t1 in temp
                                              group t by t1.departmentID into g

                                              from t in departmentList
                                              where t.property_1 == g.Key

                                              select new { Quantity = g.Count(), department = t });

            foreach (var s in AllDepartAndQuantityOfMemb) Console.WriteLine(s.department + "\nQuantity of members = " + s.Quantity);


            //=======================================================================================================================
            //=======================================================================================================================



            Console.ReadLine();

        }
    }
}
