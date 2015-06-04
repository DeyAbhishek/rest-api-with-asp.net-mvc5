$(document).ready(function () { 
    $("#inColorCoA").keyup(function () {
        highlightValue("inColorCoA", "ColorTestMinimum", "ColorTestMaximum");
    });
    $("#inColorFS").keyup(function () {
        highlightValue("inColorFS", "ColorTestMinimum", "ColorTestMaximum");
    });

    $("#inMFCoA").keyup(function () {
        highlightValue("inMFCoA", "MeltFlowTestMinimum", "MeltFlowTestMaximum");
    });
    $("#inMFFS").keyup(function () {
        highlightValue("inMFFS", "MeltFlowTestMinimum", "MeltFlowTestMaximum");
    });

    $("#inACCoA").keyup(function () {
        highlightValue("inACCoA", "AshContentTestMinimum", "AshContentTestMaximum");
    });
    $("#inACFS").keyup(function () {
        highlightValue("inACFS", "AshContentTestMinimum", "AshContentTestMaximum");
    });

    $("#inMoistCoA").keyup(function () {
        highlightValue("inMoistCoA", "MoistureTestMinimum", "MoistureTestMaximum");
    });
    $("#inMoistFS").keyup(function () {
        highlightValue("inMoistFS", "MoistureTestMinimum", "MoistureTestMaximum");
    });

    $("#inCBCoA").keyup(function () {
        highlightValue("inCBCoA", "CarbonBlackTestMinimum", "CarbonBlackTestMaximum");
    });
    $("#inCBFS").keyup(function () {
        highlightValue("inCBFS", "CarbonBlackTestMinimum", "CarbonBlackTestMaximum");
    });

    highlightAll();
});

function highlightAll()
{
        highlightValue("inColorCoA", "ColorTestMinimum", "ColorTestMaximum");
        highlightValue("inColorFS", "ColorTestMinimum", "ColorTestMaximum");
        highlightValue("inMFCoA", "MeltFlowTestMinimum", "MeltFlowTestMaximum");
        highlightValue("inMFFS", "MeltFlowTestMinimum", "MeltFlowTestMaximum");
        highlightValue("inACCoA", "AshContentTestMinimum", "AshContentTestMaximum");
        highlightValue("inACFS", "AshContentTestMinimum", "AshContentTestMaximum");
        highlightValue("inMoistCoA", "MoistureTestMinimum", "MoistureTestMaximum");
        highlightValue("inMoistFS", "MoistureTestMinimum", "MoistureTestMaximum");
        highlightValue("inCBCoA", "CarbonBlackTestMinimum", "CarbonBlackTestMaximum");
        highlightValue("inCBFS", "CarbonBlackTestMinimum", "CarbonBlackTestMaximum");
}

function highlightValue(current, min, max)
{   
    var currentValue = $("#" + current).val();
    var minValue = $("#" + min).val();
    var maxValue = $("#" + max).val();
    if (currentValue < minValue || currentValue > maxValue)
    {
        $("#" + current).css("border-color", "red");
        $("#" + current + "_val").show();
        if (currentValue < minValue) {
            $("#" + current + "_val").text("Value is less than the minimum.");
        } else
        {
            $("#" + current + "_val").text("Value is greater than the maximum.");
        }
    }
    else
    {
        $("#" + current).css("border-color", "");
        $("#" + current + "_val").hide();
    }
}