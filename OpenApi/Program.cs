using System;
using OpenApi;

namespace OpenApi
{
    class Program
    {
        public static bool WriteToConsole = true;

        static void Main(string[] args)
        {
            Console.WriteLine("Transformation is started");

            Transform(4258, 4312, 10d, 60d);
            Transform(4258, 25832, 10d, 60d);            

            TestOpenApiV1();
            
            Console.WriteLine("Transformation completed!");
        }

        static void Transform(int epsgSource, int epsgTarget, double x, double y, double z = 0d, double t = 0d)
        {
            try
            {
                using var httpClient = new System.Net.Http.HttpClient();

                var oa = new OpenApiV1.TransformerClient(httpClient);

                OpenApiV1.Epsg epsgS = (OpenApiV1.Epsg)Enum.ToObject(typeof(OpenApiV1.Epsg), epsgSource);
                OpenApiV1.Epsg epsgT = (OpenApiV1.Epsg)Enum.ToObject(typeof(OpenApiV1.Epsg), epsgTarget);

                var res = oa.GetAsync(x, y, z, t, epsgS, epsgT).Result;
            }
            catch (AggregateException ex)
            {
            }
            catch (Exception ex)
            {   
            }
        }

        static void TestOpenApiV1()
        {
            using var httpClient = new System.Net.Http.HttpClient();

            var oa = new OpenApiV1.TransformerClient(httpClient);

            var x = 9d; var y = 60d;
            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                var xrand = x + rand.NextDouble();
                var yrand = y + rand.NextDouble();
                var z = 200d + rand.NextDouble()*50d;
                var t = 2020d;

                //var res = oa.GetAsync(xrand, yrand, z, t, OpenApiV1.Epsg._4258, OpenApiV1.Epsg._25832).Result;
                var res = oa.GetAsync(xrand, yrand, z, t, OpenApiV1.Epsg._7912, OpenApiV1.Epsg._4937).Result;                

                if (WriteToConsole)
                    Console.WriteLine($"Input: x {xrand} y {yrand} z {z} t {t}, Output: x {res.X} y {res.Y} z {res.Z}");
            }            
        }        
    }
}
