using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.JSInterop;

namespace Highcharts.RenkoDiagram
{
    public partial class RenkoDiagram
    {
        [Inject]
        public IJSRuntime jsRuntime { get; set; }
        private IJSObjectReference _jsModule;

        [Parameter]
        public string chartTitle { get; set; }

        [Parameter]
        public string jsonSerializedData { get; set; }

        [Parameter]
        public string xValueFieldName { get; set; }

        [Parameter]
        public string yValueFieldName { get; set; }

        [Parameter]
        public decimal brickSize { get; set; }

        [Parameter]
        public string chartHeight { get; set; }

        //protected override async Task OnInitializedAsync()
        //{

        //}

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Highcharts.RenkoDiagram/jquery-1.12.4.js");
            await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Highcharts.RenkoDiagram//highstock.js");
            await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Highcharts.RenkoDiagram/highcharts-more.js");

            _jsModule = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Highcharts.RenkoDiagram/RenkoDiagram.js");
            await createRenko(jsonSerializedData);


            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task createRenko(string jsonSerializedData)
        {
            await _jsModule.InvokeVoidAsync(
                "createRenko",
                new
                {
                    title = chartTitle,
                    dataArray = jsonSerializedData,
                    xDateTimeFieldName = xValueFieldName,
                    yFieldName = yValueFieldName,
                    brickSize = brickSize,
                    height = chartHeight

                });
        }
    }
}
