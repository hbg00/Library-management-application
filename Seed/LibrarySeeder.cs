using BookStore.Data;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Seed
{
    public class LibrarySeeder
    {
        public static void SeedDatat(IApplicationBuilder applicationBuilder) 
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();
                context.Database.EnsureCreated();

                if (!context.Languages.Any())
                {
                    new Language()
                    {
                        Id = 1,
                        Name ="Polish"
                    };
                    new Language()
                    {
                        Id = 2,
                        Name = "English"
                    };
                    new Language()
                    {
                        Id = 3,
                        Name = "German"
                    };
                    context.SaveChanges();
                }

                if(!context.Books.Any()) 
                {
                    new Book()
                    {
                        ISBN = "8373272267",
                        Title = "Lalka",
                        Description = "Wydanie z opracowaniem.\r\n\r\nDo Warszawy powraca Stanisław Wokulski. Jego plenipotent, Ignacy Rzecki, bacznie śledzi wszystkie plotki krążące wokół jego przyjaciela. Nie ma pojęcia o wielkim i jeszcze skrywanym uczuciu Wokulskiego do pięknej i próżnej Izabeli Łęckiej. Czy głęboka miłość galanteryjnego kupca do szlachcianki może zakończyć się szczęśliwie?",
                        IdLanguage = 1,
                        Publisher = new Publisher
                        {
                            FirstName = "Bolesław",
                            LastName = "Prus",
                            Biography = "Urodził się 20 sierpnia 1847 r. w Żabczu pod Hrubieszowem, zmarł 19 maja 1912 r. w Warszawie. Naprawdę nazywał się Aleksander Głowacki. Bardzo wcześnie zmarli mu rodzice. Najpierw wychowywał się u babki w Puławach, potem u ciotki w Lublinie.\r\n\r\nJako szesnastoletni chłopiec wraz ze swoim bratem Leonem brał udział w powstaniu styczniowym (1863). Był wówczas uczniem gimnazjum w Kielcach. Po upadku powstania przebywał przez kilka miesięcy w więzieniu w Lublinie. Ukończył liceum w Lublinie, potem studiował w warszawskiej Szkole Głównej. Przerwał studia i zaczął pracować jako dziennikarz. Wkrótce stał się wybitnym publicystą. Do różnych gazet pisał „Kroniki tygodniowe”, felietony i listy z podróży, jakie odbywał.\r\n\r\nTwórczość literacką rozpoczął od nowel i opowiadań. Najważniejsze z nich to: „Pałac i rudera”, „Przygody Stasia”, „Anielka”, „Michałko”, „Katarynka”, „Na wakacjach”, „Powracająca fala”, „Antek”, „Kamizelka”, „Grzechy dzieciństwa”, „Nawrócony”, „Milknące głosy”. Pierwszą powieścią B. Prusa była „Placówka” (1886). Inne znane powieści tego pisarza to: „Lalka”, „Emancypantki”, „Faraon”.\r\n\r\nPrus był realistą, więc rzeczywistość opisywał tak, jak ją widział. pisarz był bardzo wrażliwy na ludzką krzywdę. Pochowano go w Warszawie na Powązkach. Na pomniku wyryto napis: „Serce serc”. Pamiątki po B. Prusie zostały zgromadzone w Nałęczowie, gdyż tam pisarz często bywał."
                            DateOfBrith = new DateTime(1847,8,20)
                        }

                    };
                    new Book()
                    {
                        ISBN = "",
                        Title = "",
                        Description ="",
                        IdLanguage = 1,
                        Publisher = new Publisher
                        {
                            FirstName = "",
                            LastName = "",
                            Biography = "",
                            DateOfBrith = new DateTime(1847, 8, 20)
                        }

                    };
                    new Book()
                    {
                        ISBN = "",
                        Title = "",
                        Description = "",
                        IdLanguage = 1,
                        Publisher = new Publisher
                        {
                            FirstName = "",
                            LastName = "",
                            Biography = "",
                            DateOfBrith = new DateTime(1847, 8, 20)
                        }

                    };
                    new Book()
                    {
                        ISBN = "",
                        Title = "",
                        Description = "",
                        IdLanguage = 1,
                        Publisher = new Publisher
                        {
                            FirstName = "",
                            LastName = "",
                            Biography = "",
                            DateOfBrith = new DateTime(1847, 8, 20)
                        }

                    };
                    new Book()
                    {
                        ISBN = "",
                        Title = "",
                        Description = "",
                        IdLanguage = 1,
                        Publisher = new Publisher
                        {
                            FirstName = "",
                            LastName = "",
                            Biography = "",
                            DateOfBrith = new DateTime(1847, 8, 20)
                        }

                    };
                    new Book()
                    {
                        ISBN = "",
                        Title = "",
                        Description = "",
                        IdLanguage = 1,
                        Publisher = new Publisher
                        {
                            FirstName = "",
                            LastName = "",
                            Biography = "",
                            DateOfBrith = new DateTime(1847, 8, 20)
                        }

                    };

                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRoles(IApplicationBuilder applicationBuilder) 
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                //Checking for roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Librarian))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Librarian));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                //Adding admin account
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminEmail = "emamiler@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminEmail);

                if (adminUser == null) 
                {
                    var admin = new User()
                    {
                        UserName = "emamiler",
                        Email = adminEmail,
                        EmailConfirmed = true,
                        FirstName = "Ema",
                        LastName = "Miler",
                        Address = new Address()
                        {
                            PostalCode = "32-640",
                            Street = "Maja",
                            City = "1",
                        }
                    };
                    await userManager.CreateAsync(admin, "Test1234$");
                    await userManager.AddToRoleAsync(admin, UserRoles.Librarian);
                }

                //Adding user account
                string userEmail = "testuser@gmail.com";

                var user = await userManager.FindByEmailAsync(userEmail);
                
                if (user == null) 
                {
                    var newUser = new User()
                    {
                        UserName = "app-user",
                        Email = userEmail,
                        EmailConfirmed = true,
                        FirstName = "test",
                        LastName = "test",
                        Pesel = "00000000000",
                        Address = new Address()
                        {
                            PostalCode = "12-345",
                            Street = "Random",
                            City = "Random",
                        }  
                    };
                    await userManager.CreateAsync(newUser, "Test1234$");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}
