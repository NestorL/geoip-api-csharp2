//an example of how to lookup the database info
using System;
using System.IO;
using geoip_api_csharp2.examples.Properties;
class infoExample
{
    public static void Run(String[] args)
    {
        string GeoipDbPath = Settings.Default.GeoipDbPath;
        string GeoipDb = GeoipDbPath + "GeoIPCity.dat";
        if (args.Length > 0)
        {
            GeoipDb = args[0];
        }
        //open the database
        LookupService ls = new LookupService(GeoipDb, LookupService.GEOIP_STANDARD);
        //get the database info
        DatabaseInfo dbinfo = ls.getDatabaseInfo();
        Console.Write(" dbinfo string: " + dbinfo.toString() + "\n");
        Console.Write(" dbinfo type: " + dbinfo.getType() + "\n");
        Console.Write(" dbinfo date: " + dbinfo.getDate() + "\n");
    }
}
