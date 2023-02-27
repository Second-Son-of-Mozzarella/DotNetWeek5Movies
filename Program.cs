using NLog;
using System.Text.RegularExpressions;
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();


string file = "movies.csv";
if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
    // regex is  ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"
    List<UInt64> UID = new List<UInt64>();
    List<string> MovieNames = new List<string>();
    List<string> MovieGenres = new List<string>();
    try
    {
        StreamReader sr = new StreamReader(file);
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line is not null)
            {


                string[] movieDetails = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"); // I used regex because its simpler
                UID.Add(UInt64.Parse(movieDetails[0]));

                MovieNames.Add(movieDetails[1].Replace("\"", ""));

                MovieGenres.Add(movieDetails[2].Replace("|", ", "));

            }
        }
        sr.Close();
    }
    catch (Exception ex)
    {
        logger.Error(ex.Message);
    }

    Console.WriteLine("\n\tWelcome to movie look up and add \n \n [1] to look at current file data \n [2] to add to the file data \n any other key to exit");
    int response = Int32.Parse(Console.ReadLine());
    do
    {
        if (response == 1)
        {
            StreamReader sr = new StreamReader(file);
            for (int i = 0; i < UID.Count; i++)
            {
                Console.WriteLine($"\t {MovieNames[i]} \n UID: {UID[i]} \n Genre: {MovieGenres[i]} \n \n");
            }

            sr.Close();
        }
        if (response == 2)
        {




            UInt64 UIDmax = UID.Max() + 1;

            // Console.WriteLine("How many movies would you like to add");
            // int movieNumb = Int32.Parse(Console.ReadLine());

            // for (int i = 0; i < movieNumb; i++)
            // {

                Console.WriteLine("What is the name of the movie: ");
                string? movieName = Console.ReadLine();


                if (movieName is not null)
                {
                    // I could not find a way to do this the original way I was parsing the movies file
                    List<string> LowerCaseMovieTitles = MovieNames.ConvertAll(t => t.ToLower());
                    if (LowerCaseMovieTitles.Contains(movieName.ToLower()))
                    {
                        logger.Info($"More that one {movieName} in movie titles,\n if there is a remake add (date) to the end of the title");
                    }
                    else
                    {

                        UInt64 MaxUID = UID.Max() + 1;


                        List<string> genres = new List<string>();
                        string? genre;
                        Console.WriteLine("How many genres apply to this film");
                        int response2 = Int32.Parse(Console.ReadLine());

                        for (int j = 0; j < response2; j++)
                        {
                            Console.WriteLine($"Enter genre #{j + 1}");
                            genre = Console.ReadLine();
                            if (genre is not null)
                            {
                                genres.Add(genre);
                            }
                        }
                        if (genres.Count == 0)
                        {
                            genres.Add("N/A");
                        }
                        string genresString = string.Join("|", genres);
                        movieName = movieName.IndexOf(',') != -1 ? $"\"{movieName}\"" : movieName;

                        StreamWriter sw = new StreamWriter(file, true); // it took me way to long to learn that you just add the true to append
                        sw.WriteLine($"{MaxUID},{movieName},{genresString}");
                        sw.Close();
                        UID.Add(MaxUID);
                        MovieNames.Add(movieName);
                        MovieGenres.Add(genresString);
                        

                    }
                }

                break; // it will loop infinatly otherwise

            //}



            /*  at this point of rewriting I thought I should leave some old code 
                in to for you to see how I did it before your presentation  */

            // UID += 1; 

            // Console.WriteLine("What is the name of your movie");
            // string movieName = Console.ReadLine();
            // Console.WriteLine("How many genres applies to this movie");
            // int genreNumb = Int32.Parse(Console.ReadLine());
            // string[] genres = new string[genreNumb];
            // for(int x = 0; x < genreNumb; x++){
            //     Console.WriteLine($"genre #{x} is ");
            //     string genrename = Console.ReadLine();
            //     genres[x] = genrename;
            // }


            //     sw.WriteLine($"{UID},{movieName},{string.Join("|", genres)}");


        }

    } while (response == 1 || response == 2);
}



