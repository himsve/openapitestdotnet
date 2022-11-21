using System;
using OpenApi;

namespace OpenApi
{
    class Program
    {
        public static bool WriteToConsole = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Lists EPSG codes");

            ListEPSGCodes();

            Console.WriteLine("Transformation is started");

            TestOpenApiV1();

            Transform(4258, 25832, 10d, 60d);

            // EPSG 4312 er ugyldig. Vil returnere ein exception:
            Transform(4258, 4312, 10d, 60d);
            
            Console.WriteLine("Transformation completed!");
        }

        static void Transform(int epsgSource, int epsgTarget, double x, double y, double z = 0d, double t = 0d)
        {
            try
            {
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var oa = new OpenApiV1.TransformerClient(httpClient);
                    
                    OpenApiV1.Epsg epsgS = (OpenApiV1.Epsg)Enum.ToObject(typeof(OpenApiV1.Epsg), epsgSource);
                    OpenApiV1.Epsg epsgT = (OpenApiV1.Epsg)Enum.ToObject(typeof(OpenApiV1.Epsg), epsgTarget);
                    
                    var res = oa.GetAsync(x, y, z, t, epsgS, epsgT).Result;
                }                
            }
            catch (AggregateException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        static void ListEPSGCodes()
        {
            try
            {
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var oa = new OpenApiV1.ProjeksjonerClient(httpClient);

                    var res = oa.GetAsync(OpenApiV1.Cat.Enkel, null);
                    
                  //  if (res.IsCompleted)
                    {
                        var epsgResult = res.Result;

                        foreach (var value in epsgResult)
                            Console.WriteLine($" {value.Epsg } {value.Name}");
                    }
                }
            }
            catch (AggregateException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        static void TestOpenApiV1()
        {
            try
            {
                using (var httpClient = new System.Net.Http.HttpClient())
                {
                    var oa = new OpenApiV1.TransformerClient(httpClient);
                    
                    var x = 9d; var y = 60d;
                    var rand = new Random();

                    for (int i = 0; i < 100; i++)
                    {
                        var xrand = x + rand.NextDouble();
                        var yrand = y + rand.NextDouble();
                        var z = 0; //200d + rand.NextDouble()*50d;
                        var t = 2020d;

                        var res = oa.GetAsync(xrand, yrand, z, t, OpenApiV1.Epsg._4258, OpenApiV1.Epsg._25832).Result;

                        if (WriteToConsole)
                            Console.WriteLine(
                                $"Input:  x {xrand:F9} y {yrand:F9} z {z:F5} t {t:F2}\n\r" +
                                $"Output: x {res.X:F9} y {res.Y:F9} z {res.Z:F5}");
                    }
                }
            }
            catch (AggregateException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
