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
    public class RegionTestsIpV4
    {
        protected const string GeoipDbPath = "dbs/";
        protected const string GeoLiteCityDb = "GeoLiteCity.dat";
        protected const string GeoIpRegionDb = "GeoIPRegion.dat";

        [Test]
        public void FindRegionCacheDbIp()
        {
            // 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoLiteCityDb);

            using (var ls = new LookupService(geoipDb)) // Set cached in memory database
            {

                Region r = ls.getRegion("186.33.234.28");

                r.ShouldNotBeNull();
                //r.getcountryCode().ShouldEqual("AR");
                //r.getcountryName().ShouldEqual("Argentina");

            }
        }

        [Test]
        public void FindCityDbAccessIp()
        {
            // Get location 186.33.234.28 Should give country code "AR", country "Argentina", latitude -34 and longitude -64
            string geoipDb = Path.Combine(GeoipDbPath, GeoIpRegionDb);

            using (var ls = new LookupService(geoipDb))
            {
                Region r = ls.getRegion("64.17.254.223");
                //Region r = ls.getRegion("186.33.234.28");
                //24.24.24.24

                //r.ShouldNotBeNull();
                r.getregion().ShouldEqual("CA");
                //r.countryCode.ShouldEqual("AR");
                //r.countryName.ShouldEqual("Argentina");
            }
        }

        [Test]
        public void UnknownCityReturnsNullNotFailing()
        {
            string geoipDb = Path.Combine(GeoipDbPath, GeoLiteCityDb);

            using (var ls = new LookupService(geoipDb))
            {
                Location l = ls.getLocation("0.2.3.4");

                l.ShouldBeNull();
            }
        }

        [Test]
        public void KnownRegionReturnsRegionName()
        {
            string geoipDb = Path.Combine(GeoipDbPath, GeoLiteCityDb);

            using (var ls = new LookupService(geoipDb))
            {
                Location l = ls.getLocation("64.17.254.223");
                //Region r = ls.getRegion("186.33.234.28");

                l.regionName.ShouldEqual("California");
            }
        }

        [Test]
        public void UnknownRegionReturnsNullNotFailing()
        {
            string geoipDb = Path.Combine(GeoipDbPath, GeoLiteCityDb);

            var region = RegionName.getRegionName("12", "RegionName");
            
            region.ShouldBeNull();
        }




    }
}
