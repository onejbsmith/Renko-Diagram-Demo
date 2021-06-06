using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RenkoChartDemo.Models;
using RenkoChartDemo.Data;
using Highcharts.RenkoDiagram;
using Microsoft.AspNetCore.Components;
using MatBlazor;

namespace RenkoChartDemo.Pages
{
    public partial class FetchCandleData
    {
        [Inject] public CandlesService candlesService { get; set; }
        [Inject]  IMatDialogService MatDialogService { get; set; }
        /// So we can exec methods
        protected RenkoDiagram renkoDiagram = new RenkoDiagram();


        /// Renko Settings
        private DateTime _chartDate = new DateTime(2021, 6, 4);

        public DateTime chartDate
        {
            get { return _chartDate; }
            set { _chartDate = value;
                refreshCandles();
            }
        }

        private string chartTitle = "QQQ";
        private string xFieldName = "RunDateTime";
        private string yFieldName = "ClosePrice";
        private decimal barSize = 0.09m;
        private string height = "600px";

        /// The Data being passed to the Renko
        private List<Candle> candles = new List<Candle>();
        private string serializedCandles;

        bool settingsDialogIsOpen = false;
        bool dataDialogIsOpen = false;

        void OpenSettingsDialog()
        {
            settingsDialogIsOpen = true;
        }

        void OpenDataDialog()
        {
            dataDialogIsOpen = true;
        }

        protected override async Task OnInitializedAsync()
        {
            await refreshCandlesAsync();
        }

        private async Task refreshCandles()
        {
            await refreshCandlesAsync();
            await renkoDiagram.createRenko(serializedCandles);
            StateHasChanged();

        }

        private async Task refreshCandlesAsync()
        {
            candles = await CandlesService.GetLastCandlesText("QQQ", chartDate);

            serializedCandles = System.Text.Json.JsonSerializer.Serialize(candles);

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
