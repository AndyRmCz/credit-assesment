namespace credit_assesment;


class Program
{   
    const int LOAN = 80000;
    static void Main(string[] args)
    {
        Data pronostico = new Data();
        Array.Copy(pronostico.data,4,pronostico.result,0,22);
        
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
        int firstNextMonth = pronostico.data.Length/4;
        string[,] tmpArray = new string[12,4];
        for (int i = 0; i < 12; i++){
            firstNextMonth += 1;
            double tmp = Math.Round(pronostico.b0 + (pronostico.b1*firstNextMonth),0);
            tmpArray[i, 0] = "";
            tmpArray[i, 1] = Convert.ToString(tmp);
            tmpArray[i, 2] = "";
            tmpArray[i, 3] = "";
        }
        
        pronostico.result = MergeArray(pronostico.data,tmpArray);

        int futureYearSum = 0;
        for (int i = 13; i < pronostico.result.Length/4; i++)
        {
            futureYearSum += Convert.ToInt32(pronostico.result[i,1]);
        }

        double avgFutureYear = futureYearSum / 12;
        int n = 0;
        while (pronostico.paid < LOAN){
            pronostico.paid += avgFutureYear * .1;
            n += 1;
        }

        // System.Console.WriteLine(avgFutureYear * .1);
        // System.Console.WriteLine(pronostico.paid);
        // System.Console.WriteLine(Convert.ToDouble(n)/12);

        System.Console.WriteLine("DeAntojo S.A. de C.V. podría pagar su prestamso de $80,000 en " + Math.Round(Convert.ToDouble(n)/12,2) + " años");
        
        if (n <= 60){
            if (n <= 36){
                System.Console.WriteLine("El credito se puede pagar en al menos 36 meses");
            }
            else{
                System.Console.WriteLine("El credito se puede pagar en al menos 60 meses");
            }
        }
        else{
            System.Console.WriteLine("El credito requiere más de 60 meses para pagarse");
        }
    }

    public static double b0(double avgX, double avgY, double b1){
        return avgY - (b1*avgX);
    }
    public static double b1(double sumXY, double sumXSq, double avgX, double avgY, int n){
        return (sumXY - (n*avgX*avgY))/(sumXSq-(n*Math.Pow(avgX,2)));
    }

    static public string[,] MergeArray(string[,] firstArray, string[,] secondArray)
{
    var combinedArray = new string[firstArray.Length/4 + secondArray.Length/4,4];
    Array.Copy(firstArray, combinedArray, firstArray.Length);
    Array.Copy(secondArray, 0, combinedArray, firstArray.Length, secondArray.Length);
    return combinedArray;
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

    public string[,] result = new string[25,4];

    public double paid = 0;
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