using System;
using System.IO;
public class Region{
  internal String countryCode;
  internal String countryName;
  internal String region;
  public Region(){
  }
  public Region(String countryCode,String countryName,String region){
      this.countryCode = countryCode;
      this.countryName = countryName;
      this.region = region;
  }
  public String getcountryCode() {
      return countryCode;
  }
  public String getcountryName() {
      return countryName;
  }
  public String getregion() {
      return region;
  }
}

