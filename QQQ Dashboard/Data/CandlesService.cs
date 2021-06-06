using RenkoChartDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RenkoChartDemo.Data
{
    public class CandlesService
    {

        public static async Task<List<Candle>> GetLastCandlesText(string symbol, DateTime runDate)
        {
            var txt = await System.IO.File.ReadAllTextAsync($"wwwroot/sample data/{runDate.ToString("yyyy-MM-dd")}.json");
            var candles =  System.Text.Json.JsonSerializer.Deserialize<List<Candle>>(txt);
            return candles.Where(candle => candle.RunDateTime.TimeOfDay > new TimeSpan(9, 29, 00)
            &&  candle.RunDateTime.TimeOfDay < new TimeSpan(16, 00, 00)).ToList();
        }

    }
}
