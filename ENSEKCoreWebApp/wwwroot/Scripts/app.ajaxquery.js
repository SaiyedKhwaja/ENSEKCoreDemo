var AjaxQuery = AjaxQuery || new _AjaxQuery();
function _AjaxQuery() {
    var _instance = this;

    function onUnauthenticated(func, options) {
        LoginDialog.show({
            onLogin: function () {
                AjaxQuery[func](options);
            }
        });
    }

    function call(method, options) {
        if (!options.error) {
            options.error = 'An unexpected error has occurred during the request.';
        }

        return $.ajax({
            method: method,
            url: options.url,
            data: options.data,
            async: options.async
        }).done(function (data) {
            if (data && data.IsUnauthenticated) {
                onUnauthenticated(method, options);
            }
            else {
                if (options.onDone) {
                    options.onDone(data);
                }
            }
        }).fail(function (jqxhr, textStatus, error) {
            if (jqxhr.status === 401) {
                onUnauthenticated(method, options);
            }
            else if (error === 'abort') {
                //call aborted
            }
            else {
                var err = textStatus + ", " + error;
                ErrorLog.log(err, options.error + ' URL: ' + options.url);

                if (options.onFail) {
                    options.onFail(jqxhr, textStatus, error);
                }
            }
        }).always(function () {

        });
    }

    _AjaxQuery.prototype.get = function (options) {
        return call('GET', options);
    };

    _AjaxQuery.prototype.post = function (options) {
        call('POST', options);
    };

    _AjaxQuery.prototype.delete = function (options) {
        call('DELETE', options);
    };
}