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
        public void TransformerGetAsyncOpenApiV2(int sourceEpsg, int targetEpsg)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                OpenApi.OpenApiV2.Epsg source = (OpenApi.OpenApiV2.Epsg)sourceEpsg;
                OpenApi.OpenApiV2.Epsg target = (OpenApi.OpenApiV2.Epsg)targetEpsg;

                var openApi2 = new OpenApi.OpenApiV2.TransformerClient(httpClient);

                var x = NormalDist.RandNormal(10d, 1d);
                var y = NormalDist.RandNormal(60d, 1d);
                var z = NormalDist.RandNormal(100d, 10d);
                var t = NormalDist.RandNormal(2020d, 2d);

                var res = openApi2.GetAsync(x, y, z, t, source, target);                
 
                Assert.IsNotNull(res);
            }
        }

        [TestCase(7912, 4937)]
        [TestCase(4258, 25832)]
        public void TransformerPostAsyncOpenApiV2(int sourceEpsg, int targetEpsg)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                OpenApi.OpenApiV2.Epsg source = (OpenApi.OpenApiV2.Epsg)sourceEpsg;
                OpenApi.OpenApiV2.Epsg target = (OpenApi.OpenApiV2.Epsg)targetEpsg;
                
                var openApi2 = new OpenApi.OpenApiV2.TransformerClient(httpClient);
                
                var body = new List<OpenApi.OpenApiV2.CoordParams>()
                {
                    new OpenApi.OpenApiV2.CoordParams
                    {  
                        X = NormalDist.RandNormal(10d, 1d),
                        Y = NormalDist.RandNormal(60d, 1d),
                        Z = NormalDist.RandNormal(100d, 10d),
                        T = NormalDist.RandNormal(2020d, 2d)
                    }
                };
                
                var res = openApi2.PostAsync(source, target, body).Result;
                
                Assert.IsNotNull(res);
            }                         
        }

        [TestCase(7912, 4937)]
        [TestCase(4258, 25832)]
        public void StressTestPostAsyncOpenApiV2(int sourceEpsg, int targetEpsg)
        {
            int samples = 10000; // Max number of points

            OpenApi.OpenApiV2.Epsg source = (OpenApi.OpenApiV2.Epsg)sourceEpsg;
            OpenApi.OpenApiV2.Epsg target = (OpenApi.OpenApiV2.Epsg)targetEpsg;

            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var openApi2 = new OpenApi.OpenApiV2.TransformerClient(httpClient);
                var body = new List<OpenApi.OpenApiV2.CoordParams>();

                for (int i = 0; i < samples; i++)
                {
                    body.Add(new OpenApi.OpenApiV2.CoordParams
                    {
                        X = NormalDist.RandNormal(10d, 1d),
                        Y = NormalDist.RandNormal(60d, 1d),
                        Z = NormalDist.RandNormal(100d, 10d),
                        T = NormalDist.RandNormal(2020d, 2d)
                    });
                }

                var res = openApi2.PostAsync(source, target, body).Result;

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
        public void ProjeksjonerGetAsyncOpenApiV2(int coordSystem)
        {
            var cs = (OpenApi.OpenApiV2.CoordinateSystem)coordSystem;
            
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                var openApi2 = new OpenApi.OpenApiV2.ProjeksjonerClient(httpClient);
              
                var res = openApi2.GetAsync(OpenApi.OpenApiV2.Cat.Enkel, cs).Result;
              
                Assert.IsNotNull(res);
            }
        }
    }
}