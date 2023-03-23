namespace credit_assesment;

class Program
{   
    const int LOAN = 80000;
    static void Main(string[] args)
    {
        //Crea instancia de clase Data para almacenar lhistorial de utilidades del ejercicio anterior
        Data pronostico = new Data();

        //Almacena la intancia creada en un arreglo de resultados
        Array.Copy(pronostico.data,4,pronostico.result,0,22);
        
        //Calculo de promedios y sumatorias por cada mes necesarias para calcular B0 y B1
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

        //Calculo de B1 con lo ya calculado
        pronostico.b1 = b1(pronostico.sumXY, pronostico.sumXSq, pronostico.averageX, pronostico.averageY, pronostico.data.Length/4);
        
        //Calculo de B0 en base al B1 calculado
        pronostico.b0 = b0(pronostico.averageX, pronostico.averageY, pronostico.b1);

        //Agrega el calculo de las estimaciones de los siguientes 12 meses en un arreglo temporal
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
        
        //Junta los datos originales con los calculos realizados en el arreglo de resultados final
        pronostico.result = MergeArray(pronostico.data,tmpArray);

        //suma los pagos que se podrian hacer
        int futureYearSum = 0;
        for (int i = 13; i < pronostico.result.Length/4; i++)
        {
            futureYearSum += Convert.ToInt32(pronostico.result[i,1]);
        }

        //Revisa en cuantos meses se puede pagar el prestamo
        double avgFutureYear = futureYearSum / 12;
        int n = 0;
        while (pronostico.paid < LOAN){
            pronostico.paid += avgFutureYear * .1;
            n += 1;
        }


        //Muestra en consola en cuantos años se puede pagar el prestamo y si este es posible de pagar en 36 o 60 meses
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

    //Creacion de funciones con calculos necesarios
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
