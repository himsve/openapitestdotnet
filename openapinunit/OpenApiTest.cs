using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenApi;

namespace openapinunit
{
    [TestFixture]
    public class OpenApiTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(7912, 4937)]
        [TestCase(4258, 25832)]
        // [TestCase(5004, 11000)]
        public async Task TransformerGetAsyncOpenApiV1Async(int sourceEpsg, int targetEpsg)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                OpenApi.OpenApiV1.Epsg source = (OpenApi.OpenApiV1.Epsg)sourceEpsg;
                OpenApi.OpenApiV1.Epsg target = (OpenApi.OpenApiV1.Epsg)targetEpsg;

                var openApi1 = new OpenApi.OpenApiV1.TransformerClient(httpClient);

                var x = NormalDist.RandNormal(10d, 1d);
                var y = NormalDist.RandNormal(60d, 1d);
                var z = NormalDist.RandNormal(100d, 10d);
                var t = NormalDist.RandNormal(2022d, 2d);
 
                var res = await openApi1.GetAsync(x, y, z, t, source, target);
                
                Assert.IsNotNull(res);
            }
        }

        [TestCase(7912, 4937, 5)]
        [TestCase(4258, 25832, 5)]
        public async Task TransformerPostAsyncOpenApiV1(int sourceEpsg, int targetEpsg, int samples = 100)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                OpenApi.OpenApiV1.Epsg source = (OpenApi.OpenApiV1.Epsg)sourceEpsg;
                OpenApi.OpenApiV1.Epsg target = (OpenApi.OpenApiV1.Epsg)targetEpsg;
                
                var openApi1 = new OpenApi.OpenApiV1.TransformerClient(httpClient);
                
                var body = new List<OpenApi.OpenApiV1.CoordParams>()
                {
                    new OpenApi.OpenApiV1.CoordParams
                    {  
                        X = NormalDist.RandNormal(10d, 1d),
                        Y = NormalDist.RandNormal(60d, 1d),
                        Z = NormalDist.RandNormal(100d, 10d),
                        T = NormalDist.RandNormal(2022d, 2d)
                    }
                };
                
                var res = await openApi1.PostAsync(source, target, body);
                
                Assert.IsNotNull(res);
            }                         
        }

        [TestCase(4258, 25832, 10000)]
        [TestCase(7912, 4937, 10000)]
        [TestCase(4258, 27393, 10000)]
        public async Task StressTestPostAsyncOpenApiV1(int sourceEpsg, int targetEpsg, int samples = 100)
        {
            OpenApi.OpenApiV1.Epsg source = (OpenApi.OpenApiV1.Epsg)sourceEpsg;
            OpenApi.OpenApiV1.Epsg target = (OpenApi.OpenApiV1.Epsg)targetEpsg;

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var openApi1 = new OpenApi.OpenApiV1.TransformerClient(httpClient);
                var body = new List<OpenApi.OpenApiV1.CoordParams>();

                for (int i = 0; i < samples; i++)
                {
                    body.Add(new OpenApi.OpenApiV1.CoordParams
                    {
                        X = NormalDist.RandNormal(10d, 1d),
                        Y = NormalDist.RandNormal(60d, 1d),
                        Z = NormalDist.RandNormal(100d, 10d),
                        T = NormalDist.RandNormal(2020d, 2d)
                    });
                }

                var res = await openApi1.PostAsync(source, target, body);

                Assert.IsNotNull(res);
            }       
        }

        [TestCase(0)] // CD = 0
        [TestCase(1)] // ED50 = 1
        [TestCase(2)] // ETRS89
        [TestCase(3)] // ITRF2014
        [TestCase(4)] // NGO48
        [TestCase(5)] // NN54
        [TestCase(6)] // NN2000
        [TestCase(7)] // WGS84 
        public async Task ProjeksjonerGetAsyncOpenApiV1(int coordSystem)
        {
            var cs = (OpenApi.OpenApiV1.CoordinateSystem)coordSystem;
            
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var openApi1 = new OpenApi.OpenApiV1.ProjeksjonerClient(httpClient);                

                var res = await openApi1.GetAsync(OpenApi.OpenApiV1.Cat.Enkel, cs);
              
                Assert.IsNotNull(res);
            }
        }
    }
}