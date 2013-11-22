using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
//using NUnit.Framework.Helpers.Extensions;

namespace geoip_api_csharp2.tests
{
    [TestFixture]
    public class CityTestsIpV4
    {
        protected const string GeoipDbPath = "dbs/";
        protected const string GeoipDb = "GeoLiteCity.dat";

        [Test]
        public void FindCityCacheDbIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);

            using (var ls = new LookupService(geoipDb, LookupService.GEOIP_MEMORY_CACHE)) // Set cached in memory database
            {

                Location l = ls.getLocation("186.33.234.28");

                l.ShouldNotBeNull();
                l.countryCode.ShouldEqual("AR");
                l.countryName.ShouldEqual("Argentina");
                l.latitude.ShouldEqual(-34);
                l.longitude.ShouldEqual(-64);

            }
        }

        [Test]
        public void FindCityDbAccessIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);

            using (var ls = new LookupService(geoipDb))
            {
                Location l = ls.getLocation("186.33.234.28");

                l.ShouldNotBeNull();
                l.countryCode.ShouldEqual("AR");
                l.countryName.ShouldEqual("Argentina");
                l.latitude.ShouldEqual(-34);
                l.longitude.ShouldEqual(-64);
            }
        }

        [Test]
        public void UnknownCityReturnsNullNotFailing()
        {
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);

            using (var ls = new LookupService(geoipDb))
            {
                Location l = ls.getLocation("0.2.3.4");

                l.ShouldBeNull();
            }
        }


    }
}
