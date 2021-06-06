
export function createRenko(inputData) {

    var renkoTitle = inputData.title;

    var jsonArray = JSON.parse(inputData.dataArray);

    if (jsonArray==null ||  jsonArray.length == 0) return;

    var firstValue = jsonArray[0];

    var firstDate = firstValue[inputData.xDateTimeFieldName];
    var date = new Date(firstDate);
    var localOffset = date.getTimezoneOffset() * 60000;
    var timingOffset = 0;

    /// Create a 1-d array of x=epochtime and y=price elements
    /// using xDateTimeFieldName and yFieldName to parse data from dataArray element
    var data = [];
    for (var i = 0; i < jsonArray.length; i++) {

        var currentValue = jsonArray[i];
        var epochTime = new Date(currentValue[inputData.xDateTimeFieldName]).getTime() - localOffset + timingOffset;

        data.push([
            epochTime, // the date
            currentValue[inputData.yFieldName], // the value
        ]);
    }

    Highcharts.stockChart('renkoDiagramContainer', {

        chart:
        {
            height: inputData.height
        },

        plotOptions: {
            series: {
                animation: false
            }
        },

        rangeSelector: {
            enabled:false,
            allButtonsEnabled: false,
            selected: 1
        },

        title: {
            text: renkoTitle
        },

        subtitle: {
            text: inputData.yFieldName + ` (${inputData.brickSize})`
        },

        series: [{
            name: inputData.yFieldName,
            type: 'columnrange',
            fillColor: 'forestgreen',
            turboThreshold: 0,
            groupPadding: 0,
            pointPadding: 0,
            borderWidth: 0,
            data: linearDataToRenko(data, inputData.brickSize), // change "1" to check smaller steps. 
            dataGrouping: {
                enabled: false
            },
            tooltip: {
                valueDecimals: 2
            }
        }]
    });
}

function linearDataToRenko(data, change) {
    var renkoData = [],
        prevPrice = data[0][1],
        prevTrend = 0, // 0 - no trend, 1 - uptrend, 2 - downtrend
        length = data.length,
        currentPrice,
        i = 1,
        j;
    var markAbove = prevPrice + change,
        markBelow = prevPrice - change;


    for (; i < length; i++) {
        currentPrice = data[i][1];

        if (currentPrice > markAbove) {
            // Up trend

            if (prevTrend === 2) {
                prevPrice += change;
            }

            for (j = 0; j < (currentPrice - prevPrice) / change; j++) {
                renkoData.push({
                    x: data[i][0] + j,
                    y: prevPrice,
                    low: prevPrice,
                    high: prevPrice + change,
                    color: 'green'
                });
                prevPrice += change;
            }

            prevTrend = 1;

            markAbove = prevPrice + change,
                markBelow = prevPrice - change;

        } else if (currentPrice < markBelow && currentPrice != null) {

            if (prevTrend === 1) {
                prevPrice -= change;
            }
            // Down trend
            for (j = 0; j < (prevPrice - currentPrice) / change; j++) {
                renkoData.push({
                    x: data[i][0] + j,
                    y: prevPrice,
                    low: prevPrice - change,
                    high: prevPrice,
                    color: 'red'
                });
                prevPrice -= change;
            }

            prevTrend = 2;

            markAbove = prevPrice + change,
                markBelow = prevPrice - change;
        }

    }
    //debugger;

    renkoData.push({
        x: data[data.length - 1][0],
        y: currentPrice,
        low: Math.min(currentPrice, prevPrice),
        high: Math.max(currentPrice, prevPrice),
        color: currentPrice < prevPrice ? 'red' : 'forestgreen'

    });

    return renkoData;
}

function looseJsonParse(n) {
    try {
        return Function('"use strict";return (' + n + ")")()
    }
    catch (err) {
        //alert(n);
        //debugger;
    }
}