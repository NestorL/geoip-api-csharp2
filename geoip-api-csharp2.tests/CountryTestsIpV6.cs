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
    public class CountryTestsIpV6
    {
        protected const string GeoipDbPath = "dbs/";
        protected const string GeoipDb = "GeoLiteCity.dat";

        [Test]
        public void FindRegionCacheDbIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);

            using (var ls = new LookupService(geoipDb, LookupService.GEOIP_MEMORY_CACHE)) // Set cached in memory database
            {

                Country c = ls.getCountryV6("0:0:0:0:0:ffff:ba21:ea1c");

                c.ShouldNotBeNull();
                c.getCode().ShouldEqual("--");
                c.getName().ShouldEqual("N/A");

            }
        }

        [Test]
        public void FindCityDbAccessIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);

            using (var ls = new LookupService(geoipDb))
            {
                Country c = ls.getCountryV6("0:0:0:0:0:ffff:ba21:ea1c");

                c.ShouldNotBeNull();
                c.getCode().ShouldEqual("--");
                c.getName().ShouldEqual("N/A");
            }
        }

        [Test]
        public void UnknownCityReturnsUknownNotFailing()
        {
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);

            using (var ls = new LookupService(geoipDb))
            {
                Country c = ls.getCountryV6("0:0:0:0:0:ffff:ba21:ea1c");

                c.ShouldNotBeNull();
                c.getCode().ShouldEqual("--");
                c.getName().ShouldEqual("N/A");
            }
        }


    }
}
