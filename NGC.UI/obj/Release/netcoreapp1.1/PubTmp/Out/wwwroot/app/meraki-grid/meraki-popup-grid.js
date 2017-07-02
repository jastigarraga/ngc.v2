angular.module("Meraki.UI")
    .directive("merakiPopupGrid", ["$http", "$mdDialog", "$mdToast", "$mdMenu", function ($http, $mdDialog, $mdToast, $mdMenu) {
        return {
            restrict:"A",
            require: "ngModel",
            scope: {
                mpg: "=merakiPopupGrid",
                rows:"=ngModel"
            },
            templateUrl: Templates.MerakiPopupGrid,
            link: function (s, e, a, c) {
                var filters = {
                    page: s.page,
                    pageSize: s.pageSize
                };
                s.delete = function (index) {
                    var confirm = $mdDialog.confirm()
                        .title("Confirmar Eliminar")
                        .textContent("¿Seguro que deséa eliminar el registro?")
                        .ariaLabel("Confirmar Eliminar")
                        .ok("Si")
                        .cancel("No")
                    $mdDialog.show(confirm)
                        .then(function () {
                            $mdDialog.hide();
                            s.loading = true;
                            var d = ((s.mpg.transport || {}).delete || {});
                            if (d.url) {
                                $http({
                                    url: d.url,
                                    method: d.method || "POST",
                                    data: (s.rows[index]),
                                    headers: { 'Content-Type': 'application/json' }
                                }).then(function (response) {
                                    s.rows.splice(index, 1);
                                    s.loading = false;
                                    s.to--;
                                    s.total--;
                                }, function (error) {
                                    alert(JSON.stringify(error));
                                    s.loading = false;
                                })
                            } else {
                                s.rows.splice(index, 1);
                                s.loading = false;
                            }
                        },
                        function () {
                            $mdDialog.hide();
                        });
                };
                s.read = function () {
                    var r = (s.mpg.transport || {}).read || {};
                    var data = {
                        page: s.page,
                        pageSize:s.mpg.pageSize
                    };
                    if (r.url) {
                        $http({
                            url: r.url,
                            params: (typeof r.data === "undefined" ? data : r.data(data))
                        })
                            .then(function (response) {
                                s.rows = response.data.Data;
                                s.total = response.data.Total;
                                var ps = s.mpg.pageSize;
                                var p = s.page;
                                var t = s.total;
                                s.from = (p - 1) * ps + 1;
                                s.to = s.from - 1 + s.rows.length;
                                s.pageCount = Math.ceil(t / (ps||1));
                                s.to = s.to > t ? t : s.to;
                                p = p - 3;
                                p = p <= 0 ? 1 : p;
                                for (var i = 0; i < 5; i++) {
                                    s.pageButtons[i] = {
                                        number: p + i,
                                        disabled: ((((p+i)-1)*ps+1)>t)
                                    }
                                }
                            }, function (error) {
                                s.$emit("error", { message: error });
                            });
                    }
                };
                s.from = 0;
                s.to = 0;
                s.total = 0;
                s.pageButtons = [
                    {
                        number: 1,
                        disabled: true
                    }, {
                        number: 2,
                        disabled:true
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
                s.page = 1;

                s.read();
                s.showInsertDialog = function () {
                    var i = (s.mpg.transport || {}).insert;
                    openDialog({}, "Insertar", i, -1, function (data, index) {
                        s.rows.unshift(data);
                        s.to++;
                        s.total++;
                    });
                };
                s.setPage = function (page) {
                    s.page = page;
                    s.read();
                }
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
                                    data: transport.data ? transport.data($scope.model) : $scope.model,
                                    headers: { 'Content-Type': 'application/json' }
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