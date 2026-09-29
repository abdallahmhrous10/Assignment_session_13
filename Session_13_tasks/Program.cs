using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using IOFile = System.IO.File;
namespace Session_13_tasks
{
    internal class Program
    {
        public class SameCharactersComparer:IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                if(x == null || y == null)
                {
                    return false;
                }

                return string.Concat(x.OrderBy(c => c))
                    == string.Concat(y.OrderBy(c => c));
            }

            public int GetHashCode(string obj)
            {
               return  string.Concat(obj.OrderBy(c => c)).GetHashCode();
            }
        }
        static void Main(string[] args)
        {
            #region LINQ - Element Operators

            #region Q1
            var Product = ListGenerator.ProductsList;

            var query1 = Product.First(x => x.UnitsInStock == 0);
             Console.WriteLine(query1);
            #endregion

            #region Q2
            var query2 = Product.FirstOrDefault(x => x.UnitPrice > 1000);
             Console.WriteLine(query2);
            #endregion

            #region Q3
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var query3 = Arr.Where(x => x > 5);
            Console.WriteLine(query3.ElementAt(1));
            #endregion

            #endregion


            #region LINQ - Aggregate Operators

            #region Q1
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var query4 = Arr2.Count(x => x % 2 != 0);
           Console.WriteLine($"count of odd nums: {query4} ");
            #endregion

            #region Q2

            var Customer = ListGenerator.CustomersList;

            var result = Customer.Select(x => new
            {
                customername = x.CustomerName,
                orders = x.Orders.Count(),
            });

            foreach (var item in result)
            {
               Console.WriteLine($"{item.customername} => {item.orders}");
            }


            #endregion

            #region Q3
            var query5 = Product.GroupBy(x => x.Category).Select(x => new { Category = x.Key, Productcount = x.Count() });
            foreach (var item in query5)
            {
               Console.WriteLine($"{item.Category} => {item.Productcount}");
            }
            #endregion

            #region Q4
            int[] Arr3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var query6 = Arr3.Sum();
            Console.WriteLine(query6);
            #endregion

            #region Q5
            string[] words = IOFile.ReadAllLines("dictionary_english.txt");
            var query7 = words.Sum(x => x.Length);
            Console.WriteLine(query7);
            #endregion

            #region Q6
            var query8 = words.Min(w => w.Length); ;
             Console.WriteLine(query8);
            #endregion

            #region Q7
            var query9 = words.Max(w => w.Length); ;
            Console.WriteLine(query9);
            #endregion

            #region Q8
            var query10 = words.Average(w => w.Length);
            // Console.WriteLine(query10);
            #endregion

            #region Q9
            var query11 = Product.GroupBy(x => x.Category).Select(x => new
            {
                category = x.Key,
                units = x.Sum(p => p.UnitsInStock)
            });
            foreach (var item in query11)
            {
                Console.WriteLine($"{item.category} => {item.units}");
            }
            #endregion

            #region  Q10
            var query12 = Product.GroupBy(x => x.Category).Select(x => new
            {
                category = x.Key,
                cheapestprice = x.Min(p => p.UnitPrice)
            });
            foreach (var item in query12)
            {
                 Console.WriteLine($"{item.category} => {item.cheapestprice}");
            }


            #endregion

            #region Q11
            var query13 =
                from p in Product
                group p by p.Category into g
                let minPrice = g.Min(p => p.UnitPrice)
                from p in g
                where p.UnitPrice == minPrice
                select p;
            foreach (var item in query13)
            {
               Console.WriteLine(item);
            }

            #endregion

            #region Q12
            var query14 = Product.GroupBy(x => x.Category).Select(x => new
            {
                category = x.Key,
                highestprice = x.Max(p => p.UnitPrice)
            });
            foreach (var item in query14)
            {
                Console.WriteLine($"{item.category} => {item.highestprice}");
            }
            #endregion

            #region Q13
            var query15 =
            from p in ListGenerator.ProductsList
            group p by p.Category into g
            let maxPrice = g.Max(p => p.UnitPrice)
            from p in g
            where p.UnitPrice == maxPrice
            select p;

            foreach (var item in query15)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Q14

            var query16 = Product.GroupBy(x => x.Category).Select(x => new
            {
                category = x.Key,
                cheapestprice = x.Average(p => p.UnitPrice)
            });
            foreach (var item in query16)
            {
                Console.WriteLine($"{item.category} => {item.cheapestprice}");
            }

            #endregion

            #endregion

            #region  LINQ - Set Operators

            #region Q1
            var query17 = Product.Select(x => x.Category).Distinct();
            foreach(var item in query17)
            {
               Console.WriteLine(item);
            }
            #endregion

            #region Q2
            var productFirstLetters = Product.Select(x => x.ProductName[0]);

            var customerFirstLetters = Customer.Select(x => x.CustomerName[0]);

            var query18 = productFirstLetters.Union(customerFirstLetters);

            foreach (var item in query18)
            {
                 Console.WriteLine(item);
            }
            #endregion

            #region Q3
            var productFirstLetters2 = Product.Select(x => x.ProductName[0]);

            var customerFirstLetters2 = Customer.Select(x => x.CustomerName[0]);

            var query19 = productFirstLetters2.Intersect(customerFirstLetters2);

            foreach (var item in query19)
            {
              //  Console.WriteLine(item);
            }
            #endregion

            #region Q4
            var productFirstLetters3 = Product.Select(x => x.ProductName[0]);

            var customerFirstLetters3 = Customer.Select(x => x.CustomerName[0]);

            var query20 = productFirstLetters3.Except(customerFirstLetters3);

            foreach (var item in query20)
            {
                Console.WriteLine(item);
            }
            #endregion

            #region Q5
            var productlast3Letters = Product.Select(x => x.ProductName.Substring(x.ProductName.Length -3));

            var customerlast3Letters = Customer.Select(x => x.CustomerName.Substring(x.CustomerName.Length - 3));

            var query21 = productlast3Letters.Concat(customerlast3Letters);

            foreach (var item in query21)
            {
                Console.WriteLine(item);
            }
            #endregion

            #endregion


            #region LINQ - Partitioning Operators

            #region Q1

            var query22 = Customer.Where(c => c.Region == "WA").
                SelectMany(C => C.Orders).Take(3);
            foreach (var item in query22)
            {
                Console.WriteLine(item);
            }

            #endregion

            #region Q2
            var query23 = Customer.Where(c => c.Region == "WA").
            SelectMany(C => C.Orders).Skip(2);
            foreach (var item in query23)
            {
               Console.WriteLine(item);
            }
            #endregion

            #region Q3
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var query24 = numbers.TakeWhile((v,i) => v >= i);
            foreach (var item in query24)
            {
                 Console.WriteLine(item);
            }
            #endregion

            #region Q4
            int[] numbers1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var query25 = numbers1.SkipWhile(x => x % 3 != 0);
            foreach (var item in query25)
            {
                 Console.WriteLine(item);
            }
            #endregion

            #region Q5
            int[] numbers2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var query26 = numbers2.SkipWhile((v, i) => v >= i);                                    
            foreach (var item in query26)
            {
                Console.WriteLine(item);
            }
            #endregion

            #endregion

            #region LINQ - Quantifiers

            #region Q1
            var query27 = words.Any(x => x.Contains("ei"));
            Console.WriteLine(query27);
            #endregion

            #region Q2
            var query28 = Product.GroupBy(p => p.Category).
                Where(g => g.Any(p => p.UnitsInStock == 0));

            foreach (var group in query28)
            {
                 Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }
            #endregion

            #region Q3
            var query29 = Product.GroupBy(p => p.Category).
            Where(g => g.All(p => p.UnitsInStock > 0));

            foreach (var group in query29)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);
                }
            }
            #endregion
            #endregion


            #region LINQ – Grouping Operators

            #region Q1

            List<int> numbers3 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var query30 = numbers3.GroupBy(x => x % 5);

            foreach (var group in query30)
            {
                 Console.WriteLine($"Number with a remainder of {group.Key} divided by 5: ");

                foreach (var item in group)
                {
                     Console.WriteLine(item);
                }
            }

            #endregion

            #region Q2
            var query31 = words.GroupBy(x => x[0]);

            foreach (var group in query31)
            {
                Console.WriteLine($"Letter: {group.Key}");

                foreach (var word in group)
                {
                   Console.WriteLine(word);
                }

                Console.WriteLine();
            }

            #endregion

            #region Q3
            String[] Arr4= { "from", "salt", "earn", "last", "near", "form" };
            var query32 = Arr4.GroupBy(w => w, new SameCharactersComparer());

            foreach (var group in query32)
            {
                Console.WriteLine("Group:");

                foreach (var word in group)
                {
                    Console.WriteLine(word);
                }

                Console.WriteLine();
            }
            #endregion

            #endregion
        }
    }
}

