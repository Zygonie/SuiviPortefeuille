using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SuiviPortefeuilleRBC.Models;

namespace SuiviPortefeuilleRBC.Controllers
{
   public class StockDescriptionController : Controller
   {
      private ApplicationDbContext db = new ApplicationDbContext();

      #region HTTP requests

      // GET: StockDescription
      public ActionResult Index()
      {
         return View(db.StockDescriptions.ToList());
      }

      // GET: StockDescription/Details/5
      public ActionResult Details(int? id)
      {
         if(id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         if(stockDescription == null)
         {
            return HttpNotFound();
         }
         return View(stockDescription);
      }

      // GET: StockDescription/Create
      [Authorize(Roles = "canEdit")]
      public ActionResult Create()
      {
         return View();
      }

      // POST: StockDescription/Create
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [Authorize(Roles = "canEdit")]
      [ValidateAntiForgeryToken]
      public ActionResult Create([Bind(Include = "StockDescriptionId,Name,LastPrice,ChangePercent,SpreadLowTargetPercent,SpreadHighTargetPercent,PriceTargetMin,PriceTargetMax,DividendYield,Payout,PriceEarningRatio,DividendPerShare,EarningsPerShare,EpsEstimateCurrentYear,PriceToBook,ExDividendDate,BookToValuePerShare,GrahamPrice,GrahamSpread,PtfPercent,Amount,NumberOfSharesTarget,AmountTarget")] StockDescription stockDescription)
      {
         if(ModelState.IsValid)
         {
            db.StockDescriptions.Add(stockDescription);
            db.SaveChanges();
            return RedirectToAction("Index");
         }

         return View(stockDescription);
      }

      // GET: StockDescription/Edit/5
      [Authorize(Roles = "canEdit")]
      public ActionResult Edit(int? id)
      {
         if(id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         if(stockDescription == null)
         {
            return HttpNotFound();
         }
         return View(stockDescription);
      }

      // POST: StockDescription/Edit/5
      // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
      // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [Authorize(Roles = "canEdit")]
      [ValidateAntiForgeryToken]
      public ActionResult Edit([Bind(Include = "StockDescriptionId,Name,LastPrice,ChangePercent,SpreadLowTargetPercent,SpreadHighTargetPercent,PriceTargetMin,PriceTargetMax,DividendYield,Payout,PriceEarningRatio,DividendPerShare,EarningsPerShare,EpsEstimateCurrentYear,PriceToBook,ExDividendDate,BookToValuePerShare,GrahamPrice,GrahamSpread,PtfPercent,Amount,NumberOfSharesTarget,AmountTarget")] StockDescription stockDescription)
      {
         if(ModelState.IsValid)
         {
            db.Entry(stockDescription).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
         }
         return View(stockDescription);
      }

      // GET: StockDescription/Delete/5
      [Authorize(Roles = "canEdit")]
      public ActionResult Delete(int? id)
      {
         if(id == null)
         {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
         }
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         if(stockDescription == null)
         {
            return HttpNotFound();
         }
         return View(stockDescription);
      }

      // POST: StockDescription/Delete/5
      [HttpPost, ActionName("Delete")]
      [Authorize(Roles = "canEdit")]
      [ValidateAntiForgeryToken]
      public ActionResult DeleteConfirmed(int id)
      {
         StockDescription stockDescription = db.StockDescriptions.Find(id);
         db.StockDescriptions.Remove(stockDescription);
         db.SaveChanges();
         return RedirectToAction("Index");
      }

      #endregion

      #region Help Methods

      //see https://developer.yahoo.com/yql/console/?q=show%20tables&env=store://datatables.org/alltableswithkeys to build custom YQL request

      #region Dividend history
      //Dividend History
      //select * from yahoo.finance.dividendhistory where symbol = "KO" and startDate = "1962-01-01" and endDate = "2013-12-31"
      //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.dividendhistory%20where%20symbol%20%3D%20%22KO%22%20and%20startDate%20%3D%20%221962-01-01%22%20and%20endDate%20%3D%20%222013-12-31%22&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
      #endregion
      #region Historical data
      //Historical data
      //select * from yahoo.finance.historicaldata where symbol = "YHOO" and startDate = "2009-09-11" and endDate = "2010-03-10"
      //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%20%3D%20%22YHOO%22%20and%20startDate%20%3D%20%222009-09-11%22%20and%20endDate%20%3D%20%222010-03-10%22&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
      #endregion
      #region Simple quote
      //Simple quote
      //select * from yahoo.finance.quote where symbol in ("YHOO","AAPL")
      //https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quote%20where%20symbol%20in%20(%22YHOO%22%2C%22AAPL%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
      //
      //<?xml version="1.0" encoding="UTF-8"?>
      //<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
      //    yahoo:count="4" yahoo:created="2015-11-29T22:01:23Z" yahoo:lang="en-US">
      //    <diagnostics>
      //        <url execution-start-time="0" execution-stop-time="266" execution-time="266"><![CDATA[http://www.datatables.org/yahoo/finance/quote/yahoo.finance.quote.xml]]></url>
      //        <publiclyCallable>true</publiclyCallable>
      //        <cache execution-start-time="270" execution-stop-time="270"
      //            execution-time="0" method="GET" type="MEMCACHED"><![CDATA[5d1e1de680846a307c9874dc3d6878dc]]></cache>
      //        <url execution-start-time="271" execution-stop-time="318" execution-time="47"><![CDATA[http://download.finance.yahoo.com/d/quotes.csv?f=aa2bb2b3b4cc1c3c4c6c8dd1d2ee1e7e8e9ghjkg1g3g4g5g6ii5j1j3j4j5j6k1k2k4k5ll1l2l3mm2m3m4m5m6m7m8nn4opp1p2p5p6qrr1r2r5r6r7ss1s7t1t7t8vv1v7ww1w4xy&s=YHOO,AAPL,GOOG,MSFT]]></url>
      //        <query execution-start-time="270" execution-stop-time="319"
      //            execution-time="49" params="{url=[http://download.finance.yahoo.com/d/quotes.csv?f=aa2bb2b3b4cc1c3c4c6c8dd1d2ee1e7e8e9ghjkg1g3g4g5g6ii5j1j3j4j5j6k1k2k4k5ll1l2l3mm2m3m4m5m6m7m8nn4opp1p2p5p6qrr1r2r5r6r7ss1s7t1t7t8vv1v7ww1w4xy&amp;s=YHOO,AAPL,GOOG,MSFT]}"><![CDATA[select * from csv where url=@url and columns='Ask,AverageDailyVolume,Bid,AskRealtime,BidRealtime,BookValue,Change&PercentChange,Change,Commission,Currency,ChangeRealtime,AfterHoursChangeRealtime,DividendShare,LastTradeDate,TradeDate,EarningsShare,ErrorIndicationreturnedforsymbolchangedinvalid,EPSEstimateCurrentYear,EPSEstimateNextYear,EPSEstimateNextQuarter,DaysLow,DaysHigh,YearLow,YearHigh,HoldingsGainPercent,AnnualizedGain,HoldingsGain,HoldingsGainPercentRealtime,HoldingsGainRealtime,MoreInfo,OrderBookRealtime,MarketCapitalization,MarketCapRealtime,EBITDA,ChangeFromYearLow,PercentChangeFromYearLow,LastTradeRealtimeWithTime,ChangePercentRealtime,ChangeFromYearHigh,PercebtChangeFromYearHigh,LastTradeWithTime,LastTradePriceOnly,HighLimit,LowLimit,DaysRange,DaysRangeRealtime,FiftydayMovingAverage,TwoHundreddayMovingAverage,ChangeFromTwoHundreddayMovingAverage,PercentChangeFromTwoHundreddayMovingAverage,ChangeFromFiftydayMovingAverage,PercentChangeFromFiftydayMovingAverage,Name,Notes,Open,PreviousClose,PricePaid,ChangeinPercent,PriceSales,PriceBook,ExDividendDate,PERatio,DividendPayDate,PERatioRealtime,PEGRatio,PriceEPSEstimateCurrentYear,PriceEPSEstimateNextYear,Symbol,SharesOwned,ShortRatio,LastTradeTime,TickerTrend,OneyrTargetPrice,Volume,HoldingsValue,HoldingsValueRealtime,YearRange,DaysValueChange,DaysValueChangeRealtime,StockExchange,DividendYield']]></query>
      //        <javascript execution-start-time="268" execution-stop-time="356"
      //            execution-time="87" instructions-used="193462" table-name="yahoo.finance.quote"/>
      //        <user-time>357</user-time>
      //        <service-time>313</service-time>
      //        <build-version>0.2.311</build-version>
      //    </diagnostics>
      //    <results>
      //        <quote symbol="YHOO">
      //            <AverageDailyVolume>17089700</AverageDailyVolume>
      //            <Change>-0.22</Change>
      //            <DaysLow>32.44</DaysLow>
      //            <DaysHigh>33.08</DaysHigh>
      //            <YearLow>27.20</YearLow>
      //            <YearHigh>51.68</YearHigh>
      //            <MarketCapitalization>31.11B</MarketCapitalization>
      //            <LastTradePriceOnly>32.94</LastTradePriceOnly>
      //            <DaysRange>32.44 - 33.08</DaysRange>
      //            <Name>Yahoo! Inc.</Name>
      //            <Symbol>YHOO</Symbol>
      //            <Volume>5316055</Volume>
      //            <StockExchange>NMS</StockExchange>
      //        </quote>
      //        <quote symbol="AAPL">
      //            <AverageDailyVolume>49055500</AverageDailyVolume>
      //            <Change>-0.22</Change>
      //            <DaysLow>117.60</DaysLow>
      //            <DaysHigh>118.41</DaysHigh>
      //            <YearLow>92.00</YearLow>
      //            <YearHigh>134.54</YearHigh>
      //            <MarketCapitalization>656.83B</MarketCapitalization>
      //            <LastTradePriceOnly>117.81</LastTradePriceOnly>
      //            <DaysRange>117.60 - 118.41</DaysRange>
      //            <Name>Apple Inc.</Name>
      //            <Symbol>AAPL</Symbol>
      //            <Volume>13046445</Volume>
      //            <StockExchange>NMS</StockExchange>
      //        </quote>
      //    </results>
      //</query>
      #endregion
      #region Complete quote
      //Complete quotes
      //select * from yahoo.finance.quotes where symbol in ("YHOO")
      //https://query.yahooapis.com/v1/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20(%22YHOO%22)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys
      //       <?xml version="1.0" encoding="UTF-8"?>
      //<query xmlns:yahoo="http://www.yahooapis.com/v1/base.rng"
      //    yahoo:count="1" yahoo:created="2015-11-29T22:03:04Z" yahoo:lang="en-US">
      //    <diagnostics>
      //        <url execution-start-time="0" execution-stop-time="67" execution-time="67"><![CDATA[http://www.datatables.org/yahoo/finance/yahoo.finance.quotes.xml]]></url>
      //        <publiclyCallable>true</publiclyCallable>
      //        <cache execution-start-time="70" execution-stop-time="70"
      //            execution-time="0" method="GET" type="MEMCACHED"><![CDATA[5d1e1de680846a307c9874dc3d6878dc]]></cache>
      //        <url execution-start-time="71" execution-stop-time="314" execution-time="243"><![CDATA[http://download.finance.yahoo.com/d/quotes.csv?f=aa2bb2b3b4cc1c3c4c6c8dd1d2ee1e7e8e9ghjkg1g3g4g5g6ii5j1j3j4j5j6k1k2k4k5ll1l2l3mm2m3m4m5m6m7m8nn4opp1p2p5p6qrr1r2r5r6r7ss1s7t1t7t8vv1v7ww1w4xy&s=YHOO]]></url>
      //        <query execution-start-time="70" execution-stop-time="314"
      //            execution-time="244" params="{url=[http://download.finance.yahoo.com/d/quotes.csv?f=aa2bb2b3b4cc1c3c4c6c8dd1d2ee1e7e8e9ghjkg1g3g4g5g6ii5j1j3j4j5j6k1k2k4k5ll1l2l3mm2m3m4m5m6m7m8nn4opp1p2p5p6qrr1r2r5r6r7ss1s7t1t7t8vv1v7ww1w4xy&amp;s=YHOO]}"><![CDATA[select * from csv where url=@url and columns='Ask,AverageDailyVolume,Bid,AskRealtime,BidRealtime,BookValue,Change&PercentChange,Change,Commission,Currency,ChangeRealtime,AfterHoursChangeRealtime,DividendShare,LastTradeDate,TradeDate,EarningsShare,ErrorIndicationreturnedforsymbolchangedinvalid,EPSEstimateCurrentYear,EPSEstimateNextYear,EPSEstimateNextQuarter,DaysLow,DaysHigh,YearLow,YearHigh,HoldingsGainPercent,AnnualizedGain,HoldingsGain,HoldingsGainPercentRealtime,HoldingsGainRealtime,MoreInfo,OrderBookRealtime,MarketCapitalization,MarketCapRealtime,EBITDA,ChangeFromYearLow,PercentChangeFromYearLow,LastTradeRealtimeWithTime,ChangePercentRealtime,ChangeFromYearHigh,PercebtChangeFromYearHigh,LastTradeWithTime,LastTradePriceOnly,HighLimit,LowLimit,DaysRange,DaysRangeRealtime,FiftydayMovingAverage,TwoHundreddayMovingAverage,ChangeFromTwoHundreddayMovingAverage,PercentChangeFromTwoHundreddayMovingAverage,ChangeFromFiftydayMovingAverage,PercentChangeFromFiftydayMovingAverage,Name,Notes,Open,PreviousClose,PricePaid,ChangeinPercent,PriceSales,PriceBook,ExDividendDate,PERatio,DividendPayDate,PERatioRealtime,PEGRatio,PriceEPSEstimateCurrentYear,PriceEPSEstimateNextYear,Symbol,SharesOwned,ShortRatio,LastTradeTime,TickerTrend,OneyrTargetPrice,Volume,HoldingsValue,HoldingsValueRealtime,YearRange,DaysValueChange,DaysValueChangeRealtime,StockExchange,DividendYield']]></query>
      //        <javascript execution-start-time="69" execution-stop-time="322"
      //            execution-time="253" instructions-used="63778" table-name="yahoo.finance.quotes"/>
      //        <user-time>323</user-time>
      //        <service-time>310</service-time>
      //        <build-version>0.2.311</build-version>
      //    </diagnostics>
      //    <results>
      //        <quote symbol="YHOO">
      //            <Ask>33.14</Ask>
      //            <AverageDailyVolume>17089700</AverageDailyVolume>
      //            <Bid/>
      //            <AskRealtime/>
      //            <BidRealtime/>
      //            <BookValue>29.94</BookValue>
      //            <Change_PercentChange>-0.22 - -0.66%</Change_PercentChange>
      //            <Change>-0.22</Change>
      //            <Commission/>
      //            <Currency>USD</Currency>
      //            <ChangeRealtime/>
      //            <AfterHoursChangeRealtime/>
      //            <DividendShare/>
      //            <LastTradeDate>11/27/2015</LastTradeDate>
      //            <TradeDate/>
      //            <EarningsShare>0.25</EarningsShare>
      //            <ErrorIndicationreturnedforsymbolchangedinvalid/>
      //            <EPSEstimateCurrentYear>0.59</EPSEstimateCurrentYear>
      //            <EPSEstimateNextYear>0.57</EPSEstimateNextYear>
      //            <EPSEstimateNextQuarter>0.14</EPSEstimateNextQuarter>
      //            <DaysLow>32.44</DaysLow>
      //            <DaysHigh>33.08</DaysHigh>
      //            <YearLow>27.20</YearLow>
      //            <YearHigh>51.68</YearHigh>
      //            <HoldingsGainPercent/>
      //            <AnnualizedGain/>
      //            <HoldingsGain/>
      //            <HoldingsGainPercentRealtime/>
      //            <HoldingsGainRealtime/>
      //            <MoreInfo/>
      //            <OrderBookRealtime/>
      //            <MarketCapitalization>31.11B</MarketCapitalization>
      //            <MarketCapRealtime/>
      //            <EBITDA>477.08M</EBITDA>
      //            <ChangeFromYearLow>5.74</ChangeFromYearLow>
      //            <PercentChangeFromYearLow>+21.10%</PercentChangeFromYearLow>
      //            <LastTradeRealtimeWithTime/>
      //            <ChangePercentRealtime/>
      //            <ChangeFromYearHigh>-18.74</ChangeFromYearHigh>
      //            <PercebtChangeFromYearHigh>-36.26%</PercebtChangeFromYearHigh>
      //            <LastTradeWithTime>1:00pm - &lt;b&gt;32.94&lt;/b&gt;</LastTradeWithTime>
      //            <LastTradePriceOnly>32.94</LastTradePriceOnly>
      //            <HighLimit/>
      //            <LowLimit/>
      //            <DaysRange>32.44 - 33.08</DaysRange>
      //            <DaysRangeRealtime/>
      //            <FiftydayMovingAverage>33.44</FiftydayMovingAverage>
      //            <TwoHundreddayMovingAverage>36.07</TwoHundreddayMovingAverage>
      //            <ChangeFromTwoHundreddayMovingAverage>-3.13</ChangeFromTwoHundreddayMovingAverage>
      //            <PercentChangeFromTwoHundreddayMovingAverage>-8.68%</PercentChangeFromTwoHundreddayMovingAverage>
      //            <ChangeFromFiftydayMovingAverage>-0.50</ChangeFromFiftydayMovingAverage>
      //            <PercentChangeFromFiftydayMovingAverage>-1.49%</PercentChangeFromFiftydayMovingAverage>
      //            <Name>Yahoo! Inc.</Name>
      //            <Notes/>
      //            <Open>32.79</Open>
      //            <PreviousClose>33.16</PreviousClose>
      //            <PricePaid/>
      //            <ChangeinPercent>-0.66%</ChangeinPercent>
      //            <PriceSales>6.33</PriceSales>
      //            <PriceBook>1.11</PriceBook>
      //            <ExDividendDate/>
      //            <PERatio>131.76</PERatio>
      //            <DividendPayDate/>
      //            <PERatioRealtime/>
      //            <PEGRatio>-1.77</PEGRatio>
      //            <PriceEPSEstimateCurrentYear>55.83</PriceEPSEstimateCurrentYear>
      //            <PriceEPSEstimateNextYear>57.79</PriceEPSEstimateNextYear>
      //            <Symbol>YHOO</Symbol>
      //            <SharesOwned/>
      //            <ShortRatio>2.84</ShortRatio>
      //            <LastTradeTime>1:00pm</LastTradeTime>
      //            <TickerTrend/>
      //            <OneyrTargetPrice>41.86</OneyrTargetPrice>
      //            <Volume>5316055</Volume>
      //            <HoldingsValue/>
      //            <HoldingsValueRealtime/>
      //            <YearRange>27.20 - 51.68</YearRange>
      //            <DaysValueChange/>
      //            <DaysValueChangeRealtime/>
      //            <StockExchange>NMS</StockExchange>
      //            <DividendYield/>
      //            <PercentChange>-0.66%</PercentChange>
      //        </quote>
      //    </results>
      //</query>
      #endregion

      #endregion

      protected override void Dispose(bool disposing)
      {
         if(disposing)
         {
            db.Dispose();
         }
         base.Dispose(disposing);
      }
   }
}
