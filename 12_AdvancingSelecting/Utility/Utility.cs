using _12_AdvancingSelecting.Entities;
using _12_AdvancingSelecting.Models;
using _12_AdvancingSelecting.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12_AdvancingSelecting.Utility
{
    public static class Utility
    {
        // grouping example 1
        public static void GroupingExample1()
        {
            using (var context = new RepositoryContext())
            {
                var query = context.Books.GroupBy(x => x.PublisherId).Select(z => z.Count());
                var result = query.ToList();
                Console.WriteLine(result.GetType());
            };
            /*
             SELECT COUNT(*)
                FROM [Books] AS [b]
                GROUP BY [b].[PublisherId]
             */
        }

        // grouping example 2
        public static void GroupingExample2()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            group books by books.PublisherId into g
                            select new
                            {
                                count = g.Count()
                            };
                var result = query.ToList();
                Console.WriteLine(result.GetType());
            };
            /*
                 SELECT COUNT(*) AS [count]
                 FROM [Books] AS [b]
                 GROUP BY [b].[PublisherId]
             */
        }


        // grouping example 3
        public static void GroupingWithJoinExample3()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            join publishers in context.Publishers on books.PublisherId equals publishers.Id
                            group new { books, publishers } by new { publishers.Name, books.PublisherId } into g
                            select new
                            {
                                PublisherName = g.Key.Name,
                                Count = g.Count(),
                                MinValue = g.Min(x => x.books.Price)
                            };
                var result = query.ToList();
                Console.WriteLine(result.GetType());
            };
            /*
                SELECT [p].[Name] AS [PublisherName], COUNT(*) AS [Count], MIN([b].[Price]) AS [MinValue]
                FROM [Books] AS [b]
                INNER JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
                GROUP BY [p].[Name], [b].[PublisherId]
             */
        }


        // grouping example 4
        public static void GroupingWithJoinWithModelExample4()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            join publishers in context.Publishers on books.PublisherId equals publishers.Id
                            group new { books, publishers } by new { publishers.Name, books.PublisherId } into g
                            select new PublisherStatsDto
                            {
                                PublisherName = g.Key.Name,
                                Count = g.Count(),
                                MinValue = g.Min(x => x.books.Price)
                            };
                var result = query.ToList();
                Console.WriteLine(result.GetType());
            };
            /*
               SELECT [p].[Name] AS [PublisherName], COUNT(*) AS [Count], MIN([b].[Price]) AS [MinValue]
               FROM [Books] AS [b]
               INNER JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
               GROUP BY [p].[Name], [b].[PublisherId]
             */
        }


        // grouping  Grouping Using Where And Having  example 5
        public static void GroupingUsingWhereAndHavingExample5()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            join publishers in context.Publishers on books.PublisherId equals publishers.Id
                            where books.Price > 20
                            group new { books, publishers } by new { publishers.Name, books.PublisherId } into g
                            where g.Key.Name.Contains("Microsoft")//having
                            select new PublisherStatsDto
                            {
                                PublisherName = g.Key.Name,
                                Count = g.Count(),
                                MinValue = g.Min(x => x.books.Price)
                            };
                var result = query.ToList();
                Console.WriteLine(result.GetType());
            };
            /*
               SELECT [p].[Name] AS [PublisherName], COUNT(*) AS [Count], MIN([b].[Price]) AS [MinValue]
               FROM [Books] AS [b]
               INNER JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
               WHERE [b].[Price] > 20.0
               GROUP BY [p].[Name], [b].[PublisherId]
               HAVING [p].[Name] LIKE N'%Microsoft%'
             */
        }

        // grouping with inner join using where/having example 6
        public static void GroupingUsingWhereAndHavingExample6()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            join publishers in context.Publishers on books.PublisherId equals publishers.Id
                            where EF.Functions.Like(books.Title, "%C#%")
                            group new { books, publishers } by new { publishers.Name, books.PublisherId } into g
                            where g.Key.Name.Contains("Microsoft")
                            select new PublisherStatsDto
                            {
                                PublisherName = g.Key.Name,
                                Count = g.Count(),
                                MinValue = g.Min(x => x.books.Price)
                            };
                var result = query.ToList();
                Console.WriteLine(result.GetType());

                /*
                 SELECT [p].[Name] AS [PublisherName], COUNT(*) AS [Count], MIN([b].[Price]) AS [MinValue]
                 FROM [Books] AS [b]
                 INNER JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
                 WHERE [b].[Title] LIKE N'%C#%'
                 GROUP BY [p].[Name], [b].[PublisherId]
                 HAVING [p].[Name] LIKE N'%Microsoft%' 
                */
            };

        }


        // Example 7
        public static void GroupJoinxample7()
        {
            using (var context = new RepositoryContext())
            {
                List<Publisher> publisherData = context.Publishers.ToList();
                List<Book> booksData = context.Books.ToList();

                var query = publisherData.GroupJoin(booksData,
                                                            p => p.Id,
                                                            b => b.PublisherId,
                                                            (publishers, books) => new
                                                            {
                                                                publishername = publishers.Name,
                                                                books = books
                                                            });


                var result = query.ToList();
                Console.WriteLine(result.GetType());

                /*
                
                */
            };


        }


        // Example 8
        public static void LeftJoinJoinxample8()
        {
            using (var context = new RepositoryContext())
            {
                List<Publisher> publisherData = context.Publishers.ToList();
                List<Book> booksData = context.Books.ToList();

                var query = from books in context.Books
                            join publishers in context.Publishers on books.PublisherId equals publishers.Id into gr
                            from publishersL in gr.DefaultIfEmpty()
                            select new
                            {
                                books,
                                publishersL
                            };


                var result = query.ToList();
                Console.WriteLine(result.GetType());

                /*
                SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title], [p].[PublisherId], [p].[Name]
                FROM [Books] AS [b]
                LEFT JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
                */
            };


        }


        // Inner Join Example 9
        public static void InnerJoin9()
        {
            using (var context = new RepositoryContext())
            {
                var query = context.Books.SelectMany(x => x.Reviews, (b, r) => new
                {
                    b.Title,
                    r.NumStars
                });



                var result = query.ToList();
                Console.WriteLine(result.GetType());

                /*
               SELECT [b].[Title], [r].[NumStars]
               FROM [Books] AS [b]
               INNER JOIN [Reviews] AS [r] ON [b].[BookId] = [r].[BookId]
                */
            };


        }

        // Select Many example 10
        public static void SelectManyExample10()
        {

            PetOwner[] petOwners =
            { new PetOwner { Name="Higa",
                    Pets = new List<string>{ "Scruffy", "Sam" } },
                new PetOwner { Name="Ashkenazi",
                    Pets = new List<string>{ "Walker", "Sugar" } },
                new PetOwner { Name="Price",
                    Pets = new List<string>{ "Scratches", "Diesel" } },
                new PetOwner { Name="Hines",
                    Pets = new List<string>{ "Dusty" } } };


            var query =
                petOwners
                .SelectMany(petOwner => petOwner.Pets, (petOwner, petName) => new { petOwner, petName })
                //.Where(ownerAndPet => ownerAndPet.petName.StartsWith("S"))
                .Select(ownerAndPet =>
                        new
                        {
                            Owner = ownerAndPet.petOwner.Name,
                            Pet = ownerAndPet.petName
                        }
                );



            var query1 =
              petOwners
              .SelectMany(petOwner => petOwner.Pets);


            // Print the results.
            foreach (var obj in query1)
            {
                Console.WriteLine(obj);
            }

            /*
          
            */
        }



        // Select Many Example 11 - generates cross join
        public static void SelectMany_CrossJoin_GeneratingExample11()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            from publishers in context.Publishers
                            select new
                            {
                                books,
                                publishers
                            };



                var result = query.ToList();


                /*
              SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title], [p].[PublisherId], [p].[Name]
              FROM [Books] AS [b]
              CROSS JOIN [Publishers] AS [p]
                */
            };


        }

        // Select Many Example 12 - generates inner join
        public static void SelectMany_Inner_GeneratingExample12()
        {
            using (var context = new RepositoryContext())
            {
                var query = from books in context.Books
                            from publishers in context.Publishers.Where(p => p.Id == books.PublisherId)
                            select new
                            {
                                books,
                                publishers
                            };



                var result = query.ToList();


                /*
             SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title], [p].[PublisherId], [p].[Name]
             FROM [Books] AS [b]
             INNER JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
            */
            };


        }

        // Select Many Example 1 - generates inner join
        public static void CrossApplyExample13()
        {
            using (var context = new RepositoryContext())
            {

                var query = from books in context.Books
                            from publishers in context.Publishers.Select(p => p.Id + " " + books.Title)
                            select new
                            {
                                books,
                                publishers
                            };


                var result = query.ToList();

                /*
                    SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title], (CAST([p].[PublisherId] AS nvarchar(max)) + N' ') + [b].[Title] AS [publishers]
                    FROM [Books] AS [b]
                    CROSS APPLY [Publishers] AS [p]
                */
            };


        }
        //IEnumerable and IQueryabnle
        // Example 14
        public static void IEnumerableExample14()
        {
            using (var context = new RepositoryContext())
            {

                // IEnumerable = works with delegates(Linq to entoty)
                IEnumerable<Book> books = context.Books.Where(x => x.Publisher.Name.Contains("A"));
                var booksWithPrice = books.Where(x => x.Price > 50);

                var result = booksWithPrice.ToList();

                /*
                  SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title], (CAST([p].[PublisherId] AS nvarchar(max)) + N' ') + [b].[Title] AS [publishers]
                  FROM [Books] AS [b]
                  CROSS APPLY [Publishers] AS [p]  
                */
            };

        }
        //IEnumerable and IQueryabnle
        // Example 15
        public static void IQExample15()
        {
            using (var context = new RepositoryContext())
            {

                //   IQueryable - works wirh expression tress - linq to sql
                IQueryable<Book> books = context.Books.Where(x => x.Publisher.Name.Contains("A"));
                var booksWithPrice = books.Where(x => x.Price > 50);

                var result = booksWithPrice.ToList();

                /*
                 SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title]
                 FROM [Books] AS [b]
                 INNER JOIN [Publishers] AS [p] ON [b].[PublisherId] = [p].[PublisherId]
                 WHERE ([p].[Name] LIKE N'%A%') AND ([b].[Price] > 50.0)
                
                */
            }
        }

        //IEnumerable and IQueryabnle
        // Example 16
        public static void AsnoTrackingExample16()
        {
            using (var context = new RepositoryContext())
            {

                var book = context.Books.AsNoTracking().First();
                book.Title = "New Title2";
                context.SaveChanges();
                context.Books.ToList();

                /*
               
                */
            };
        }

        //IEnumerable and IQueryabnle
        // Example 17
        public static void AsTrackingExample17()
        {
            using (var context = new RepositoryContext())
            {

                var book = context.Books.First();
                book.Title = "New Title1";
                context.SaveChanges();
                context.Books.ToList();

                /*
              
                */
            };
        }

        
        // Example 18 with all filters
        public static void SubqueryExample18()
        {
            using (var context = new RepositoryContext())
            {

                var query = context.Books.Where(x => x.Reviews.All(z => z.NumStars >= 4));

                var result = query.ToList();
                Console.WriteLine();

                /*
              SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title]
              FROM [Books] AS [b]
              WHERE NOT EXISTS (
                SELECT 1
                FROM [Reviews] AS [r]
                WHERE ([b].[BookId] = [r].[BookId]) AND ([r].[NumStars] < CAST(4 AS smallint)))
                */
            };
        }


        // Example 19 subquery with any
        public static void SubqueryExample19()
        {
            using (var context = new RepositoryContext())
            {

                var query = from books in context.Books
                            select new
                            {
                                AvgRating = books.Reviews.Any() ? books.Reviews.Average(z => z.NumStars) : 0
                            };


                var result = query.ToList();
                Console.WriteLine();

                /*
             SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title]
             FROM [Books] AS [b]
             WHERE NOT EXISTS (
                 SELECT 1
                 FROM [Reviews] AS [r]
                 WHERE ([b].[BookId] = [r].[BookId]) AND ([r].[NumStars] < CAST(4 AS smallint)))
                */
            };
        }

        // Example 20 subquery with any
        public static void SubqueryExample20()
        {
            using (var context = new RepositoryContext())
            {

                var query = context.Books.Where(x => x.Reviews.Any(z => z.NumStars >= 4));


                var result = query.ToList();
                Console.WriteLine();

                /*
            SELECT [b].[BookId], [b].[Description], [b].[Price], [b].[PublishedOn], [b].[PublisherId], [b].[Title]
            FROM [Books] AS [b]
            WHERE EXISTS (
                SELECT 1
                FROM [Reviews] AS [r]
                WHERE ([b].[BookId] = [r].[BookId]) AND ([r].[NumStars] >= CAST(4 AS smallint)))
                */
            };
        }


        // Example 21  with any
        public static void Example21()
        {
            using (var context = new RepositoryContext())
            {

                var query = from books in context.Books
                            select new
                            {
                                BookTitle = books.Title.Contains("A") ? books.Title : "Without A"
                            };


                var result = query.ToList();
                Console.WriteLine();

                /*
          SELECT CASE
             WHEN [b].[Title] LIKE N'%A%' THEN [b].[Title]
             ELSE N'Without A'
          END AS [BookTitle]
          FROM [Books] AS [b]
                */
            };
        }


        // Example 22 
        public static void Example22()
        {
            using (var context = new RepositoryContext())
            {

                var query = from books in context.Books
                            select new
                            {
                                BookTitle = books.Title ?? "Title"
                            };


                var result = query.ToList();
                Console.WriteLine();

                /*
                 SELECT COALESCE([b].[Title], N'Title') AS [BookTitle]
                 FROM [Books] AS [b]
                */
            };
        }

        // Example 23 pagination
        public static void Pagination23()
        {
            using (var context = new RepositoryContext())
            {

             //   PAGINATIONS
                int page = 1;
                int pageSize = 2;

                var query = context.Books.OrderBy(x => x.Title).Skip((page - 1) * pageSize).Take(pageSize);

               
                var result = query.ToList();
                Console.WriteLine();

                /*
                
                */
            };
        }

        // Example 24 sql raw
        public static void Sqlraw24()
        {
            using (var context = new RepositoryContext())
            {

                var query = context.Books.FromSqlRaw("SELECT * FROM Books WHERE Price > 0").Where(x => x.Publisher.Name.Contains("A")).Select(x => new
                {
                    Name = x.Title,
                });

                var result = query.ToList();
                               
                Console.WriteLine();

                /*
                SELECT [_].[Title] AS [Name]
                FROM (
                    SELECT * FROM Books WHERE Price > 0
                    ) AS [_]
                INNER JOIN [Publishers] AS [p] ON [_].[PublisherId] = [p].[PublisherId]
                WHERE [p].[Name] LIKE N'%A%'
                */
            };
        }


    }


    class PetOwner
    {
        public string Name { get; set; }
        public List<string> Pets { get; set; }
    }
}



