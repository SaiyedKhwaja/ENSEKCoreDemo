var ViewManager = ViewManager || new _ViewManager();
function _ViewManager() {
    var _instance = this;

    // List to store all registered view objects.
    // { elementId, sharedProperties, onDispose }
    _instance.views = [];

    // options: { elementId, onInit(sharedProperties), onDispose(sharedProperties) }
    _instance.registerView = function (options) {
        if (!options) {
            ErrorLog.log('ViewManager.registerView Error', 'Options not specified');
            return;
        }
        if (!options.elementId) {
            ErrorLog.log('ViewManager.registerView Error', 'Options missing required elementId property');
            return;
        }
        if (!options.onInit) {
            ErrorLog.log('ViewManager.registerView Error', 'Options missing required onInit property');
            return;
        }
        if (!options.onDispose) {
            ErrorLog.log('ViewManager.registerView Error', 'Options missing required onDispose property');
            return;
        }

        var view = Object.create(null);
        view.elementId = options.elementId;
        view.sharedProperties = Object.create(null);
        view.onDispose = options.onDispose;

        options.onInit(view.sharedProperties);

        _instance.views.push(view);

        var $scriptElement = $('#script_' + view.elementId);
        $scriptElement.remove();
    };

    // Removes view element from the DOM and disposes script data.
    _instance.removeView = function (elementId, options, callback) {
        if (!elementId) {
            return false;
        }

        if (!options) {
            options = {
                showVisualDelete: false
            };
        }

        // Find the view with matching elementId.
        for (var i = 0; i < _instance.views.length; i++) {
            var view = _instance.views[i];
            if (view.elementId === elementId) {
                // Call the disposed function for the view, passing sharedProperties to use.
                view.onDispose(view.sharedProperties);

                var $viewElement = $('#' + view.elementId);

                // Remove the view element from the DOM (this will clear any events registered to any child elements).
                if (options.showVisualDelete) {
                    var $deleteElement = $('<div/>', { class: 'view-entry-deleted' });
                    $viewElement.css('position', 'relative');
                    $viewElement.append($deleteElement);
                    $viewElement.slideUp(500);
                    $deleteElement.fadeOut(1000, function () {
                        $viewElement.remove(); if (typeof options.callback !== typeof undefined)
                            options.callback(); });
                } else {
                    $viewElement.remove();
                }

                // Remove the view from the list  of tracked views.
                _instance.views.splice(i, 1);

               

                return true;
            }
        }

        return false;
    };
}