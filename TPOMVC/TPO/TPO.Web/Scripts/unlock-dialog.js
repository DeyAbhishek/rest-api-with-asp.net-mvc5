var _successCallback;

function createUnlockDialog(successCallback)
{
    _successCallback = successCallback;
    var dialog = $("<div></div>").dialog(
        {
            title: "Enter password to unlock fields",
            autoOpen: false,
            resizable: false,
            height: 300,
            width: 400,
            modal: true,
            draggable: true,
            open: function (event, ui)
            {
                $(".ui-dialog-titlebar-close").hide();
                $(this).load("/Security/FormUnlock");
            },
            buttons:
                {
                    "OK": submitUnlock,
                    "Cancel": function ()
                    {
                        $(this).dialog("close");
                    }
                }
        });
    return dialog;
}

function submitUnlock()
{
    var password = $("#Password").val();
    var model =
        {
            "Password": password
        }
    $.ajax(
        {
            url: "/Security/FormUnlock",
            async: false,
            type: 'post',
            data: model,
            success: function (data)
            {
                if (data.TriedUnlock && !data.Unlocked)
                {
                    $("#frmUnlockWarning").text("Password incorrect.");
                } else if (data.TriedUnlock && data.Unlocked)
                {
                    if (_successCallback) { _successCallback(data); }
                }
            },
            error: function(jqXHR, textStatus, error)
            {
                $("#frmUnlockWarning").text(error);
            }
        });
}
