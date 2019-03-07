using GameStore.DAL.Context;
using GameStore.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GameStore.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GameStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GameStoreContext context)
        {
            if (!context.Genres.Any())
            {
                context.Genres.AddRange(_genres);
                context.PlatformTypes.AddRange(_platformTypes);
                context.Publishers.AddRange(_publishers);

                var games = CreateGames();
                games.AddRange(CreateGamesTest());
                games.ForEach(x => x.Comments = new List<Comment>());
                context.Games.AddRange(games);

                context.SaveChanges();
            }
        }

        private List<Genre> _genres = new List<Genre>
        {
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Indie",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Action",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Adventure",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Casual",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Strategy",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Simulation",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "RPG",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Free to play",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Sports",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Racing",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Multiplayer",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Puzzle",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Anime",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "VR",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Horror",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Open-World",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "FPS",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "TPS",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Survival",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Arcade",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Animation & Modeling",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Education",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Memes",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "RTS",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Tactical",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Action RPG",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Fighting",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "MMORPG",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Romance",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cyberpunk",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Cartoony",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Driving",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Crime",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Detective",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Psychological",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "RealTime",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "3D",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "2D",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Clicker",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Pirates",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Logical",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Ninja",
                Games = new List<Game>()
            },
            new Genre
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Runner",
                Games = new List<Game>()
            }
        };

        private List<PlatformType> _platformTypes = new List<PlatformType>
        {
            new PlatformType
            {
                Id = Guid.NewGuid().ToString(),
                Type = "Mobile",
                Games = new List<Game>()
            },
            new PlatformType
            {
                Id = Guid.NewGuid().ToString(),
                Type = "Descktop",
                Games = new List<Game>()
            },
            new PlatformType
            {
                Id = Guid.NewGuid().ToString(),
                Type = "Browser",
                Games = new List<Game>()
            },
            new PlatformType
            {
                Id = Guid.NewGuid().ToString(),
                Type = "Console",
                Games = new List<Game>()
            },
            new PlatformType
            {
                Id = Guid.NewGuid().ToString(),
                Type = "Play Station",
                Games = new List<Game>()
            },
            new PlatformType
            {
                Id = Guid.NewGuid().ToString(),
                Type = "XBOX",
                Games = new List<Game>()
            },
        };

        private List<Publisher> _publishers = new List<Publisher>
        {
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "1-Up Studio",
                Description =
                    " Japanese Nintendo-funded and owned video game developer founded on June 30, 2000 in Tokyo, Japan. " +
                    "On February 1, 2013, the company announced that due to their recent co-development efforts with Nintendo, " +
                    "that they were undergoing a change in internal structure, which included changing the name of their company to 1-Up Studio.",
                Games = new List<Game>(),
                HomePage = "https://1-up-studio.jp/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "4mm Games",
                Description =
                    "New York–based video game development company founded in 2008 by two founders of Rockstar Games, " +
                    "Jamie King and Gary Foreman along with former Image Metrics exec Nicholas Perrett, and Def Jam Enterprises, " +
                    "Warner Music Group and NBC exec Paul Coyne. " +
                    "4mm was founded with the intent of bringing the experience of Constantly Connected Gaming to the next generation of gamers. " +
                    "OBE and former NCSoft CEO Geoff Heath was a member of the advisory board and offers strategic input",
                Games = new List<Game>(),
                HomePage = "https://4mmgames.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "ABA Games",
                Description = "Japanese video game developer, composed solely of game designer Kenta Cho. " +
                              "ABA Games' works, available as open source freeware, are predominantly shoot 'em up games often inspired by classic games in the genre. " +
                              "Its games feature stylised retro graphics, innovative gameplay features and modes and feature random rather than scripted events. " +
                              "These creations have been acclaimed as some of the best and well-known independent games available, " +
                              "though some commentators, including Cho himself, feel they are too simple for commercial release.",
                Games = new List<Game>(),
                HomePage = "http://www.asahi-net.or.jp/~cs8k-cyu/index.html"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Activision",
                Description =
                    "American video game publisher based in Santa Monica. The company was founded in October 1979 by former Atari employees as the first independent video game developer. " +
                    "As of January 2017, Activision is one of the largest third-party video game publishers in the world and was the top publisher for 2016 in the United States." +
                    " Its parent company is Activision Blizzard, formed from the merger of Activision and Vivendi Games on July 9, 2008, an entity which became a completely independent company on July 25, " +
                    "2013 when Activision Blizzard purchased the remaining shares from then majority owner Vivendi. Its CEO was Eric Hirshberg until March 2018.",
                Games = new List<Game>(),
                HomePage = "https://www.activision.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Agate Studio",
                Description =
                    "Indonesian video game development company based in Bandung, West Java, Indonesia. It was founded on April 1, 2009. " +
                    "It has worked with publishers such as Square Enix and Electronic Arts. " +
                    "The company produces serious games, providing gamified solutions to companies either via gamification of systems, advertisement games or training games meant to simulate the work environment. " +
                    "Agate had worked for Microsoft, Samsung, and Coca-Cola. In October 2011, Agate together with Kummara and AnimartDigi held Indonesia Bermain in Bandung. 7000 people attended.",
                Games = new List<Game>(),
                HomePage = "https://agate.id/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Bandai Namco Entertainment",
                Description =
                    "Japanese video game development company and publisher. The company also releases videos, music, and other entertainment products related to its intellectual properties (IP). " +
                    "The company is headquartered in Minato-ku, Tokyo. Bandai Namco Entertainment is a wholly owned subsidiary of Bandai Namco Holdings (BNHD) and specializes in management and sales of video games " +
                    "and other related entertainment products, while its Bandai Namco Studios subsidiaries specialize in the development of these products. " +
                    "It is the core company of Bandai Namco Group's Content Strategic Business Unit (Content SBU). " +
                    "Bandai Namco Entertainment is the result of a merger in March 2006 between the video game operations of Namco and Bandai. " +
                    "Previously known as Namco Bandai Games, the company was renamed as Bandai Namco Games in January 2014. In April 2015, " +
                    "Bandai Namco Holdings changed its gaming name from Bandai Namco Games to Bandai Namco Entertainment.",
                Games = new List<Game>(),
                HomePage = "https://www.bandainamcoent.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Capcom",
                Description =
                    "Japanese video game developer and publisher known for creating numerous multi-million selling game franchises, including Street Fighter, Mega Man, Resident Evil, " +
                    "Devil May Cry, Monster Hunter, Sengoku Basara, Ace Attorney, Onimusha, Breath of Fire, Darkstalkers, as well as games based on the Disney animated properties. Established in 1979," +
                    " it has become an international enterprise with subsidiaries in North America, Europe, and Japan.",
                Games = new List<Game>(),
                HomePage = "http://www.capcom-europe.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Electronic Arts",
                Description =
                    "American video game company headquartered in Redwood City, California. Founded and incorporated on May 28, 1982 by Trip Hawkins, " +
                    "the company was a pioneer of the early home computer games industry and was notable for promoting the designers and programmers responsible for its games. " +
                    "As of March 2018, Electronic Arts is the second-largest gaming company in the Americas and Europe by revenue and market capitalization after Activision Blizzard and ahead of Take-Two Interactive and Ubisoft.",
                Games = new List<Game>(),
                HomePage = "http://www.capcom-europe.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Eden Games",
                Description =
                    "In May 2002, the company was sold to the Infogrames Group. It is most well known for the V-Rally series of games, as well as the 2006 release Test Drive Unlimited. " +
                    "They have most recently developed Test Drive Unlimited 2, which was released in February 2011 for the PlayStation 3, Xbox 360 and PC. T" +
                    "he company was a wholly owned subsidiary of Atari, SA. In 2013 Atari decided to close it. However, " +
                    "the company re-opened in 2014 under the impulsion of former employees and with the financing of ID Invest and Monster Capital. As of 2013, " +
                    "Eden Games have started up as a small independent games company, independent of Atari and releases its new game, GT Spirit, on Apple TV in December, 2015.",
                Games = new List<Game>(),
                HomePage = "http://www.edengames.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Egosoft",
                Description =
                    "German video game developer based in W?rselen, Germany. The company was founded by Bernd Lehahn in 1988. " +
                    "Egosoft is best known for its X series of video games, a series of space simulator games noted for combining open-ended gameplay, " +
                    "dynamic market-driven economy and compelling storyline. The series began in 1999 with X: Beyond the Frontier. " +
                    "Since then, the series has expanded with three sequels and three standalone expansions; the latest being X4: Foundations, released November 30, 2018.",
                Games = new List<Game>(),
                HomePage = "http://www.egosoft.com/games/x4/info_ru.php"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Epic Games",
                Description =
                    "Epic Games, Inc. (formerly Potomac Computer Systems and later Epic MegaGames, Inc.) is an American video game and software development company based in Cary, North Carolina. " +
                    "The company was founded by Tim Sweeney as Potomac Computer Systems in 1991, originally located in his parents' house in Potomac, Maryland. " +
                    "Following his first commercial video game release, ZZT (1991), the company became Epic MegaGames in early 1992, and brought on Mark Rein, who is the company's vice president to date. " +
                    "Moving their headquarters to Cary in 1999, the studio's name was simplified to Epic Games. " +
                    "Epic Games develops the Unreal Engine, a commercially available game engine which also powers their internally developed video games, such as Fortnite and the Unreal, " +
                    "Gears of War and Infinity Blade series. In 2014, Unreal Engine was named the \"most successful videogame engine\" by Guinness World Records. " +
                    "Epic Games owns video game developer Chair Entertainment and cloud-based software developer Cloudgine, and operates eponymous sub-studios in Seattle, England, Berlin, Yokohama and Seoul. " +
                    "Key personnel at Epic Games include chief executive officer Tim Sweeney, lead programmer Steve Polge and art director Chris Perna." +
                    " Tencent acquired a 40% stake in the company in 2012, after Epic Games realized that the video game industry was heavily developing towards the games as a service model.",
                Games = new List<Game>(),
                HomePage = "https://www.epicgames.com/store/ru/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Gameloft",
                Description =
                    "Gameloft SE is a French video game publisher based in Paris, founded in December 1999 by Ubisoft co-founder Michel Guillemot. " +
                    "The company operates 21 development studios worldwide, and publishes games with a special focus on the mobile games market. " +
                    "Formerly a public company traded at the Paris Bourse, Gameloft was fully acquired by French media conglomerate Vivendi in 2016.",
                Games = new List<Game>(),
                HomePage = "http://www.gameloft.com/en/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Gaijin Entertainment",
                Description =
                    "Gaijin Entertainment is a Russian video game developer and publisher established in 2002. It is the largest independent video games developer in Russia, known for War Thunder and Star Conflict.",
                Games = new List<Game>(),
                HomePage = "https://gaijinent.com/en"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Gearbox Software",
                Description =
                    "American video game development company based in Frisco, Texas. It was established in 1999 by developers from companies such as 3D Realms and Bethesda Softworks, with one of the founders, Randy Pitchford, as CEO. " +
                    "The company initially created expansions for the Valve Corporation game Half-Life, then ported that game and others to console platforms. " +
                    "In 2005 Gearbox launched its first independent set of games, Brothers in Arms, on console and mobile devices. " +
                    "It became their flagship franchise and spun off a comic book series, television documentary, books, and action figures. " +
                    "Their second original game series Borderlands was released in 2009, and by 2015 had sold over 26 million copies. The company also owns the intellectual property of Duke Nukem and Homeworld.",
                Games = new List<Game>(),
                HomePage = "http://www.gearboxsoftware.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Ideaworks Game Studio",
                Description = "Ideaworks Game Studio (IGS) was a video game developer based in London, UK. " +
                              "Founded in 1998, originally trading as Ideaworks3D the studio has a heritage of developing high-end native cross platform technology and games for the iPhone and Smartphone markets. " +
                              "The studio has created award-winning games, including original and franchise-based games for publishers.",
                Games = new List<Game>(),
                HomePage = "https://www.hugedomains.com/domain_profile.cfm?d=ideaworksgamestudio&e=com"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Kakao Games",
                Description =
                    "Kakao Games Corp. (Hangul: ??????) is a South Korean video game company and a subsidiary of Kakao, it specializes in developing and publishing game on PC, mobile, and VR platforms." +
                    " Each is represented by Namgoong Hoon and Cho Gye-hyun. Originally known as Daum Games before the acquisition of Daum, " +
                    "Kakao Games has grown from game distribution in Korea only to distributing its games outside the local including North America, Europe, " +
                    "and other parts of Asia thanks in part to its social aspect with KakaoTalk. Kakao uses the game portal of Daum and social network of KakaoTalk to connect players within their games.",
                Games = new List<Game>(),
                HomePage = "https://www.kakaogames.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Ketchapp",
                Description =
                    "Ketchapp SARL is a French video game publisher based in Paris, specialising in the mobile games market. Founded in March 2014 by brothers Antoine and Michel Morcos, " +
                    "the company first came into the public eye in 2014, through its port of the open-source game 2048. " +
                    "Many of Ketchapp's games are unlicensed variations of popular casual games by other developers. Ketchapp was acquired by Ubisoft in September 2016.",
                Games = new List<Game>(),
                HomePage = "https://en.wikipedia.org/wiki/Ketchapp"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Konami",
                Description =
                    "Japanese entertainment and gaming conglomerate. It operates as a product distributor (which produces and distributes trading cards, anime, tokusatsu, slot machines and arcade cabinets), " +
                    "video game developer and publisher company. It also operates health and physical fitness clubs across Japan. " +
                    "Konami is best known for their video games, including Metal Gear, Silent Hill, Castlevania, Contra, Frogger, Gradius, Yu-Gi-Oh!, Suikoden and Pro Evolution Soccer. Additionally, " +
                    "Konami also owns Bemani, known for Dance Dance Revolution and Beatmania, as well as the assets of former game developer Hudson Soft, known for Bomberman, Adventure Island, Bonk, " +
                    "Bloody Roar and Star Soldier. Konami is the twentieth-largest game company in the world by revenue. " +
                    "The company originated in 1969 as a jukebox rental and repair business in Toyonaka, Osaka, Japan, by Kagemasa K?zuki, who remains the company's chairman. The name \"Konami\" " +
                    " is a portmanteau of the names Kagemasa Kozuki, Yoshinobu Nakama, and Tatsuo Miyasako. Konami is currently headquartered in Tokyo. In the United States, " +
                    "Konami manages its video game business from offices in El Segundo, California and its casino gaming business from offices in Paradise, Nevada. " +
                    "Its Australian gaming operations are located in Sydney. As of March 2016, it owns 21 consolidated subsidiaries around the world.",
                Games = new List<Game>(),
                HomePage = "https://www.konami.com/en/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Matrix Software",
                Description =
                    "Matrix Software (?????????? Kabushiki-gaisha Matorikkusu) is a Japanese video game development company located in Tokyo. " +
                    "Founded in July 1994 by former members of Climax Entertainment and Telenet Japan, the company has since created games for a number of systems beginning with their action-adventure game title Alundra in April 1997. " +
                    "Matrix has teamed with other developers such as Square Enix and Chunsoft to produce games for existing franchises such as Final Fantasy and Dragon Quest, as well as other anime and manga properties. " +
                    "In addition to game console development, Matrix Software has also made games for various Japanese mobile phone brands since 2001.",
                Games = new List<Game>(),
                HomePage = "http://www.matrixsoft.co.jp/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Nintendo",
                Description =
                    "Nintendo Co., Ltd.[a] is a Japanese multinational consumer electronics and video game company headquartered in Kyoto." +
                    " Nintendo is one of the world's largest video game companies by market capitalization, creating some of the best-known and top-selling video game franchises, such as Mario, The Legend of Zelda, and Pok?mon. " +
                    "Founded on 23 September 1889 by Fusajiro Yamauchi, it originally produced handmade hanafuda playing cards. By 1963, the company had tried several small niche businesses, such as cab services and love hotels. " +
                    "Abandoning previous ventures in favor of toys in the 1960s, Nintendo developed into a video game company in the 1970s, ultimately becoming one of the most influential in the industry and " +
                    "Japan's third most-valuable company with a market value of over $85 billion.",
                Games = new List<Game>(),
                HomePage = "https://www.nintendo.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Playground Games",
                Description =
                    "Playground Games Limited is a British video game developer located in Royal Leamington Spa, England, United Kingdom and a subsidiary of American technology company Microsoft through its video game division. " +
                    "It is known for developing the Forza Horizon series, which is part of the larger Forza franchise.",
                Games = new List<Game>(),
                HomePage = "https://www.playground-games.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Raven Software",
                Description =
                    "PRaven Software (or Raven Entertainment Software, Inc.) is an American video game developer based in Wisconsin and founded in 1990. In 1997, Raven made an exclusive publishing deal with Activision and was subsequently acquired by them. " +
                    "]After the acquisition, many of the studio's original developers, largely responsible for creating the Heretic and Hexen: Beyond Heretic games, left to form Human Head Studios.",
                Games = new List<Game>(),
                HomePage = "https://www.playground-games.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Rebellion Developments",
                Description =
                    "Rebellion Developments Limited is a British video game developer based in Oxford, England. Founded by Jason and Chris Kingsley in December 1992, the company is best known for its Sniper Elite series and multiple games in the Alien vs. Predator series. " +
                    "Rebellion has published comic books since 2000, when it purchased 2000 AD, the publisher of characters such as Judge Dredd and Rogue Trooper.",
                Games = new List<Game>(),
                HomePage = "https://rebellion.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Rockstar Games",
                Description =
                    "Rockstar Games, Inc. is an American video game publisher based in New York City. The company was established in December 1998 as a subsidiary of Take-Two Interactive, and as successor to BMG Interactive, " +
                    "a dormant video game publisher Take-Two had previously acquired the assets of. Founding members of the company were Sam and Dan Houser, Terry Donovan and Jamie King, who worked for Take-Two at the time, " +
                    "and of which the Houser brothers were previously executives at BMG Interactive. Co-founders Sam and Dan Houser head the studio as president and vice-president for creative, respectively." +
                    " Jennifer Kolbe, who started at the front desk of Take-Two, acts as Rockstar Games' head of publishing and oversees all development studios.[2][3] Simon Ramsey is the company's head of PR and communications. " +
                    "Since 1999, several companies acquired by or established under Take-Two became part of Rockstar Games, such as Rockstar Canada (later renamed Rockstar Toronto) becoming the first one in 1999, " +
                    "and Rockstar India the most recent in 2016. All companies organized under Rockstar Games bear the \"Rockstar\" name and logo; in that context, Rockstar Games is sometimes also referred to as Rockstar New York or " +
                    "Rockstar NYC.\r\n\r\nRockstar Games predominantly publishes games in the action-adventure genre, while racing games also saw success for the company. One of such action-adventure game franchises is Grand Theft Auto, " +
                    "which Rockstar Games took over from BMG Interactive, which published the series' original 1997 entry. The most recent game in the series, Grand Theft Auto V, " +
                    "has shipped over 95 million copies since its release in September 2013, making it one of the best-selling video games of all time. Other popular franchises published by Rockstar Games are Red Dead, " +
                    "Midnight Club, Max Payne and Manhunt.",
                Games = new List<Game>(),
                HomePage = "https://www.rockstargames.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Sega",
                Description =
                    "Sega Games Co., Ltd. (/?s???/; stylized as SEGA)[a] is a Japanese multinational video game developer and publisher headquartered in Tokyo, Japan. The company, previously known as Sega Enterprises Ltd. " +
                    "and Sega Corporation, is a subsidiary of Sega Holdings Co., Ltd., which is part of Sega Sammy Holdings. Its international divisions, Sega of America and Sega of Europe, are headquartered in Irvine, " +
                    "California and London respectively. The company's formation can be traced back to the founding of Nihon Goraku Bussan in 1960, which became known as Sega Enterprises, Ltd. following acquisition of " +
                    "Rosen Enterprises. Sega began developing coin-operated games with Periscope. In 1969, Sega was sold to Gulf and Western Industries. Following a downturn in the arcade business in the early 1980s, " +
                    "Sega began to develop video game consoles, starting with the SG-1000 and Master System, but struggled against competitors such as the Nintendo Entertainment System. Sega released its next console, " +
                    "the Sega Genesis (known as the Mega Drive outside North America), in 1988. Although it was a distant third in Japan, the Genesis found major success after the release of Sonic the Hedgehog in 1991 " +
                    "and briefly outsold its main competitor, the Super Nintendo Entertainment System, in the U.S. However, in the second half of the decade, Sega suffered commercial failures such as the 32X, Sega Saturn, " +
                    "and Dreamcast consoles. In 2001, Sega stopped manufacturing consoles to become a third-party developer and publisher, and was acquired by Sammy Corporation in 2004. In the years since the acquisition, " +
                    "Sega has been more profitable, but has been criticized for prioritizing quantity of game releases over quality. Sega produces multi-million-selling game franchises including Sonic the Hedgehog, Total War, " +
                    "and Yakuza, and is the world's most prolific arcade game producer. It also operates amusement arcades and produces other entertainment products, including Sega Toys. Sega is a subsidiary of Sega Sammy Holdings, " +
                    "a corporate conglomerate with over 60 individual subsidiaries.",
                Games = new List<Game>(),
                HomePage = "https://www.sega.com/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Sony Interactive Entertainment",
                Description =
                    "Sony Interactive Entertainment Inc. (SIE) is a multinational video game and digital entertainment company that is a wholly owned subsidiary of Japanese conglomerate Sony. " +
                    "The company was founded in Tokyo, Japan, and established on November 16, 1993, as Sony Computer Entertainment (SCE), to handle Sony's venture into video game development through its PlayStation brand. " +
                    "Since the successful launch of the original PlayStation console in 1994, the company has been developing the PlayStation lineup of home video game consoles and accessories. " +
                    "Expanding into North America and other countries, the company quickly became Sony's main resource for research and development in video games and interactive entertainment. " +
                    "In April 2016, SCE and Sony Network Entertainment International was restructured and reorganized into Sony Interactive Entertainment, carrying over the operations and primary objectives from both companies. " +
                    "The same year, SIE moved its headquarters from Tokyo to San Mateo, California. Sony Interactive Entertainment handles the research and development, production, and sales of both hardware and software for the " +
                    "PlayStation video game systems. SIE is also a developer and publisher of video game titles, and operates several subsidiaries in Sony's largest markets: North America, Europe and Asia. By August 2018, the company had " +
                    "sold more than 525 million PlayStation consoles worldwide",
                Games = new List<Game>(),
                HomePage = "https://www.sie.com/en/index.html"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Ubisoft",
                Description =
                    "Ubisoft Entertainment SA (/?ju?bis?ft/;[4] French: [ybis?ft]; formerly Ubi Soft Entertainment SA) is a French video game company headquartered in Montreuil, France, with several development studios across the world. " +
                    "It is known for publishing games for several acclaimed video game franchises including Assassin's Creed, Far Cry, Just Dance, Prince of Persia, Rayman, Raving Rabbids, and Tom Clancy's. " +
                    "As of March 2018, it is the fourth largest publicly-traded game company in the Americas and Europe after Activision Blizzard, Electronic Arts, and Take-Two Interactive in terms of revenue and market capitalisation",
                Games = new List<Game>(),
                HomePage = "https://www.ubisoft.com/en-us/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Valve Corporation",
                Description =
                    "Valve Corporation is an American video game developer, publisher and digital distribution company headquartered in Bellevue, Washington. " +
                    "It is the developer of the software distribution platform Steam and the Half-Life, Counter-Strike, Portal, Day of Defeat, Team Fortress, Left 4 Dead, and Dota 2 games. " +
                    "Valve was founded in 1996 by former Microsoft employees Gabe Newell and Mike Harrington. Their debut product, the PC first-person shooter Half-Life, was released in 1998 to critical acclaim and commercial success, " +
                    "after which Harrington left the company. In 2003, Valve launched Steam, which accounted for around half of digital PC game sales by 2011. By 2012, Valve employed around 250 people and was reportedly worth over US$3 " +
                    "billion, making it the most profitable company per employee in the United States. In 2015, Valve entered the game hardware market with the Steam Machine, a line of third-party built gaming PCs running Valve's " +
                    "SteamOS operating system.",
                Games = new List<Game>(),
                HomePage = "https://www.valvesoftware.com/ru/"
            },
            new Publisher
            {
                Id = Guid.NewGuid().ToString(),
                CompanyName = "Wargaming (company)",
                Description =
                    "Wargaming Group Limited (also known as Wargaming.net) is a Belarusian video game company headquartered in Nicosia, Cyprus. The group operates across 16 offices and development studios, " +
                    "the largest of which is located in Minsk, where the company originated from. Initially focused on turn-based strategy and real-time strategy games, " +
                    "Wargaming switched to developing free-to-play online action games in 2009, including the military-themed team-based game World of Tanks.",
                Games = new List<Game>(),
                HomePage = "https://wargaming.com/en/"
            },
        };

        private List<Game> CreateGamesTest()
        {
            var games = new List<Game>();

            for (var i = 1; i < 201; i++)
            {
                var game = new Game()
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = i + 200,
                    UnitsInStock = (short)i,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[1],
                        _genres[2],
                        _genres[5],
                        _genres[6],
                        _genres[7]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[3]
                    },
                    Publisher = _publishers[4],
                    Name = $"Test Game {i}",
                    Key = $"Test Game {i}",
                    Description =
                        "Test Game that is a unique combination of first person shooter, survival horror, " +
                        "tower defense, and role-playing games. " +
                        "Play the definitive zombie survival sandbox RPG that came first. Navezgane awaits!",
                    ReleaseDate = new DateTime(2019, 1, 1, 0, 0, 0).AddDays(-i),
                    Price = 110 + i
                };

                games.Add(game);
            }

            return games;
        }

        private List<Game> CreateGames()
        {
            var games = new List<Game>
            {
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 56972,
                    UnitsInStock = 100,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[1],
                        _genres[2],
                        _genres[5],
                        _genres[6],
                        _genres[7]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[3]
                    },
                    Publisher = _publishers[3],
                    Name = "7 Days to Die",
                    Key = "7 Days to Die",
                    Description =
                        "7 Days to Die is an open-world game that is a unique combination of first person shooter, survival horror, " +
                        "tower defense, and role-playing games. " +
                        "Play the definitive zombie survival sandbox RPG that came first. Navezgane awaits!",
                    ReleaseDate = new DateTime(2013, 12, 13, 0, 0, 0),
                    Price = 479
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 2000,
                    UnitsInStock = 33,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[13],
                    Name = "The Last Hope: Atomic Bomb - Crypto War",
                    Key = "The Last Hope Atomic Bomb Crypto War",
                    Description =
                        "The ongoing struggle between mobsters, terrorists and President John Trump has now shifted its focus towards cryptocurrency. " +
                        "Your mission, while not as easy as it may appear, is to locate the cryptocurrency farm " +
                        "and put an end to this senseless war once and for all!",
                    ReleaseDate = new DateTime(2018, 6, 12, 0, 0, 0),
                    Price = 23
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 33,
                    UnitsInStock = 143,
                    Genres = new List<Genre>
                    {
                        _genres[5],
                        _genres[9]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1],
                        _platformTypes[5]
                    },
                    Publisher = _publishers[4],
                    Name = "Cricket Captain 2018",
                    Key = "Cricket Captain 2018",
                    Description = "Cricket Captain returns for 2018 with an updated match engine, " +
                                  "extensive additions to the records and statistics (including stats of every historical " +
                                  "international player), Ireland & Afghanistan as playable test nations, improved internet game, " +
                                  "improved coaching, and much more.",
                    ReleaseDate = new DateTime(2018, 7, 13, 0, 0, 0),
                    Price = 329
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 12,
                    UnitsInStock = 1040,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2],
                        _genres[3],
                        _genres[5],
                        _genres[7],
                        _genres[8]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[5],
                    Name = "Horse Riding Deluxe",
                    Key = "Horse Riding Deluxe",
                    Description = "Explore a beautiful open world, full of horses and other animals. " +
                                  "Or just enjoy the beautiful view while riding. In the world of Horse Riding Deluxe you " +
                                  "and your horses have no limits. But train and care for your horses well because " +
                                  "the various races will demand everything from you!",
                    ReleaseDate = new DateTime(2018, 01, 29, 0, 0, 0),
                    Price = 329
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 173,
                    UnitsInStock = 95,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[1]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[10],
                    Name = "Lucius III",
                    Key = "Lucius III",
                    Description = "Lucius is back. In a whole new narrative experience, " +
                                  "he now returns to his old neighborhood of Winter Hill. " +
                                  "The path will be a difficult one, with trials and tribulations waiting ahead. " +
                                  "Has he made the right choices? Is it finally time to end it all?",
                    ReleaseDate = new DateTime(2018, 12, 13, 0, 0, 0),
                    Price = 279
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 126816,
                    UnitsInStock = 67,
                    Genres = new List<Genre>
                    {
                        _genres[1]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[13],
                    Name = "Dead by Daylight",
                    Key = "Dead by Daylight",
                    Description =
                        "Dead by Daylight is a multiplayer (4vs1) horror game where one player takes on the role " +
                        "of the savage Killer, and the other four players play as Survivors, trying to escape the " +
                        "Killer and avoid being caught and killed.",
                    ReleaseDate = new DateTime(2016, 6, 14, 0, 0, 0),
                    Price = 379
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 14796,
                    UnitsInStock = 35,
                    Genres = new List<Genre>
                    {
                        _genres[1]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[2],
                    Name = "MORTAL KOMBAT XL",
                    Key = "MORTAL KOMBAT XL",
                    Description = "Experience the Next Generation of the #1 Fighting Franchise. " +
                                  "Mortal Kombat X combines unparalleled, cinematic presentation with all new gameplay.",
                    ReleaseDate = new DateTime(2015, 4, 14, 0, 0, 0),
                    Price = 379
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 125506,
                    UnitsInStock = 55,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[6]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[9],
                    Name = "Euro Truck Simulator 2",
                    Key = "Euro Truck Simulator 2",
                    Description = "Travel across Europe as king of the road, a trucker who delivers " +
                                  "important cargo across impressive distances! With dozens of cities " +
                                  "to explore from the UK, Belgium, Germany, Italy, the Netherlands, Poland, " +
                                  "and many more, your endurance, skill and speed will all be pushed to their limits.",
                    ReleaseDate = new DateTime(2012, 10, 12, 0, 0, 0),
                    Price = 1084
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 8796,
                    UnitsInStock = 123,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[8],
                    Name = "Shadow of the Tomb Raider",
                    Key = "Shadow of the Tomb Raider",
                    Description = "As Lara Croft races to save the world from a Maya apocalypse, " +
                                  "she must become the Tomb Raider she is destined to be.",
                    ReleaseDate = new DateTime(2018, 9, 17, 0, 0, 0),
                    Price = 1116
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 44341,
                    UnitsInStock = 1005,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[2],
                    Name = "Far Cry 3",
                    Key = "Far Cry 3",
                    Description = "Discover the dark secrets of a lawless island ruled by violence and" +
                                  " take the fight to the enemy as you try to escape. You’ll need more than luck to escape alive!",
                    ReleaseDate = new DateTime(2012, 11, 29, 0, 0, 0),
                    Price = 635
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 26592,
                    UnitsInStock = 9,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[10],
                    Name = "Far Cry® 5",
                    Key = "Far Cry 5",
                    Description =
                        "Welcome to Hope County, Montana, home to a fanatical doomsday cult known as Eden’s Gate. " +
                        "Stand up to cult leader Joseph Seed & his siblings, the Heralds, to spark the fires of resistance & liberate the besieged community.",
                    ReleaseDate = new DateTime(2018, 3, 27, 0, 0, 0),
                    Price = 1480
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 35319,
                    UnitsInStock = 13,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[3],
                    Name = "Rise of the Tomb Raider™",
                    Key = "Rise of the Tomb Raider",
                    Description =
                        "Rise of the Tomb Raider: 20 Year Celebration includes the base game and Season Pass featuring all-new content. " +
                        "Explore Croft Manor in the new “Blood Ties” story, then defend it against a zombie invasion in “Lara’s Nightmare”.",
                    ReleaseDate = new DateTime(2016, 2, 9, 0, 0, 0),
                    Price = 14180
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 9296,
                    UnitsInStock = 100,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[3],
                    Name = "Far Cry® Primal",
                    Key = "Far Cry Primal",
                    Description =
                        "The award-winning Far Cry franchise returns with its innovative open world gameplay, bringing together massive beasts, " +
                        "breathtaking environments, and unpredictable savage encounters. Welcome to the Stone Age, a time of danger and adventure.",
                    ReleaseDate = new DateTime(2016, 3, 1, 0, 0, 0),
                    Price = 2730
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 34480,
                    UnitsInStock = 70,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[14],
                    Name = "Just Cause™ 3",
                    Key = "Just Cause 3",
                    Description =
                        "With over 1000 km of complete freedom from sky to seabed, Rico Rodriguez returns to unleash chaos in the most creative and explosive ways imaginable.",
                    ReleaseDate = new DateTime(2015, 12, 1, 0, 0, 0),
                    Price = 14116
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 406,
                    UnitsInStock = 11,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[3],
                        _genres[9]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[12],
                    Name = "Megaquarium",
                    Key = "Megaquarium",
                    Description =
                        "A theme park management tycoon game with an aquatic twist. Design your displays, look after your fish, manage your staff and keep your guests happy! " +
                        "It's all in a day's work as the curator of your very own Megaquarium.",
                    ReleaseDate = new DateTime(2018, 9, 13, 0, 0, 0),
                    Price = 329
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 3563,
                    UnitsInStock = 60,
                    Genres = new List<Genre>
                    {
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[7],
                    Name = "Life is Strange 2",
                    Key = "Life is Strange 2",
                    Description =
                        "Two brothers Sean and Daniel Diaz, are forced to run away from home after a tragic incident in Seattle. " +
                        "In fear of the police, Sean & Daniel head to Mexico while attempting to conceal a sudden & mysterious supernatural power.",
                    ReleaseDate = new DateTime(2018, 9, 26, 0, 0, 0),
                    Price = 628
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 6479,
                    UnitsInStock = 7,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[9]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[7],
                    Name = "Shadow Tactics: Blades of the Shogun",
                    Key = "Shadow Tactics Blades of the Shogun",
                    Description =
                        "Shadow Tactics is a hardcore tactical stealth game set in Japan around the Edo period. " +
                        "A new Shogun seizes power over Japan and enforces nationwide peace. " +
                        "In his battle against conspiracy and rebellion, he recruits five specialists with extraordinary skills for assassination, sabotage and espionage.",
                    ReleaseDate = new DateTime(2016, 12, 6, 0, 0, 0),
                    Price = 479
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 27073,
                    UnitsInStock = 1,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2]
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[3],
                    Name = "Just Cause 2",
                    Key = "Just Cause 2",
                    Description =
                        "Dive into an adrenaline-fuelled free-roaming adventure with 400 square miles of rugged terrain and hundreds of weapons and vehicles.",
                    ReleaseDate = new DateTime(2010, 4, 23, 0, 0, 0),
                    Price = 14116
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 5307,
                    UnitsInStock = 100,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[2],
                        _genres[4],
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[3],
                    Name = "Murdered: Soul Suspect",
                    Key = "Murdered Soul Suspect",
                    Description =
                        "MURDERED: SOUL SUSPECT™ takes players into a whole new realm of mystery where the case is personal and the clues just out of reach, " +
                        "challenging gamers to solve the hardest case of all… their own murder.",
                    ReleaseDate = new DateTime(2014, 01, 6, 0, 0, 0),
                    Price = 379
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 17498,
                    UnitsInStock = 80,
                    Genres = new List<Genre>
                    {
                        _genres[0],
                        _genres[1],
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[6],
                    Name = "Depth",
                    Key = "Depth",
                    Description =
                        "Play as a shark or a diver in a dark aquatic world and overcome your enemies by employing cunning, teamwork, and stealth. " +
                        "Depth blends tension and visceral action as you team up against AI or be matched with other players in heart pounding combat.",
                    ReleaseDate = new DateTime(2014, 11, 3, 0, 0, 0),
                    Price = 379
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 135,
                    UnitsInStock = 30,
                    Genres = new List<Genre>
                    {
                        _genres[3],
                        _genres[9],
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[6],
                    Name = "Fishing Sim World",
                    Key = "Fishing Sim World",
                    Description =
                        "Fishing Sim World is the most authentic fishing simulator ever made, taking you around the world on an angling journey like no other.",
                    ReleaseDate = new DateTime(2018, 9, 17, 0, 0, 0),
                    Price = 579
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 7235,
                    UnitsInStock = 40,
                    Genres = new List<Genre>
                    {
                        _genres[3],
                        _genres[6],
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[16],
                    Name = "Car Mechanic Simulator 2015",
                    Key = "Car Mechanic Simulator 2015",
                    Description =
                        "New cars, new tools, new options, more parts and much more fun in the next version of Car Mechanic Simulator! " +
                        "Take your wrench! Create and expand your auto repairs service empire. " +
                        "Car Mechanic Simulator 2015 will take you behind the scenes of daily routine in car workshop.",
                    ReleaseDate = new DateTime(2015, 4, 23, 0, 0, 0),
                    Price = 579
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 11245,
                    UnitsInStock = 22,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[4],
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[13],
                    Name = "Deus Ex: Human Revolution - Director's Cut",
                    Key = "Deus Ex Human Revolution Directors Cut",
                    Description =
                        "You play Adam Jensen, an ex-SWAT specialist who's been handpicked to oversee the defensive needs of " +
                        "one of America's most experimental biotechnology firms. Your job is to safeguard company secrets, " +
                        "but when a black ops team breaks in and kills the very scientists you were hired to protect, everything you ",
                    ReleaseDate = new DateTime(2013, 10, 25, 0, 0, 0),
                    Price = 279
                },
                new Game
                {
                    Id = Guid.NewGuid().ToString(),
                    Comments = new List<Comment>(),
                    ViewCount = 14476,
                    UnitsInStock = 14,
                    Genres = new List<Genre>
                    {
                        _genres[1],
                        _genres[4],
                    },
                    PlatformTypes = new List<PlatformType>
                    {
                        _platformTypes[1]
                    },
                    Publisher = _publishers[13],
                    Name = "Deus Ex: Mankind Divided",
                    Key = "Deus Ex Mankind Divided",
                    Description =
                        "Now an experienced covert operative, Adam Jensen is forced to operate in a world that has grown to despise his kind. " +
                        "Armed with a new arsenal of state-of-the-art weapons and augmentations, he must choose the right approach, along with who to trust, " +
                        "in order to unravel a vast worldwide conspiracy.",
                    ReleaseDate = new DateTime(2016, 08, 23, 0, 0, 0),
                    Price = 249
                },
            };

            return games;
        }
    }
}




