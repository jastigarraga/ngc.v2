angular.module("Meraki.UI")
    .directive("merakiGrid", ["$http","$mdDialog", function ($http,$mdDialog) {
        return {
            restrict: "A",
            require: "ngModel",
            scope: {
                table: "=ngModel",
                ngcGrid: "=merakiGrid"
            },
            templateUrl: Templates.MerakiGrid,
            link: function (s, e, a, c) {
                s.editIndex = -1;
                s.spinnerText = "Cargando...";
                s.setEditIndex = function (index) {
                    s.inserting = false;
                    s.editIndex = index;
                    s.view = angular.copy(s.table.rows[index]);
                };
                s.showInsertRow = function () {
                    s.editIndex = -1;
                    s.view = {};
                    s.inserting = true;
                };
                s.cancel = function () {
                    s.view = {};
                    s.inserting = 0;
                    s.editIndex = -1;
                };
                s.delete = function (index) {
                    s.cancel();
                    var confirm = $mdDialog.confirm()
                        .title("Confirmar Eliminar")
                        .textContent("¿Seguro que deséa eliminar el registro?")
                        .ariaLabel("Confirmar Eliminar")
                        .ok("Si")
                        .cancel("No")
                    $mdDialog.show(confirm)
                        .then(function () {
                            $mdDialog.hide();
                            s.cancel();
                            s.loading = true;
                            var d = ((s.table.transport || {}).delete || {});
                            if (d.url) {
                                $http({
                                    url: d.url,
                                    method: d.method || "POST",
                                    data: (s.table.rows[index]),
                                    headers: { 'Content-Type': 'application/json' }
                                }).then(function (response) {
                                    s.table.rows.splice(index, 1);
                                    s.loading = false;
                                }, function (error) {
                                    alert(JSON.stringify(error));
                                    s.loading = false;
                                })
                            } else {
                                s.table.rows.splice(index, 1);
                                s.loading = false;
                            }
                        },
                        function () {
                            $mdDialog.hide();
                        });
                };
                
                s.insert = function () {
                    var r = ((s.table.transport || {}).insert || {});
                    s.loading = true;
                    if (r.url) {
                        $http({
                            url: r.url,
                            method: r.method || "POST",
                            data:(typeof r.data === "undefined" ? s.view : r.data(s.view))
                        }).then(function (response) {
                            if (typeof r.success === "undefined") {
                                if (typeof response.data !== "undefined") {
                                    if (typeof response.data.length !== "undefined") {
                                        for (var i in response.data) {
                                            s.table.rows.unshift(response.data[i])
                                        }
                                    } else {
                                        s.table.rows.unshift(response.data);
                                    }
                                } else {
                                    s.table.unshift(response)
                                }
                            } else {
                                r.success(response);
                            }
                            s.view = {};
                            s.inserting = false;
                            s.loading = false;
                        }, function (error) {
                            alert(JSON.stringify(error));

                        });
                    } else {
                        s.table.rows.unshift(angular.copy(s.view));
                        s.view = {};
                        s.inserting = false;
                        s.loading = false;
                    }
                };
                s.update = function (index) {
                    s.loading = true;
                    var u = ((s.table.transport || {}).update || {});
                    if (u.url) {
                        $http({
                            url: u.url,
                            method: u.method || "POST",
                            data: (typeof u.data === "undefined" ? s.view : u.data(s.view, s.table.rows[index]))
                        }).then(function (response) {
                            var data = response.data || reponse;
                            data = data[0] || data;
                            s.table.rows[index] = data;
                            s.editIndex = -1;
                            s.loading = false;
                        }, function (error) {
                            alert(JSON.stringify(error));
                            s.loading = false;
                        })
                    } else {
                        s.table.rows[index] = angular.copy(s.view);
                        s.editIndex = -1;
                        s.loading = false;
                    }
                };
                s.read = function () {
                    s.inserting = false;
                    s.editIndex = -1;
                    s.view = {};
                    var r = ((s.table.transport || {}).read || {});
                    var data = {
                        page: s.table.page || 1,
                        pageSize: s.table.pageSize
                    };
                    if (r.url) {
                        s.loading = true;
                        $http({
                            url: r.url,
                            method: (r.method || "GET"),
                            params: (typeof r.data === "undefined" ? data : r.data(data))
                        }).then(function (resp) {
                            s.table.total = resp.data.total;
                            s.table.from = ((s.table.page || 1) - 1) * s.table.pageSize + 1;
                            s.table.rows = resp.data.data || resp.data || r;
                            s.table.to = s.table.from + s.table.rows.length - 1;
                            s.pageCount = Math.ceil(s.table.total / s.table.pageSize);
                            s.pages = [];
                            var from = (s.table.page || 1) - 3;
                            from = from < 1 ? 1 : from;
                            for (from; from <= s.pageCount && ((s.table.page || 1) + 3) >= from; from++) {
                                s.pages.push(from);
                            }
                            s.loading = false;
                        }, function (error) {
                            s.loading = false;
                        });
                    }

                };
                s.setPage = function (page) {
                    s.table.page = page;
                    s.read();
                }
                s.ngcGrid = {
                    read: s.read
                };
                s.read();
            }
        }
    }])