angular.module("Meraki.UI")
    .directive("merakiFileInput", ["$mdDialog", function ($mdDialog) {
        return {
            restrict: "A",
            require: "ngModel",
            scope: {
                files: "=ngModel",
                mkChange:"&"
            },
            link: function (s, e, a, c) {
                e.on("change", function () {
                    s.$apply(function () {
                        c.$setViewValue(e[0].files);
                        if (typeof s.mkChange !== "undefined") {
                            s.mkChange();
                        }
                    });
                });
            }
        }

    }]);