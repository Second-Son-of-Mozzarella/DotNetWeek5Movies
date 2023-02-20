using NLog; 
using System.Text.RegularExpressions;
int UID = 0;
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();


string file = "movies.csv";




Console.WriteLine("Welcome to movie look up and add \n \n [1] to look at current file data \n [2] to add to the file data");
int response = Int32.Parse(Console.ReadLine());

if(response == 1){
    StreamReader sr = new StreamReader(file);
         while (!sr.EndOfStream){
              string line = sr.ReadLine();
                 // convert string to array
             string[] data = Regex.Split(line, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

             string[] genresArray = data[2].Split('|');
            
            


                // display array data
                Console.WriteLine($" UID: {data[0]}\n Name: {data[1]} \n Genres: {string.Join(", ", genresArray)} \n \n");
                }

                sr.Close(); 
}if(response == 2){
    
    
     

     

        
     StreamWriter sw = new StreamWriter(file, true);
     Console.WriteLine("How many movies would you like to add");
     int movieNumb = Int32.Parse(Console.ReadLine());
   

    String csvInfo = System.IO.File.ReadAllText(file);
    List<string> csvCheck = Regex.Split(csvInfo, "\n").ToList();

        for(int i = 0; i < movieNumb; i++){
            UID += 1;

            

            Console.WriteLine("What is the name of your movie");
            string movieName = Console.ReadLine();
            Console.WriteLine("How many genres applies to this movie");
            int genreNumb = Int32.Parse(Console.ReadLine());
            string[] genres = new string[genreNumb];
            for(int x = 0; x < genreNumb; x++){
                Console.WriteLine($"genre #{x} is ");
                string genrename = Console.ReadLine();
                genres[x] = genrename;
            }
            

            
                sw.WriteLine($"{UID},{movieName},{string.Join("|", genres)}");
            
            
            
                    

              
            

        }

    sw.Close();
     

}