angular.module("Meraki.UI")
    .directive("merakiEditor",["$mdDialog","$http", function ($mdDialog,$http) {
        return {
            require: "ngModel",
            scope: {
                html:"=ngModel"
            },
            templateUrl: Templates.MerakiEditor,
            link: function (s, e, a, c) {
                $http({
                    url: ApiRoutes.ComboMerakiImages
                }).then(function (response) {
                    s.textImages = response.data;
                    }, function (error) {

                });
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
                angular.element(text.contentDocument.body).css({
                    "transform-origin": "top left",
                    margin: 0,
                    background: "#FFFFFF"
                });
                angular.element(text.contentDocument.body.parentElement).css({
                    margin: 0,
                    background: "#F1F1F1"
                });
                var scale = 1;
                angular.element(text.contentDocument).on("mousewheel","body", function (evt) {
                    if (evt.ctrlKey) {
                        if (evt.originalEvent.wheelDelta < 0) {
                            if (scale > 0.11) {
                                scale -= 0.1;
                            }
                        } else {
                            scale += 0.1;
                        }
                        angular.element(text.contentDocument.body).css("transform", "matrix(" + scale + ",0,0," + scale + ",0,0)");
                        evt.preventDefault();
                        evt.stopPropagation();
                    }
                });
                c.$parsers = [];
                c.$formatters = [];
                text.contentDocument.body.onkeydown = function () {
                    s.$apply(function () { s.html = text.contentDocument.body.innerHTML; });
                };
                function getSelectedNodes() {
                    var selectedNodes = [];
                    var selection = text.contentDocument.getSelection();
                    if (selection.rangeCount > 0) {
                        var range = selection.getRangeAt(0);
                        var allElements = range.commonAncestorContainer.getElementsByTagName("*");
                        for (var i = 0; i < allElements.length; i++) {
                            if (selection.containsNode(allElements[i])) {
                                selectedNodes.push(allElements[i]);
                            }
                        }
                    }
                    return selectedNodes;
                }
                function getFontSizes(cb) {
                    var sel = text.contentDocument.getSelection();
                    if (sel.rangeCount > 0) {
                        var r = sel.getRangeAt(0);
                        walk(selection, range.startContainer, range.endContainer, function (sel, currEl, currVal, next) {
                            debugger;
                        }, cb);
                    }
                    cb();
                }
                function walk(selection,from, to, next,cb,currValue) {
                    if (from === to) {
                        next(selection,from, currValue, function (value) {
                            cb(value);
                        });
                    } else {
                        from = nextNode(from);
                        next(selection,from, currValue, function (value) {
                            walk(selection, from, to, next, cb, currValue);
                        });
                    }
                }
                function nextNode(node) {
                    if (node.hasChildNodes()) {
                        return node.firstChild;
                    } else {
                        while (node && !node.nextSibling) {
                            node = node.parentNode;
                        }
                        if (!node) {
                            return null;
                        }
                        return node.nextSibling;
                    }
                }
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
                s.insertImage = function (src) {
                    var img = text.contentDocument.createElement("img");
                    img.src = src;
                    img.style = "width:" + s.width + ";height:" + s.height + ";";
                    var sel = text.contentDocument.getSelection();
                    if (sel.rangeCount > 0) {
                        var r = sel.getRangeAt(0);
                        r.deleteContents();
                        r.insertNode(img);
                    } else {
                        text.contentDocument.body.append(img);
                    }
                    s.html = text.contentDocument.body.innerHTML;
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
                };
                s.addTextImage = function () {
                    var selection = text.contentDocument.getSelection();
                    var img = text.contentDocument.createElement("img");
                    img.src = "{Image:" + s.selectedTextImage + "}";
                    if (selection.rangeCount != 0) {
                        var range = selection.getRangeAt(0);
                        range.deleteContents();
                        range.insertNode(img);
                    } else {
                        text.appendChild(img);
                    }
                    s.html = text.contentDocument.body.innerHTML;
                    s.closeDialog();
                };
                s.link = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $mdDialog.show(
                        $mdDialog.prompt()
                            .title("Introduzca Hipervínculo")
                            .textContent("Por favor, introduzca la dirección")
                            .placeholder("http://www.direccion.com")
                            .ok("Aceptar")
                            .cancel("Cancel")
                    ).then(function(content){
                        s.execCommand($event,"createLink", content);
                        });
                };
            }
        };
    }]);