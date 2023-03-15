namespace credit_assesment;


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Data pronostico = new Data();
        Array.Copy(pronostico.data,4,pronostico.result,0,22);

        
        System.Console.WriteLine(pronostico.result[0,2]);
        
        for (int i = 1; i <= (pronostico.data.Length/4); i++)
        {
            pronostico.data[i-1,2] = Convert.ToString(i * Convert.ToInt32(pronostico.data[i-1,1]));
            pronostico.sumXY += Convert.ToDouble(pronostico.data[i-1,2]);
            pronostico.data[i-1,3] = Convert.ToString(i * i);
            pronostico.sumXSq += Convert.ToDouble(pronostico.data[i-1,3]);
            
            pronostico.sumY += Convert.ToDouble(pronostico.data[i-1,1]);
            
            pronostico.averageX += i;
            pronostico.averageY += Convert.ToDouble(pronostico.data[i-1,1]);
        }
        pronostico.averageX = pronostico.averageX / (pronostico.data.Length/4);
        pronostico.averageY = pronostico.averageY / (pronostico.data.Length/4);

        pronostico.b1 = b1(pronostico.sumXY, pronostico.sumXSq, pronostico.averageX, pronostico.averageY, pronostico.data.Length/4);
        
        pronostico.b0 = b0(pronostico.averageX, pronostico.averageY, pronostico.b1);

         //Next: Try while loop until array ends to add future values based on number of known months

        
        
    }

    public static double b0(double avgX, double avgY, double b1){
        return avgY - (b1*avgX);
    }
    public static double b1(double sumXY, double sumXSq, double avgX, double avgY, int n){
        return (sumXY - (n*avgX*avgY))/(sumXSq-(n*Math.Pow(avgX,2)));
    }
}

class Data{

    public string[,] data = {
        {"Enero 2022", "31000","",""},
        {"Febrero 2022", "32000","",""},
        {"Marzo 2022", "33000","",""},
        {"Abril 2022", "31500","",""},
        {"Mayo 2022", "33400","",""},
        {"Junio 2022", "35000","",""},
        {"Julio 2022", "37300","",""},
        {"Agosto 2022", "36100","",""},
        {"Septiembre 2022", "37000","",""},
        {"Octubre 2022", "36200","",""},
        {"Noviembre 2022", "35000","",""},
        {"Diciembre 2022", "38300","",""},
        {"Enero 2023", "37800","",""}
    };

    public double averageX = 0;
    public double averageY = 0;
    public double   sumY = 0;
    public double sumXSq = 0;
    public double sumXY = 0;
    public double b0 = 0;
    public double b1 = 0;

    public string[,] result = new string[26,4];
}
class Handling

{
     public static Char GetKeyPress(String msg, Char[] validChars){
      ConsoleKeyInfo keyPressed;
      bool valid = false;

      Console.WriteLine();
      do {
         Console.Write(msg);
         keyPressed = Console.ReadKey();
         Console.WriteLine();
         if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
            valid = true;
      } while (! valid);
      return keyPressed.KeyChar;
  } 
}

class Table
{
    static int tableWidth = 70;
    static string[] columns = {"Column1", "column2"};
    static void PrintLine(){
        Console.WriteLine(new string('-', tableWidth));
    }

    static void PrintRow(params string[] columns){
        int width = (tableWidth - columns.Length) / columns.Length;
        string row = "|";

        foreach (string column in columns){
            row += AlignCenter(column, width) + "|";
        }

        Console.WriteLine(row);
    }

    static string AlignCenter(string text, int width){
        text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

        if (string.IsNullOrEmpty(text))
        {
            return new string(' ', width);
        }
        else
        {
            return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
        }
    }

    public static void PrintTable(){
        
        PrintLine();
        PrintRow(columns);
        // for(int i = 1; i <= plazo;i++){
        //     saldo = saldo - (totalCredito / plazo);
        //     PrintLine();
        //     PrintRow(Convert.ToString(i),Convert.ToString((Math.Round(totalCredito / plazo,2))),Convert.ToString((monto/plazo).ToString("C")),Convert.ToString((interes/plazo).ToString("C")),Convert.ToString((saldo).ToString("C")));
        // }
        PrintLine();
        Console.ReadLine();
    }
}