using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace SuiviPortefeuilleRBC.Models
{
   [XmlRoot("quote"), XmlType("quote")]
   public class SimpleQuoteQueryResultModel
   {
      //<quote symbol="YHOO">
      //   <AverageDailyVolume>17089700</AverageDailyVolume>
      //   <Change>-0.22</Change>
      //   <DaysLow>32.44</DaysLow>
      //   <DaysHigh>33.08</DaysHigh>
      //   <YearLow>27.20</YearLow>
      //   <YearHigh>51.68</YearHigh>
      //   <MarketCapitalization>31.11B</MarketCapitalization>
      //   <LastTradePriceOnly>32.94</LastTradePriceOnly>
      //   <DaysRange>32.44 - 33.08</DaysRange>
      //   <Name>Yahoo! Inc.</Name>
      //   <Symbol>YHOO</Symbol>
      //   <Volume>5316055</Volume>
      //   <StockExchange>NMS</StockExchange>
      //</quote>
      [XmlAttribute("symbol")]
      public string SymbolAttr { get; set; }
      [XmlElement("AverageDailyVolume")]
      public string AverageDailyVolume { get; set; }
      [XmlElement("Change")]
      public string Change { get; set; }
      [XmlElement("DaysLow")]
      public string DaysLow { get; set; }
      [XmlElement("DaysHigh")]
      public string DaysHigh { get; set; }
      [XmlElement("YearLow")]
      public string YearLow { get; set; }
      [XmlElement("YearHigh")]
      public string YearHigh { get; set; }
      [XmlElement("MarketCapitalization")]
      public string MarketCapitalization { get; set; }
      [XmlElement("LastTradePriceOnly")]
      public string LastTradePriceOnly { get; set; }
      [XmlElement("DaysRange")]
      public string DaysRange { get; set; }
      [XmlElement("Name")]
      public string Name { get; set; }
      [XmlElement("Symbol")]
      public string Symbol { get; set; }
      [XmlElement("Volume")]
      public string Volume { get; set; }
      [XmlElement("StockExchange")]
      public string StockExchange { get; set; }
   }
}