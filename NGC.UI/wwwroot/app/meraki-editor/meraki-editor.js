angular.module("Meraki.UI")
    .directive("merakiEditor",["$mdDialog", function ($mdDialog) {
        return {
            require: "ngModel",
            scope: {
                html:"=ngModel"
            },
            templateUrl: Templates.MerakiEditor,
            link: function (s, e, a, c) {
                var fileInput = e.find("#fileInput")[0];
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
                    if ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
                    }
                };
                s.image = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    var imageDialog = $mdDialog.show({
                        contentElement: "#imageDialog"
                    });
                };
                s.closeDialog = function () {
                    $mdDialog.hide();
                };
                s.insertImage = function (img) {
                    s.execCommand(null, "insertImage", img);
                    $mdDialog.hide();
                };
                s.openFileDialog = function () {
                    fileInput.onchange = function () {
                        if (fileInput.files.length) {
                            var fileReader = new FileReader();
                            fileReader.onload = function () {
                                var image = new Image();
                                s.insertImage(this.result);
                                fileInput.onload = null;
                                angular.element(fileInput).val("");
                            };
                            fileReader.readAsDataURL(fileInput.files[0])
                        }
                    };
                    angular.element(fileInput).trigger("click");
                }
            }
        };
    }]);