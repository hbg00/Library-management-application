using BookStore.Data;
using BookStore.Data.Enum;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Seed
{
    public class LibrarySeeder
    {
        public static void SeedData(IApplicationBuilder applicationBuilder) 
        {
            using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var context = serviceScope.ServiceProvider.GetService<LibraryDbContext>();

                context.Database.EnsureCreated();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            ISBN = "8373272267",
                            Title = "Lalka",
                            Description = "Wydanie z opracowaniem. Do Warszawy powraca Stanisław Wokulski. Jego plenipotent, Ignacy Rzecki, bacznie śledzi wszystkie plotki krążące wokół jego przyjaciela. Nie ma pojęcia o wielkim i jeszcze skrywanym uczuciu Wokulskiego do pięknej i próżnej Izabeli Łęckiej. Czy głęboka miłość galanteryjnego kupca do szlachcianki może zakończyć się szczęśliwie?",
                            Language = Languages.Polish,
                            NumberOfCopies = 3,
                            Publisher = new Publisher
                            {
                                FirstName = "Bolesław",
                                LastName = "Prus",
                                Biography = "Urodził się 20 sierpnia 1847 r. w Żabczu pod Hrubieszowem, zmarł 19 maja 1912 r. w Warszawie." +
                                " Naprawdę nazywał się Aleksander Głowacki. Bardzo wcześnie zmarli mu rodzice. Najpierw wychowywał" +
                                " się u babki w Puławach, potem u ciotki w Lublinie. Jako szesnastoletni chłopiec wraz ze swoim " +
                                "bratem Leonem brał udział w powstaniu styczniowym (1863). Był wówczas uczniem gimnazjum w Kielcach. Po " +
                                "upadku powstania przebywał przez kilka miesięcy w więzieniu w Lublinie. Ukończył liceum w Lublinie, " +
                                "potem studiował w warszawskiej Szkole Głównej. Przerwał studia i zaczął pracować jako dziennikarz." +
                                " Wkrótce stał się wybitnym publicystą. Do różnych gazet pisał „Kroniki tygodniowe”, felietony i listy " +
                                "z podróży, jakie odbywał. Twórczość literacką rozpoczął od nowel i opowiadań. Najważniejsze z nich to:" +
                                " „Pałac i rudera”, „Przygody Stasia”, „Anielka”, „Michałko”, „Katarynka”, „Na wakacjach”, „Powracająca fala”" +
                                ", „Antek”, „Kamizelka”, „Grzechy dzieciństwa”, „Nawrócony”, „Milknące głosy”. Pierwszą powieścią B. Prusa była " +
                                "„Placówka” (1886). Inne znane powieści tego pisarza to: „Lalka”, „Emancypantki”, „Faraon”. Prus był realistą, " +
                                "więc rzeczywistość opisywał tak, jak ją widział. pisarz był bardzo wrażliwy na ludzką krzywdę. Pochowano go w " +
                                "Warszawie na Powązkach. Na pomniku wyryto napis: „Serce serc”. Pamiątki po B. Prusie zostały zgromadzone w " +
                                "Nałęczowie, gdyż tam pisarz często bywał.",
                                DateOfBrith = new DateTime(1847, 8, 20)
                            }

                        },
                        new Book()
                        {
                            ISBN = "9787532231676",
                            Title = "Sherlock Holmes: The Collection",
                            Description = "Sherlock Holmes to fikcyjny detektyw z przełomu XIX i XX wieku, który po raz pierwszy ukazał się w publikacji" +
                            " w 1887 roku. Jest dziełem urodzonego w Szkocji pisarza i lekarza Sir Arthura Conana Doyle'a. Holmes, genialny londyński" +
                            " detektyw, słynie ze swojej sprawności intelektualnej i umiejętnego stosowania rozumowania dedukcyjnego" +
                            " (nieco błędnie – patrz rozumowanie indukcyjne) i wnikliwej obserwacji w celu rozwiązania trudnych przypadków." +
                            " Jest prawdopodobnie najsłynniejszym fikcyjnym detektywem",
                            Language = Languages.Polish,
                            NumberOfCopies = 2,
                            Publisher = new Publisher
                            {
                                FirstName = "Arthur",
                                LastName = "Conan Doyle",
                                Biography = "Sir Arthur Conan Doyle urodził się w 1859 r. w Edynburgu, a zmarł w 1930 r." +
                                " W ciągu tych lat nastąpiła różnorodna aktywność i twórczość, która uczyniła go postacią międzynarodową i zainspirowała" +
                                " Francuzów do nadania mu przydomka „dobrego giganta”. Był bratankiem artysty „Dickiego Doyle’a”, kształcił się" +
                                " w Stonyhurst, a później studiował medycynę na Uniwersytecie w Edynburgu, gdzie metody diagnozowania jednego z " +
                                "profesorów dały pomysł na metody dedukcji stosowane przez Sherlocka Holmesa.R ozpoczął pracę jako lekarz " +
                                "w Southsea i właśnie w oczekiwaniu na pacjentów zaczął pisać. Rosnący sukces jako autora pozwolił mu porzucić" +
                                " praktykę i zająć się innymi tematami. Był gorącym orędownikiem wielu spraw, począwszy od reformy prawa rozwodowego" +
                                " i tunelu pod kanałem La Manche po wydawanie marynarzom nadmuchiwanych kamizelek ratunkowych." +
                                " Prowadził także kampanię na rzecz udowodnienia niewinności poszczególnych osób, a jego praca nad" +
                                " sprawą Edjalji odegrała kluczową rolę w powołaniu Sądu Apelacyjnego Karnego. Podczas wojny burskiej" +
                                " był ochotniczym lekarzem, a później nawrócił się na spirytyzm. Jego największym osiągnięciem" +
                                " było oczywiście stworzenie Sherlocka Holmesa, który wkrótce osiągnął status międzynarodowy i nieustannie" +
                                " odrywał go od innych zajęć; pewnego razu Conan Doyle go zabił, ale protest publiczny był zmuszony przywrócić" +
                                " go do życia. A kreując doktora Watsona, towarzysza przygód Holmesa i kronikarza, Conan Doyle stworzył" +
                                " nie tylko doskonały film dla Holmesa, ale także jednego z najsłynniejszych narratorów fikcji" +
                                ". Pingwin wydał wszystkie książki o wielkim detektywie, Studium w szkarłacie, Znak czterech," +
                                " Przygody Sherlocka Holmesa, Wspomnienia Sherlocka Holmesa, Pies Baskerville'ów, Powrót Sherlocka Holmesa," +
                                " Dolina strachu, Jego Ostatni łuk, Księga spraw Sherlocka Holmesa, Niezebrane Sherlock Holmes i Pingwin" +
                                " Kompletny Sherlock Holmes.Zdjęcie: Walter Benington (aukcja RR) [domena publiczna w USA]," +
                                " za pośrednictwem Wikimedia Commons.",
                                DateOfBrith = new DateTime(1859, 5, 22)
                            }

                        },
                        new Book()
                        {
                            ISBN = "9788375068306",
                            Title = "Gra o tron (Polish Edition)",
                            Description = "W Zachodnich Krainach o ośmiu tysiącach lat zapisanej historii widmo wojen i katastrofy nieustannie wisi " +
                            "nad ludźmi. Zbliża się zima, lodowate wichry wieją z północy, gdzie schroniły się wyparte przez ludzi pradawne rasy i " +
                            "starzy bogowie. Zbuntowani władcy na szczęście pokonali szalonego Smoczego Króla, Aerysa Targaryena, zasiadającego na " +
                            "Żelaznym Tronie Zachodnich Krain, lecz obalony władca pozostawił po sobie potomstwo, równie szalone jak on sam. Tron objął " +
                            "Robert – najznamienitszy z buntowników. Minęły już lata pokoju i oto możnowładcy zaczynają grę o tron. Na podstawie " +
                            "\"Gry o tron\" ukazał się serial w telewizji HBO.",
                            Language = Languages.Polish,
                            NumberOfCopies = 1,
                            Publisher = new Publisher
                            {
                                FirstName = "George Raymond",
                                LastName = "Martin",
                                Biography = "George R.R. Martin, urodzony 20 września 1948 roku w Bayonne, New Jersey, to znany pisarz fantasy i science" +
                                " fiction. Początkowo publikował bez jednego \"R\" w nazwisku, ale z powodu mylenia go z inną osobą, dodał drugie imię." +
                                " Zdobył sławę dzięki bestsellerowej serii \"Pieśń lodu i ognia\", z której powstał popularny serial \"Gra o tron\"." +
                                " Seria składa się z kilku tomów, takich jak \"Gra o tron\", \"Starcie królów\", \"Nawałnica mieczy\", \"Uczta dla wron\"" +
                                " i \"Taniec ze smokami\". Martin otrzymał wiele nagród, w tym Nebula, Hugo, World Fantasy, Bram Stoker Award i Locus" +
                                " Award.\r\n\r\nSerial \"Ród Smoka\", premierę miał w sierpniu 2022 roku, opowiadając historię rodu Targaryenów 200 lat" +
                                " przed wydarzeniami z \"Gry o tron\". Martin jest także znany z żartów dotyczących tego, że uwielbia zabijać swoich" +
                                " bohaterów, a ostatni tom sagi może być pusty z powodu braku postaci. Poza pisarstwem, Martin współpracował przy" +
                                " projektach telewizyjnych, takich jak \"Strefa mroku\", \"Beauty and the Beast\" i \"Doorways",
                                DateOfBrith = new DateTime(1848, 9, 20)
                            }

                        },
                        new Book()
                        {
                            ISBN = "9788310137746",
                            Title = "Kubuś Puchatek",
                            Description = "\"Kubuś Puchatek\" to powieść dla dzieci licząca sobie już blisko sto lat. Napisał ją Alan Alexander Milne " +
                            "i to ON stworzył postać tytułowego bohatera. Kubuś Puchatek to niedźwiadek-zabawka, wraz z nim pojawiają się w historii " +
                            "takie postacie, jak Prosiaczek, Królik, Mama Kangurzyca i jej dziecko Maleństwo oraz osioł Kłapouchy. Akcja rozgrywa się" +
                            " w Stumilowym Lesie, w którym mieszka również Sowa Przemądrzała. Wszystkie zwierzątka to przyjaciele małego chłopca," +
                            " Krzysia. Pierwowzorem tej postaci był nie kto inny jak syn autora.Książka składa się z kilku odrębnych epizodów," +
                            " przedstawiających fantastyczne przygody, jakie przeżywają jej bohaterowie na obszarze Stumilowego Lasu. Każde ze zwierząt" +
                            " obdarzone jest innymi, bardzo ludzkimi cechami charakteru, co sprawia, że stają się one bliskie czytelnikowi." +
                            " Z jednej strony bawi czasem ich naiwność, z drugiej zaś zaskakuje ich filozoficzne podejście do życia." +
                            " Ponadto autor ukazuje siłę przyjaźni, przekonując, że różnice są cenne i mogą cementować relacje międzyludzkie." +
                            "Każdy z bohaterów ma swoje wady, ale też zalety, które warto dostrzec. Królik na przykład, choć lubi się rządzić," +
                            " jest niezwykle zaradnym życiowo, doskonałym organizatorem. Kłapouchy nieustannie narzeka, jednakże jest przy tym bardzo" +
                            " inteligentny, odpowiedzialny i uczynny. Prosiaczek jest co prawda podszyty tchórzem, jednakże stara się dotrzymywać" +
                            " przyrzeczeń i wszystkich traktuje z jednakową życzliwością.",
                            Language = Languages.Polish,
                            NumberOfCopies = 5,
                            Publisher = new Publisher
                            {
                                FirstName = "Alan Alexander",
                                LastName = "Milne",
                                Biography = "Alan Alexander Milne to żyjący w latach 1882-1956 brytyjski pisarz, autor książek dla dzieci." +
                                " Ma na swoim koncie zbiór wierszy dla dzieci oraz książki \"Kubuś Puchatek\" i \"Chatka Puchatka\"." +
                                " W swoim dorobku ma też powieść sensacyjną \"Tajemnica Czerwonego Domu\" oraz romans \"Dwoje ludzi\".",
                                DateOfBrith = new DateTime(1882, 1, 18)
                            }

                        },
                        new Book()
                        {
                            ISBN = "9788381860734",
                            Title = "Chłopcy z placu broni",
                            Description = "Tytułowy Plac Broni to ulubione miejsce zabaw grupy chłopców," +
                            " którym przewodzi Janosz Boka. Wśród członków grupy znajduje się również Ernest Nemeczek," +
                            " główny bohater książki, najniższy stopniem i najmniej poważany chłopiec, który często jest" +
                            " kozłem ofiarnym. Inna grupa chłopców, Czerwone Koszule, chce zająć Plac Broni" +
                            ", aby móc grać na nim w palanta. Przywódcą tej grupy jest Feri Acz. Walki pomiędzy chłopcami" +
                            " o prawo do Placu Broni, znaczącego dla nich więcej niż zwykły plac dziecięcych zabaw," +
                            " stanowią oś fabuły powieści. Główny bohater, choć z pozoru jest najsłabszym" +
                            " ogniwem swojej grupy, wykazuje się niezwykłą odwagą i zdobywa szacunek kolegów." +
                            " Podejmuje szereg heroicznych czynów, w których efekcie zachorował na zapalenie płuc." +
                            " Mimo choroby bierze udział w decydującej bitwie, ostatkiem sił przyczyniając się do" +
                            " zwycięstwa nad Czerwonymi Koszulami. Jaką cenę przyjdzie Ernestowi Nemeczkowi " +
                            "zapłacić za bohaterskie czyny? Czy Janosz Boka i jego kompani ocalą Plac Broni? Powieść w" +
                            " interesujący sposób przedstawia relacje panujące w grupie młodych chłopców oraz" +
                            " próby tworzenia w ich społeczności struktur wojskowych. Dla członków grupy ważne" +
                            " są takie wartości, jak: honor, odwaga, bohaterstwo i braterstwo, jednak wartością" +
                            " nadrzędną zdaje się być dla nich wierność sprawie. Ich postawę przyrównywać można do patriotyzmu.",
                            Language = Languages.Polish,
                            NumberOfCopies = 6,
                            Publisher = new Publisher
                            {
                                FirstName = "Ferenc",
                                LastName = "Molnar",
                                Biography = "Ferenc Molnar to żyjący w latach 1878-1952 węgierski pisarz, dramaturg i dziennikarz," +
                                " jeden z najbardziej znanych węgierskich twórców literatury pierwszej połowy XX wieku. Największą" +
                                " sławę przyniosła mu przetłumaczona na ponad 40 języków powieść \"Chłopcy z Placu Broni\". Prócz" +
                                " kilku innych powieści i wielu opowiadań, ma na swoim koncie także liczne dramaty, z" +
                                " których najbardziej znane to \"Diabeł\" i \"Liliom\".",
                                DateOfBrith = new DateTime(1878, 1, 12)
                            }

                        },
                        new Book()
                        {
                            ISBN = "9788328726291",
                            Title = "Wladca Pierscieni. Trylogia ",
                            Description = "Władca Pierścieni\" to jedna z najbardziej niezwykłych książek w całej współczesnej literaturze." +
                            " Ogromna, z epickim rozmachem napisana powieść wprowadza nas w wykreowany przez wyobraźnię autora świat -" +
                            " fantastyczny, lecz ukazany wszechstronnie i szczegółowo, równie pełny i bogaty jak świat realny." +
                            " Przykuwająca uwagę i wzruszająca, zabawna, choć momentami także przerażająca," +
                            " opowieść ta rzuca na czytelnika czar, od którego nawet po zakończeniu lektury trudno się uwolnić." +
                            " W ciągu pięćdziesięciu przeszło lat od pierwszego wydania \"Władcy Pierścieni\" miliony ludzi na" +
                            " całym świecie uległy temu urokow",
                            Language = Languages.Polish,
                            NumberOfCopies = 10,
                            Publisher = new Publisher
                            {
                                FirstName = "J.R.R",
                                LastName = "Tolkien",
                                Biography = "J.R.R. Tolkien (John Ronald Reuel Tolkien) to urodzony 3 stycznia 1892 roku w" +
                                " Bloemfontein, autor i filolog. Najbardziej znany ze swojej epickiej serii \"Władca Pierścieni\"," +
                                " składającej się z \"Drużyny Pierścienia\", \"Dwóch Wież\" i \"Powrotu Króla\". Tolkien był profesorem" +
                                " filologii angielskiej i zafascynowany literaturą staroangielską i staronordycką. Jego wpływ na gatunek" +
                                " fantasy jest ogromny, a \"Władca Pierścieni\" stał się klasyką literatury. Zmarł 2 września 1973 roku" +
                                " w Bournemouth, Anglia.",
                                DateOfBrith = new DateTime(1892, 1, 3)
                            }

                        }
                     });
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
                        Pesel = "00000000001",
                        Address = new Address()
                        {
                            PostalCode = "32-640",
                            Street = "Maja",
                            City = "Zator",
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
