angular.module("Meraki.UI")
    .directive("merakiEditor", function () {
        return {
            require: "ngModel",
            scope: {
                html:"=ngModel"
            },
            templateUrl: Templates.MerakiEditor,
            link: function (s, e, a, c) {
                var text = angular.element(e).find(".text-iframe")[0];
                text.contentDocument.designMode = "on"
                c.$render = function () {
                    if (c.$modelValue == text.contentDocument.body.innerHTML) {
                        return;
                    }
                    if (!c.$modelValue && c.$modelValue !== text.contentDocument.body.innerHTML) {
                        text.contentDocument.body.innerHTML = "";
                        return;
                    }
                    text.contentDocument.body.innerHTML = c.$modelValue;
                };
                c.$parsers = [];
                c.$formatters = [];
                text.contentDocument.body.onkeydown = function () {
                    s.$apply(function () { s.html = text.contentDocument.body.innerHTML; });
                };

                s.execCommand = function ($event,com,arg) {
                    text.contentDocument.execCommand(com, false, arg);
                    s.html = text.contentDocument.body.innerHTML;
                    $event.preventDefault();
                    $event.stopPropagation();
                };
            }
        };
    });