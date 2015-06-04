$.extend($.fn.datagrid.defaults.editors,
    {
        workingcheckbox:
            {
                init: function (container, options) {
                    var input = $('<input type="checkbox">').appendTo(container);
                    return input;
                },
                getValue: function (target) {
                    return $(target).prop('checked');
                },
                setValue: function (target, value) {
                    $(target).prop('checked', value);
                }
            },
        workingtextarea: {
            init: function (container, options) {
                var input = $('<textarea class="datagrid-editable-input" ></textarea>').appendTo(container);
                if (options.rows) {
                    $(input).attr('rows', options.rows);
                }
                if (options.cols) {
                    $(input).attr('cols', options.cols);
                }
                return input;
            },
            getValue: function (target) {
                return $(target).val();
            },
            setValue: function (target, value) {
                $(target).val(value);
            },
            resize: function (target, width) {
                $(target)._outerWidth(width);
            }
        },
        timespinner: {
            init: function (container, options) {
                var input = $('<input>').appendTo(container);
                input.timespinner(options);
                return input;
            },
            destroy: function (target) {
                $(target).timespinner('destroy');
            },
            getValue: function (target) {
                return $(target).timespinner('getValue');
            },
            setValue: function (target, value) {
                $(target).timespinner('setValue', value);
            },
            resize: function (target, width) {
                $(target).timespinner('resize', width);
            }
        }
    });