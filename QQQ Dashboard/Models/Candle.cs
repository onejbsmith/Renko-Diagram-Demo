using System;
using System.Collections.Generic;

#nullable disable

namespace RenkoChartDemo.Models
{
    public partial class Candle
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public DateTime RunDateTime { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public int Volume { get; set; }
        public int? BuyVolume { get; set; }
        public int? SellVolume { get; set; }
        public double? Value1 { get; set; }
        public double? Value2 { get; set; }
        public double? Value3 { get; set; }
        public double? BuyVolStdDev { get; set; }
        public double? SellVolStdDev { get; set; }
        public double? BuysOverAskVolume { get; set; }
        public double? SellsUnderBidVolume { get; set; }
        public decimal? BidPrice { get; set; }
        public decimal? AskPrice { get; set; }
    }
}
