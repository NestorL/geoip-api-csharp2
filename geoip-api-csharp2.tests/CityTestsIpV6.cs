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
    public class CityTestsIpV6
    {
        protected const string GeoipDbPath = "dbs/";
        protected const string GeoipDb = "GeoLiteCityv6.dat";

        [Test]
        public void FindCityCacheDbIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);
            //LookupService ls = new LookupService(geoipDb, LookupService.GEOIP_STANDARD);
            LookupService ls = new LookupService(geoipDb, LookupService.GEOIP_MEMORY_CACHE); // Set cached in memory database

            Location l = ls.getLocationV6("0:0:0:0:0:ffff:ba21:ea1c");

            l.ShouldNotBeNull();
            l.countryCode.ShouldEqual("AR");
            l.countryName.ShouldEqual("Argentina");
            l.latitude.ShouldEqual(-34);
            l.longitude.ShouldEqual(-64);
        }

        [Test]
        public void FindCityDbAccessIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoipDb);
            //LookupService ls = new LookupService(geoipDb, LookupService.GEOIP_STANDARD);
            LookupService ls = new LookupService(geoipDb); // Defaults to LookupService.GEOIP_STANDARD

            Location l = ls.getLocationV6("0:0:0:0:0:ffff:ba21:ea1c");

            l.ShouldNotBeNull();
            l.countryCode.ShouldEqual("AR");
            l.countryName.ShouldEqual("Argentina");
            l.latitude.ShouldEqual(-34);
            l.longitude.ShouldEqual(-64);
        }

    }
}
