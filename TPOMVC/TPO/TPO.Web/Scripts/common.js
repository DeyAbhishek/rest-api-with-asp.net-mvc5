function formatDate(val, row) {
    if (val != undefined) {
        return $.datepicker.formatDate('mm/dd/yy', new Date(parseInt(val.substr(6))));
    } else {
        return new Date();
    }
}

function formatNumberAsCommaSeparatedInt(value, row, index) {
    if (value == null) return value;
    value = Math.round(value);
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function GetMessage(type, result) {
    GetMessage(type, result, null);
}
function GetMessage(type, result, custom) {
    $.ajax({
        url: '../Message/GetMessage',
        type: 'GET',
        data: { message: type, result: result },
        success: function (data, textStatus, jqXHR) {
            if (result) {
                displayMessage(data[1], data[0]);
            } else {
                displayMessage(data[1], custom);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            displayMessage(data[1], custom);
        }
    });
};

function displayMessage(alertMessage, message) {
    $("#alertDIV").css("display", "block").addClass(alertMessage);
    $("#alertP").css("display", "block").text(message);

    $("#alertDIV").fadeOut(10000);
    $("#alertP").fadeOut(10000);

    setTimeout(function () {
        $("#alertDIV").css("display", "none").removeClass(alertMessage);
        $("#alertP").css("display", "none");
    }, 8000);
}

// Loading dialog
function showLoadingDialog()
{
    $('<div id="loadingDialog" ><img src="/Content/images/ajax-loader.gif" alt="Loading..." style="text-align: center;" /></div>').dialog(
        {
            autoOpen: true,
            modal: true,
            title: 'Loading',
            resizable: false,
            draggable: false,
            closable: false,
            noheader: true
        });
}
function closeLoadingDialog()
{
    $("#loadingDialog").dialog("close");
}

//Grid functions
function getRowIndex(target) {
    var tr = $(target).closest('tr.datagrid-row');
    var index = parseInt(tr.attr('datagrid-row-index'));
    return index;
}
function updateActions(gridID, index) {
    $('#' + gridID).datagrid('updateRow', {
        index: index,
        row: {}
    });
}
function editRow(gridID, target) {
    $("#" + gridID).datagrid('beginEdit', getRowIndex(target));
}
function cancelEdit(gridID, target) {
    $("#" + gridID).datagrid('cancelEdit', getRowIndex(target));
    $("#" + gridID).datagrid('rejectChanges');
}
function endEdit(gridID, target) {
    $("#" + gridID).datagrid('endEdit', getRowIndex(target));
}
function validateRow(gridID, target) {
    return $("#" + gridID).datagrid('validateRow', getRowIndex(target));
}
function showColumn(gridID, fieldName)
{
    $("#" + gridID).datagrid('showColumn', fieldName);
}
function hideColumn(gridID, fieldName)
{
    $("#" + gridID).datagrid('hideColumn', fieldName);
}