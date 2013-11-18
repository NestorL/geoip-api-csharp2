//an example of how to lookup the isp or org of a ip address
using System;
using System.IO;
class ispExample
{
    public static void Run(String[] args)
    {
        //open the database
        LookupService ls = new LookupService("/usr/local/share/GeoIP/GeoIPISP.dat", LookupService.GEOIP_STANDARD);
        //get org of the ip address
        String orgorisp = ls.getOrg("24.24.24.24");
        Console.Write(" isp: " + orgorisp + "\n");
    }
}
