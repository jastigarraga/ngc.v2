angular.module("Meraki.UI")
    .directive("merakiPopupGrid", ["$http", "$mdDialog", "$mdToast", function ($http, $mdDialog, $mdToast) {
        return {
            restrict:"A",
            require: "ngModel",
            scope: {
                mpg: "=merakiPopupGrid",
                rows:"=ngModel"
            },
            templateUrl: Templates.MerakiPopupGrid,
            link: function (s, e, a, c) {
                s.read = function () {
                    var r = (s.mpg.transport || {}).read || {};
                    if (r.url) {
                        $http({
                            url: r.url,
                            params: r.params ? r.params() : null
                        })
                            .then(function (response) {
                                s.rows = response.data.data;
                                s.total = response.data.total;
                            }, function (error) {

                            });
                    }
                };
                s.from = 1;
                s.to = 5;
                s.page = 2;
                s.pageButtons = [
                    {
                        number: 1,
                        disabled: false
                    }, {
                        number: 2,
                        disabled:false
                    }, {
                        number: 3,
                        disabled:true
                    },{
                        number: 4,
                        disabled:true
                    }, {
                        number: 5,
                        disabled:true
                    }
                ];
                s.read();
                s.showInsertDialog = function () {
                    var i = (s.mpg.transport || {}).insert;
                    openDialog({}, "Insertar", i, -1, function (data, index) {
                        s.$apply(function () { s.rows.unshift(data); });
                    });
                };
                s.showEditDialog = function (model, $index) {
                    var u = (s.mpg.transport || {}).update;
                    openDialog(model, "Editar", u, $index, function (data, index) {
                        s.rows[index] = data;
                    })
                };
                function openDialog(customer,title,transport,index,cb) {
                    $mdDialog.show({
                        templateUrl: s.mpg.templateUrl,
                        locals: {
                            options: {
                                model: angular.copy(customer),
                                title: title || "Editar",
                                transport: transport,
                                index: index,
                                cb:cb
                            }
                        },
                        controller: function ($scope, $mdDialog, options) {
                            var cb = options.cb;
                            var index = options.index;
                            $scope.title = options.title || "Editar";
                            $scope.model = options.model;
                            var transport = options.transport;
                            $scope.getCollection = function (propertyName, url) {
                                $http({
                                    url:url
                                }).then(function (response) {
                                    $scope[propertyName] = response.data;
                                    }, function (error) {

                                })
                            };
                            $scope.cancel = function () {
                                $mdDialog.hide();
                            }
                            $scope.save = function () {
                                if (typeof $scope.modelForm !== "undefined") {
                                    if (!$scope.modelForm.$valid) {
                                        $scope.modelForm.$setSubmitted(true);
                                        return;
                                    }
                                }
                                $scope.$emit("load_start", $scope.$id);
                                $http({
                                    url: transport.url,
                                    method: transport.method,
                                    data: transport.data ? transport.data($scope.model) : $scope.model
                                }).then(function (response) {
                                    cb(response.data, index);
                                    $mdDialog.hide();
                                    $mdToast.showSimple("Guardado!");
                                    }, function (error) {
                                        $scope.emit("error", { message:error });
                                    })
                            }
                        }
                    });
                };
            }
        }
    }])