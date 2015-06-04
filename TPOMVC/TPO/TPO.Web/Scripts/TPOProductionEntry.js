function FetchProductionMetrics() {

    var prodLineId = $("#ProductionLineId").val();
    var shiftId = $("#ShiftId").val();
    var workOrderId = $("#WorkOrderId").val();
    var prodDate = $("#ProductionDate").val();

    if (prodLineId > 0 && shiftId > 0 && workOrderId > 0 && prodDate != undefined) {
        var data = {
            productionLineId: prodLineId,
            shiftId: shiftId,
            workOrderId: workOrderId,
            productionDate: prodDate
        }
        var options = {
            url: '/TPOProductionEntry/FetchProductionMetrics',
            type: 'get',
            data: data
        }

        $.ajax(options)
            .success(function (data, textStatus, jqXHR) {

            })
            .error(function (jqXHR, textStatus, errorThrown) {
                displayMessage('@TPO.Common.Resources.General.ResponseTypeError', 'Error: ' + errorThrown + ' Status: ' + jqXHR.status);
            });
    }
}