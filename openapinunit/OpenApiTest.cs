using System;
using System.Collections;
using System.Collections.Generic;
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
        public void TransformerGetAsyncOpenApiV3(int sourceEpsg, int targetEpsg)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                OpenApi.OpenApiV3.Epsg source = (OpenApi.OpenApiV3.Epsg)sourceEpsg;
                OpenApi.OpenApiV3.Epsg target = (OpenApi.OpenApiV3.Epsg)targetEpsg;

                var openApi3 = new OpenApi.OpenApiV3.TransformerClient(httpClient);

                var x = NormalDist.RandNormal(10d, 1d);
                var y = NormalDist.RandNormal(60d, 1d);
                var z = NormalDist.RandNormal(100d, 10d);
                var t = NormalDist.RandNormal(2020d, 2d);

                var res = openApi3.GetAsync(x, y, z, t, source, target);                
 
                Assert.IsNotNull(res);
            }
        }

        [TestCase(7912, 4937, 10000)]
        [TestCase(4258, 25832, 10000)]
        public void TransformerPostAsyncOpenApiV3(int sourceEpsg, int targetEpsg, int samples = 100)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                OpenApi.OpenApiV3.Epsg source = (OpenApi.OpenApiV3.Epsg)sourceEpsg;
                OpenApi.OpenApiV3.Epsg target = (OpenApi.OpenApiV3.Epsg)targetEpsg;
                
                var openApi3 = new OpenApi.OpenApiV3.TransformerClient(httpClient);
                
                var body = new List<OpenApi.OpenApiV3.CoordParams>()
                {
                    new OpenApi.OpenApiV3.CoordParams
                    {  
                        X = NormalDist.RandNormal(10d, 1d),
                        Y = NormalDist.RandNormal(60d, 1d),
                        Z = NormalDist.RandNormal(100d, 10d),
                        T = NormalDist.RandNormal(2020d, 2d)
                    }
                };
                
                var res = openApi3.PostAsync(source, target, body).Result;
                
                Assert.IsNotNull(res);
            }                         
        }

        [TestCase(4258, 25832, 10000)]
        [TestCase(7912, 4937, 10000)]
        [TestCase(4258, 27393, 10000)]
        public void StressTestPostAsyncOpenApiV3(int sourceEpsg, int targetEpsg, int samples = 100)
        {
            OpenApi.OpenApiV3.Epsg source = (OpenApi.OpenApiV3.Epsg)sourceEpsg;
            OpenApi.OpenApiV3.Epsg target = (OpenApi.OpenApiV3.Epsg)targetEpsg;

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var openApi3 = new OpenApi.OpenApiV3.TransformerClient(httpClient);
                var body = new List<OpenApi.OpenApiV3.CoordParams>();

                for (int i = 0; i < samples; i++)
                {
                    body.Add(new OpenApi.OpenApiV3.CoordParams
                    {
                        X = NormalDist.RandNormal(10d, 1d),
                        Y = NormalDist.RandNormal(60d, 1d),
                        Z = NormalDist.RandNormal(100d, 10d),
                        T = NormalDist.RandNormal(2020d, 2d)
                    });
                }

                var res = openApi3.PostAsync(source, target, body).Result;

                Assert.IsNotNull(res);
            }       
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        public void ProjeksjonerGetAsyncOpenApiV3(int coordSystem)
        {
            var cs = (OpenApi.OpenApiV3.CoordinateSystem)coordSystem;
            
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var openApi3 = new OpenApi.OpenApiV3.ProjeksjonerClient(httpClient);                

                var res = openApi3.GetAsync(OpenApi.OpenApiV3.Cat.Enkel, cs).Result;
              
                Assert.IsNotNull(res);
            }
        }
    }
}