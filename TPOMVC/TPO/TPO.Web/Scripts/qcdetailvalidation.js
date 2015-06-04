$(document).ready(function () { 
    $("#inColorCoA").keyup(function () {
        highlightValue("inColorCoA", "ColorTestMinimum", "ColorTestMaximum");
        highlightAll();
    });
    $("#inColorFS").keyup(function () {
        highlightValue("inColorFS", "ColorTestMinimum", "ColorTestMaximum");
        highlightAll();
    });

    $("#inMFCoA").keyup(function () {
        highlightValue("inMFCoA", "MeltFlowTestMinimum", "MeltFlowTestMaximum");
        highlightAll();
    });
    $("#inMFFS").keyup(function () {
        highlightValue("inMFFS", "MeltFlowTestMinimum", "MeltFlowTestMaximum");
        highlightAll();
    });

    $("#inACCoA").keyup(function () {
        highlightValue("inACCoA", "AshContentTestMinimum", "AshContentTestMaximum");
        highlightAll();
    });
    $("#inACFS").keyup(function () {
        highlightValue("inACFS", "AshContentTestMinimum", "AshContentTestMaximum");
        highlightAll();
    });

    $("#inMoistCoA").keyup(function () {
        highlightValue("inMoistCoA", "MoistureTestMinimum", "MoistureTestMaximum");
        highlightAll();
    });
    $("#inMoistFS").keyup(function () {
        highlightValue("inMoistFS", "MoistureTestMinimum", "MoistureTestMaximum");
        highlightAll();
    });

    $("#inCBCoA").keyup(function () {
        highlightValue("inCBCoA", "CarbonBlackTestMinimum", "CarbonBlackTestMaximum");
        highlightAll();
    });
    $("#inCBFS").keyup(function () {
        highlightValue("inCBFS", "CarbonBlackTestMinimum", "CarbonBlackTestMaximum");
        highlightAll();
    });

    highlightAll();
});

var _isValid = true;
function highlightAll() {
    _isValid = true;
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
        //$('#saveButton').disabled = !_isValid;
        //document.getElementById("saveButton").disabled = !_isValid;
    }

function highlightValue(current, min, max) {
    var currentValue = $("#" + current).val();
    var minValue = $("#" + min).val();
    var maxValue = $("#" + max).val();
    if (currentValue < minValue || currentValue > maxValue)
    {
        $("#" + current).css("border-color", "red");
        _isValid = false;
    }
    else
    {
        $("#" + current).css("border-color", "");
    }
}